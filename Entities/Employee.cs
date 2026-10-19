namespace NablusWS.Entities;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }

    public int SiteId { get; set; }
    public WasteCollectionSite Site { get; set; } = null!;
}