using System.Runtime.Serialization;

namespace RedTechnologies.Application.Model
{
    public class OrderModel
    {
        public string? Id { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string CustomerName { get; set; }  = null!;
        public string? CreatedDate { get; set; }
        public string? CreatedByUsername { get; set; }
    }
}
