﻿using RestAPI.Utility;
using RestAPI.ViewModels.Employees;
using RestAPI.ViewModels.Rooms;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.ViewModels.Bookings
{
    public class BookingDetailVM
    {
        public Guid? Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public string BookedNIK { get; set; }
        public string Fullname { get; set; }
        public string RoomName { get; set; }

    }
}
