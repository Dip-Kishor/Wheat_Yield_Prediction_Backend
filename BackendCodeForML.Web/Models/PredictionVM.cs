namespace BackendCodeForML.Web.Models
{
    public class PredictionVM
    {
        public string DistrictName { get; set; }
        
    }
    public class CustomDataVM
    {
        public decimal Rainfall { get; set; }
        public decimal AvgTemp { get; set; }
        public decimal RelativeHumidity { get; set; }
        public decimal SoilTemp { get; set; }
        public decimal Sand { get; set; }
        public decimal PHLevel { get; set; }
        public decimal Phosphorus { get; set; }
        public decimal Potassium { get; set; }
        public decimal Clay { get; set; }
        public decimal ProductionArea { get; set; }
    }
}
