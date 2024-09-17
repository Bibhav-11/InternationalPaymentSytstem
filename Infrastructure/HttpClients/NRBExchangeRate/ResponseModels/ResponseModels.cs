namespace Infrastructure.HttpClients.NRBExchangeRate.ResponseModels;

public class Currency
{
    public string? Iso3 { get; set; }
    public string? Name { get; set; }
    public int? Unit { get; set; }
}

public class Data
{
    public List<Payload> Payload { get; set; }
}

public class Errors
{
    public object? Validation { get; set; }
}

public class Links
{
    public object? Prev { get; set; }
    public object? Next { get; set; }
}

public class Pagination
{
    public int? Page { get; set; }
    public int? Pages { get; set; }
    public int? PerPage { get; set; }
    public int? Total { get; set; }
    public Links Links { get; set; }
}

public class Params
{
    public object? Date { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public object? PostType { get; set; }
    public string? PerPage { get; set; }
    public string? Page { get; set; }
    public object? Slug { get; set; }
    public object? Q { get; set; }
}

public class Payload
{
    public string? Date { get; set; }
    public string? PublishedOn { get; set; }
    public string? ModifiedOn { get; set; }
    public List<Rate> Rates { get; set; }
}

public class Rate
{
    public Currency? Currency { get; set; }
    public string? Buy { get; set; }
    public string? Sell { get; set; }
}

public class Status
{
    public int? Code { get; set; }
}

