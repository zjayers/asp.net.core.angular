using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net.core.angular.Controllers.Resources;
using asp.net.core.angular.Models;
using asp.net.core.angular.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.core.angular.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/api/[controller]")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<MakeResource> GetOneMake(int id)
        {
            var make = await _context.Makes.Include(m => m.Models).SingleOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<Make, MakeResource>(make);
        }
    }
}
