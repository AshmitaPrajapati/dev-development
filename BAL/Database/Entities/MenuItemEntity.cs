using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database.Entities
{
    [Table("MenuItemEntities")]
    public class MenuItemEntity : BaseEntity
    {
        public virtual string MenuName { get; set; }

        public virtual string Icon { get; set; }
    }
}
