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

        public IEnumerable<BookingDetailVM> GetAllBookingDetail(IEnumerable<Booking> bookings, IEnumerable<Employee> employees, IEnumerable<Room> rooms)
        {
            
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

        public BookingDetailVM GetBookingDetailByGuid(Booking booking, Employee employee, Room room)
        {
            var bookingDetail = new
            {
                booking.Guid,
                employee.NIK,
                BookedBy = employee.FirstName + " " + employee.LastName,
                room.Name,
                booking.StartDate,
                booking.EndDate,
                booking.Status,
                booking.Remarks
            };
            var bookingDetailVM = new BookingDetailVM
            {

                Guid = bookingDetail.Guid,
                BookedNIK = bookingDetail.NIK,
                Fullname = bookingDetail.BookedBy,
                RoomName = bookingDetail.Name,
                StartDate = bookingDetail.StartDate,
                EndDate = bookingDetail.EndDate,
                Status = bookingDetail.Status,
                Remarks = bookingDetail.Remarks,

            };
            return bookingDetailVM;
        }

    }
}
