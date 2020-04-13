using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    [Table("busket")]
    public class Busket
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("idproduct"), ForeignKey("SelectedProduct")]
        public int IdProduct { get; set; }

        [Column("kol")]
        public int Kol { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("iduser"), ForeignKey("Customer")]
        public int Iduser { get; set; }

        public virtual Users Customer { get; set; }
        public virtual Products SelectedProduct { get; set; }
    }
}
