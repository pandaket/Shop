using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    [Table("orders")]
    public class Orders
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("idproduct"), ForeignKey("SelectedProduct")]
        public int IdProduct { get; set; }

        [Column("iduser"), ForeignKey("Customer")]
        public int Iduser { get; set; }

        [Column("kol")]
        public int Kol { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("payment")]
        public int Payment { get; set; }

        [Column("status"), ForeignKey("OrderStatus")]
        public int Status { get; set; }

        public virtual Users Customer { get; set; }
        public virtual Products SelectedProduct { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
