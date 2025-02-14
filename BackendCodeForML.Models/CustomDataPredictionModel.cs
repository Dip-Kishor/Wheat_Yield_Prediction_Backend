using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCodeForML.Models
{
    public class CustomDataPredictionModel
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
