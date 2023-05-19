using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Model
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(20)")]
        public string Password { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("otp")]
        public int OTP { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        //kardinalitas
        public Employee? Employee { get; set; }
        public ICollection<AccountRole>? Accountroles {get;set;}
    }
}
