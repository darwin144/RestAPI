﻿using System.ComponentModel.DataAnnotations;

namespace RestAPI.ViewModels.Accounts
{
    public class AccountResetPasswordVM
    {
        public int OTP { get; set; }
        public string Email { get; set; }
    }
}
