namespace NablusWS.Entities;

public class Language
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;   
    public string Name { get; set; } = string.Empty;   

    public ICollection<SiteTranslation> SiteTranslations { get; set; } = new List<SiteTranslation>();
}
