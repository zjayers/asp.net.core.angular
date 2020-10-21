using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using asp.net.core.angular.Controllers.Resources;
using asp.net.core.angular.Core;
using asp.net.core.angular.Core.Models;
using asp.net.core.angular.Persistence;
using asp.net.core.angular.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace asp.net.core.angular.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PhotoSettings photoSettings;

        public PhotosController(IOptionsSnapshot<PhotoSettings> options, IWebHostEnvironment host,
            IVehicleRepository vehicleRepository, IPhotoRepository photoRepository, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.photoSettings = options.Value;
            _host = host;
            _vehicleRepository = vehicleRepository;
            _photoRepository = photoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            // Ensure vehicle is valid
            var vehicle = await _vehicleRepository.GetVehicle(vehicleId, false);
            if (vehicle == null) return NotFound();

            if (file == null) return BadRequest("File is null");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Maximum file size exceeded");


            if (!photoSettings.IsSupported(file.FileName))
                return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");

            // Create directory if not exists
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            // Generate filename for the photo
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fileSavePath = Path.Combine(uploadsFolderPath, filename);

            // Stream the input file and store it in the path
            await using var stream = new FileStream(fileSavePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var photo = new Photo() {FileName = filename};
            vehicle.Photos.Add(photo);

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Photo, PhotoResource>(photo));
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await _photoRepository.GetPhotos(vehicleId);
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
    }
}
