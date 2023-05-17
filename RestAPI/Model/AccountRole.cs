namespace RestAPI.Model
{
    public class AccountRole : BaseEntity
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
    }
}
