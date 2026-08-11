namespace K9UnitApi.DTO_s;

public class PagedDto
{
    public int Id { get; set; }

    public DateTime SessionDate { get; set; }

    public int PerformanceScore { get; set; }

    public string? DogName { get; set; } = string.Empty;
}

public class PageData<T>
{
    public List<T> items { get; set; }
    public int totalCount { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public int totalPages { get; set; }
}
