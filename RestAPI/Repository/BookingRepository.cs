using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Bookings;
using RestAPI.ViewModels.Employees;
using System;

namespace RestAPI.Repository
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
      
        public BookingRepository(BookingManagementContext context) : base(context)
        {
            
        }
        
        //solusi pakai context
        public IEnumerable<BookingDetailVM> GetAllBookingDetail()
        {

            var bookings = GetAll();
            var employees = _context.Employees.ToList();
            var rooms = _context.Rooms.ToList();
                       
            var BookingDetails = from b in bookings
                                 join e in employees on b.EmployeeGuid equals e.Guid
                                 join r in rooms on b.RoomGuid equals r.Guid
                                 select new
                                 {
                                    b.Guid,
                                    e.NIK,
                                     BookedBy = e.FirstName + "" + e.LastName,
                                     r.Name,
                                     b.StartDate,
                                     b.EndDate,
                                     b.Status,
                                     b.Remarks
                                 };
            var BookingDetailConverteds = new List<BookingDetailVM>();
            foreach (var dataBookingDetail in BookingDetails)
            {
                var newBookingDetail = new BookingDetailVM
                {
                    Guid = dataBookingDetail.Guid,
                    StartDate = dataBookingDetail.StartDate,
                    EndDate = dataBookingDetail.EndDate,
                    Status = dataBookingDetail.Status,
                    Remarks = dataBookingDetail.Remarks,
                    BookedNIK = dataBookingDetail.NIK,
                    Fullname = dataBookingDetail.BookedBy,
                    RoomName = dataBookingDetail.Name
                };
                BookingDetailConverteds.Add(newBookingDetail);
            }

            return BookingDetailConverteds;
        }

        public BookingDetailVM GetBookingDetailByGuid(Guid guid)
        {
            var booking = GetByGuid(guid);
            var employee = _context.Employees.Find(booking.EmployeeGuid);
            var room = _context.Rooms.Find(booking.RoomGuid);
            var bookingDetail = new BookingDetailVM
            {
                Guid = booking.Guid,
                BookedNIK = employee.NIK,
                Fullname = employee.FirstName + " " + employee.LastName,
                RoomName = room.Name,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                               
            };           
            return bookingDetail;
        }

    }
}
