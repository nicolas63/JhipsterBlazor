namespace JhipsterBlazor.Models
{
    public class CountryModel
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        public RegionModel Region { get; set; }
    }
}
