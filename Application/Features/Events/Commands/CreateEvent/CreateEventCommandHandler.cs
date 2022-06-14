using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models.Mail;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler:IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    private readonly IEmailService _emailService;

    public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEventCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new Exceptions.ValidationException(validationResult);
        
        var @event = _mapper.Map<Event>(request);

        @event = await _eventRepository.AddAsync(@event);
        
        //Sending email notification to admin address
        var email = new Email() { To = "drisstestcode@gmail.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            //this shouldn't stop the API from doing else so this can be logged
            //_logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
        }

        return @event.EventId;
    }
}