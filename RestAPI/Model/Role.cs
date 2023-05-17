using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Model
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name",TypeName ="nvarchar(50)")]
        public string Name { get; set; }

        // kardinalitas
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
