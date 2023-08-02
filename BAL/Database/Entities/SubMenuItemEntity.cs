using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database.Entities
{
    [Table("SubMenuItemEntities")]
    public class SubMenuItemEntity : BaseEntity
    {
        public virtual string SubMenuName { get; set; }

        public virtual int MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItemEntity MenuItemEntity { get; set; }

        public virtual string Key { get; set; }
    }
}
