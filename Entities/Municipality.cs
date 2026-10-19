namespace NablusWS.Entities;

public class Municipality
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }     // nullable: optional
    public string? Address { get; set; }

    public ICollection<WasteCollectionSite> Sites { get; set; } = new List<WasteCollectionSite>();
}
