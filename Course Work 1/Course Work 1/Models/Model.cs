using System;
using System.Data.Common;

namespace Course_Work_1.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public DateTime ModelProductionDate { get; set; }
        public string ModelBodyType { get; set; }
    }
}
