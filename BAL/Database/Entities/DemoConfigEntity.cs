using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database.Entities
{
    [Table("DemoConfigEntities")]
    public class DemoConfigEntity
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Configuration { get; set; }

        public virtual int SubMenuItemId { get; set; }

        [ForeignKey("SubMenuItemId")]
        public virtual SubMenuItemEntity SubMenuItemEntity { get; set; }
    }
}
