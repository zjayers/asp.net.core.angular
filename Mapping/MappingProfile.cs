using System.Collections.Generic;
using System.Linq;
using asp.net.core.angular.Controllers.Resources;
using asp.net.core.angular.Models;
using AutoMapper;

namespace asp.net.core.angular.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain To API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();

            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactInformationResource()
                    {
                        Name = v.ContactInformation.Name, Email = v.ContactInformation.Email,
                        Phone = v.ContactInformation.PhoneNumber
                    }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactInformationResource()
                    {
                        Name = v.ContactInformation.Name, Email = v.ContactInformation.Email,
                        Phone = v.ContactInformation.PhoneNumber
                    }))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v =>
                        v.Features.Select(vf => new KeyValuePairResource() {Id = vf.Feature.Id, Name = vf.Feature.Name})));

            // API Resource To Domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForPath(v => v.ContactInformation.Name,
                    opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForPath(v => v.ContactInformation.PhoneNumber,
                    opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForPath(v => v.ContactInformation.Email,
                    opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features,
                    opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    // Remove unselected features
                    var featuresToRemove = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in featuresToRemove)
                        v.Features.Remove(f);

                    // Add New Features If They Do Not Exist
                    var featuresToAdd = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id))
                        .Select(id => new VehicleFeature() {FeatureId = id}).ToList();
                    foreach (var f in featuresToAdd)
                        v.Features.Add(f);
                });
        }
    }
}
