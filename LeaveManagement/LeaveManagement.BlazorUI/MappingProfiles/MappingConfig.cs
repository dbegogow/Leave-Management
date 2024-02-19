namespace LeaveManagement.BlazorUI.MappingProfiles;

using LeaveManagement.BlazorUI.Models.LeaveTypes;
using LeaveManagement.BlazorUI.Services.Base;

using AutoMapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
    }
}
