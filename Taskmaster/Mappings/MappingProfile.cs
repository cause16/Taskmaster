using AutoMapper;
using Taskmaster.Models;
using Taskmaster.ViewModels;

namespace Taskmaster.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CompanyDetails, CompanyDetailsViewModel>();

        CreateMap<Project, ProjectViewModel>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));

        CreateMap<Models.Task, TaskViewModel>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => 
                    src.Description != null ? src.Description.Trim() : null));

		CreateMap<DisplaySettings, DisplaySettingsViewModel>()
            .ReverseMap()
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore());
    }
}
