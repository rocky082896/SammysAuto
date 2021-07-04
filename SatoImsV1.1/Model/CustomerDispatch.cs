using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    public class CustomerDispatch
    {
        [Key, ForeignKey("Customer")]
        [Column(Order = 0)]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Key, ForeignKey("Dispatch")]
        [Column(Order = 1)]
        public int DispatchId { get; set; }

        public virtual Dispatch Dispatch { get; set; }
    }
}
