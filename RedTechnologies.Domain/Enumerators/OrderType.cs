using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RedTechnologies.Domain.Enumerators
{
    public enum OrderType
    {        
        Standard,
        SaleOrder,
        PurchaseOrder,
        TransferOrder,
        ReturnsOrder        
    }
}
