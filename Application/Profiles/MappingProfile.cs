using Application.Features.Categories.Queries.GetCategoriesList;
using Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Commands.UpdateEvent;
using Application.Features.Events.Queries.GetEventDetail;
using Application.Features.Events.Queries.GetEventsList;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventListVm>().ReverseMap();
        CreateMap<Event, EventDetailVm>().ReverseMap();
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryEventListVm>();
        CreateMap<Event, CreateEventCommand>().ReverseMap();
        CreateMap<Event, UpdateEventCommand>().ReverseMap();
    }
}