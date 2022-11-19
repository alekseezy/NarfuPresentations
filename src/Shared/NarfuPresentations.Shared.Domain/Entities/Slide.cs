namespace NarfuPresentations.Shared.Domain.Entities;

public record Slide : BaseEntity<Guid>
{
    public int Number { get; set; }
    public bool IsServey { get; set; }
    public ImageSlide? ImageSlide { get; set; }
    public ServeySlide? ServeySlide { get; set; }
}

public record ImageSlide : BaseEntity<Guid>
{
    public string ImageUrl { get; set; } = default!;
}

public record ServeySlide : BaseEntity<Guid>
{
    public Servey Servey { get; set; } = default!;
}

public record Servey : BaseEntity<Guid>
{
    public string Title { get; set; } = default!;
    public List<Question> Questions { get; set; } = default!;
}

public record Question : BaseEntity<Guid>
{
    public int Number { get; set; }
    public string Description { get; set; } = default!;
    public List<Answer> PossibleAnswers { get; set; } = default!;
}

public record Answer : BaseEntity<Guid>
{
    public string Description { get; set; } = default!;
    public bool IsRight { get; set; }
}
