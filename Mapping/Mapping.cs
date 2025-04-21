using AutoMapper;
using TaskManagerApi.Models;
using TaskManagerApi.Dtos.Task;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TaskItem, TaskDto>()
            .ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => src.DueDate.ToString("yyyy-MM-dd")));

        CreateMap<IGrouping<DateTime, TaskItem>, TaskStatsDto>()
            .ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => src.Key.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.Count,
                opt => opt.MapFrom(src => src.Count()));
    }
}