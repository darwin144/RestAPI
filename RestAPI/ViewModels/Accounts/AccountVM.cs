using RestAPI.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.ViewModels.Accounts
{
    public class AccountVM
    {
        public Guid? Guid { get; set; }
        public string Password { get; set; }        
        public int OTP { get; set; }
       
    }
}
