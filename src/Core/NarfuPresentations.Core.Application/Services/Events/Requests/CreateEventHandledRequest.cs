using FluentValidation;

using Mapster;

using MediatR;

using NarfuPresentations.Core.Application.Authentication.Exceptions;
using NarfuPresentations.Core.Application.Common.FileStorage;
using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Application.Persistense;
using NarfuPresentations.Shared.Contracts.Event.Requests;
using NarfuPresentations.Shared.Contracts.FileStorage.Requests;
using NarfuPresentations.Shared.Domain.Common.Enums;
using NarfuPresentations.Shared.Domain.Entities;

namespace NarfuPresentations.Core.Application.Services.Events.Requests;

public record CreateEventHandledRequest : IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public FileUploadRequest Image { get; set; } = default!;
    public AccessType AccessType { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CreatorId { get; set; } = default!;

    public DateTime StartsOn { get; set; } = default!;

    public IEnumerable<(Guid UserId, UserRole Role)> Participants { get; set; } = default!;
    public IEnumerable<Presentation> Presentations { get; set; } = default!;

    public CreateEventHandledRequest(CreateEventRequest request)
    {
        Title = request.Title;
        Image = request.Image;
        AccessType = request.AccessType;
        Description = request.Description;
        StartsOn = request.StartsOn;
        Participants = request.Participants ?? new List<(Guid, UserRole)>();
        Presentations = request.Presentations ?? new List<Presentation>();
    }
}

public class CreateEventRequestValidator : AbstractValidator<CreateEventHandledRequest>
{
    public CreateEventRequestValidator(ICurrentUser currentUser)
    {
        RuleFor(properties => properties.Title)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(75);

        RuleFor(properties => properties.Image)
            .InjectValidator();

        RuleFor(properties => properties.AccessType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(properties => properties.Description)
            .MaximumLength(400);

        RuleFor(properties => properties.CreatorId)
            .NotEmpty()
            .Must(creator => creator != currentUser.GetUserId())
                .WithMessage("You cannot create an event with as another user.");

        RuleFor(properties => properties.StartsOn)
            .NotEmpty()
            .Must(startsOn => startsOn.Date >= DateTime.UtcNow.Date);
    }
}

public class CreateEventRequestHandler : IRequestHandler<CreateEventHandledRequest, Guid>
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IFileStorageService _fileStorage;

    public CreateEventRequestHandler(IRepository<Event> eventRepository, IFileStorageService fileStorage)
    {
        _eventRepository = eventRepository;
        _fileStorage = fileStorage;
    }

    public async Task<Guid> Handle(CreateEventHandledRequest request, CancellationToken cancellationToken)
    {
        var @event = new Event
        {
            Title = request.Title,
            Description = request.Description,
            ImageUrl = await _fileStorage.UploadAsync<Event>(request.Image, FileType.Image, cancellationToken),
            AccessType = request.AccessType,
            CreatorId = request.CreatorId,
            Participants = request.Participants?.Adapt<IEnumerable<Participant>>() ?? new List<Participant>(),
        };

        await _eventRepository.AddAsync(@event, cancellationToken);

        return @event.Id;
    }
}
