using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    [Table("productsize")]
    public class Productsize
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("idproduct"), ForeignKey("SelectedProduct")]
        public int IdProduct { get; set; }

        [Column("availables")]
        public int AvailableS { get; set; }

        [Column("availablexs")]
        public int AvailableXS { get; set; }

        [Column("availablem")]
        public int AvailableM { get; set; }

        [Column("availablel")]
        public int AvailableL { get; set; }

        [Column("availablexl")]
        public int AvailableXL { get; set; }

        public virtual Products SelectedProduct { get; set; }
    }
}
