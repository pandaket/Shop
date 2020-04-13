using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    [Table("categories")]
    public class Categories
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("name")]
        public int Name { get; set; }

        [Column("parentid"), ForeignKey("ParentCategory")]
        public int Parentid { get; set; }

        public virtual Categories ParentCategory { get; set; }
    }
}
