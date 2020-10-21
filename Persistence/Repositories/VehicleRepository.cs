using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using asp.net.core.angular.Core;
using asp.net.core.angular.Core.Models;
using asp.net.core.angular.Extensions;
using asp.net.core.angular.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net.core.angular.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _context;

        public VehicleRepository(VegaDbContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery vehicleQuery)
        {
            var result = new QueryResult<Vehicle>();

            // Eager load vehicle information
            var query = _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .AsQueryable();

            // Sort by make and model
            if (vehicleQuery.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == vehicleQuery.MakeId.Value);

            if (vehicleQuery.ModelId.HasValue)
                query = query.Where(v => v.ModelId == vehicleQuery.ModelId.Value);


            // Sort by query params
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactInformation.Name,
            };

            // Provide column sorting
            query = query.ApplyOrdering(vehicleQuery, columnsMap);

            // Calculate Total Pages
            result.TotalItems = await query.CountAsync();

            // Provide Pagination
            query = query.ApplyPagination(vehicleQuery);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelatedData = true)
        {
            if (!includeRelatedData) return await _context.Vehicles.FindAsync(id);

            return await _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Remove(vehicle);
        }
    }
}
