using RestAPI.Model;
using RestAPI.ViewModels.Bookings;

namespace RestAPI.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
      BookingDetailVM GetBookingDetailByGuid(Booking booking,Employee employee,Room room);
      IEnumerable<BookingDetailVM> GetAllBookingDetail(IEnumerable<Booking> bookings, IEnumerable<Employee> employees, IEnumerable<Room> rooms);


    }
}
