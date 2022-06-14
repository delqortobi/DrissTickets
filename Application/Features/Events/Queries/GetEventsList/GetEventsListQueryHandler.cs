using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Events.Queries.GetEventsList;

public record GetEventsListQuery : IRequest<List<EventListVm>>;

public class GetEventsListQueryHandler: IRequestHandler<GetEventsListQuery, List<EventListVm>>
{
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetEventsListQueryHandler(IAsyncRepository<Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
    {
        var allEvents = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
        return _mapper.Map<List<EventListVm>>(allEvents);
    }
}