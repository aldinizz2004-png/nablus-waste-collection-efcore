namespace NablusWS.Entities;

public class SiteTranslation
{
    
    public int SiteId { get; set; }
    public WasteCollectionSite Site { get; set; } = null!;

    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;

    public string Name { get; set; } = string.Empty;   
}
