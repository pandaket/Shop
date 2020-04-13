using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    [Table("products")]
    public class Products
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("category"), ForeignKey("ProductCategory")]
        public int? Category { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public double? Price { get; set; }

        [Column("img")]
        public byte[] Img { get; set; }

        [Column("article")]
        public int? Article { get; set; }

        public virtual Products ProductCategory { get; set; }
    }
}
