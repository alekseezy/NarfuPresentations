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
    public IEnumerable<ServeyEntry> Serveys { get; set; } = default!;
}

public record ServeyEntry(
    int Number,
    string Question,
    IEnumerable<string> PossibleAnswers,
    IEnumerable<int> RightAnswers) : BaseEntity<Guid>;