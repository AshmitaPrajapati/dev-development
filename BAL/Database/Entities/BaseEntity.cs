using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int? CreatedBy { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual int? UpdatedBy { get; set; }
        public virtual DateTime? UpdatedOn { get; set; }
        public virtual int? TenantId { get; set; }

        public BaseEntity()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
