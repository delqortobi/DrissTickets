using Application.Features.Categories.Commands.CreateCateogry;
using Application.Features.Categories.Queries.GetCategoriesList;
using Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Commands.UpdateEvent;
using Application.Features.Events.Queries.GetEventDetail;
using Application.Features.Events.Queries.GetEventsExport;
using Application.Features.Events.Queries.GetEventsList;
using Application.Features.Orders.GetOrdersForMonth;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventListVm>().ReverseMap();
        CreateMap<Event, CreateEventCommand>().ReverseMap();
        CreateMap<Event, UpdateEventCommand>().ReverseMap();
        CreateMap<Event, EventDetailVm>().ReverseMap();
        CreateMap<Event, CategoryEventDto>().ReverseMap();
        CreateMap<Event, EventExportDto>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryEventListVm>();
        CreateMap<Category, CreateCategoryCommand>();
        CreateMap<Category, CreateCategoryDto>();

        CreateMap<Order, OrdersForMonthDto>();
    }
}