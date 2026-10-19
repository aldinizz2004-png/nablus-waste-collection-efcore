namespace NablusWS.Entities;

public class WasteCollectionSite
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int MunicipalityId { get; set; }
    public Municipality Municipality { get; set; } = null!;

    public Employee? Employee { get; set; } //nullable:optional
    public ICollection<SiteTranslation> Translations { get; set; } = new List<SiteTranslation>();

}