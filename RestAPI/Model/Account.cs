﻿namespace RestAPI.Model
{
    public class Account : BaseEntity
    {
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

    }
}