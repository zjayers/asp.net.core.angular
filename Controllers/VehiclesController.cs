using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using asp.net.core.angular.Controllers.Resources;
using asp.net.core.angular.Core;
using asp.net.core.angular.Core.Models;
using asp.net.core.angular.Models;
using asp.net.core.angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.core.angular.Controllers
{
    [Route("/api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetAllVehicles(VehicleQueryResource vehicleQueryResource)
        {
            var filter = _mapper.Map<VehicleQueryResource, VehicleQuery>(vehicleQueryResource);
            var queryResult = await _vehicleRepository.GetVehicles(filter);

            return _mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id);

            if (vehicle == null) return NotFound(id);

            var mappedVehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(mappedVehicleResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate that the model ID exists in the Database
            // var model = await _context.Models.FindAsync(saveVehicleResource.ModelId);
            //
            // if (model == null)
            // {
            //     ModelState.AddModelError("ModelID", "Invalid modelId.");
            //     return BadRequest(ModelState);
            // }

            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            _vehicleRepository.Add(vehicle);
            await _unitOfWork.CompleteAsync();

            vehicle = await _vehicleRepository.GetVehicle(vehicle.Id);

            var mappedVehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(mappedVehicleResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _vehicleRepository.GetVehicle(id);

            if (vehicle == null) return NotFound(id);

            _mapper.Map(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            var mappedVehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(mappedVehicleResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id, includeRelatedData: false);

            if (vehicle == null) return NotFound(id);

            _vehicleRepository.Remove(vehicle);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
