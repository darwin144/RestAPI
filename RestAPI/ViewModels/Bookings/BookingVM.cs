﻿using RestAPI.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.ViewModels.Bookings
{
    public class BookingVM
    {
        public Guid? Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
    }
}
