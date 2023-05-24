using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.ViewModels.AccontRole
{
    public class AccountRoleVM
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
    }
}
