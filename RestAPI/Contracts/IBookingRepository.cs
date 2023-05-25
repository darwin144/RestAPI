using RestAPI.Model;
using RestAPI.ViewModels.Bookings;

namespace RestAPI.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
        
        BookingDetailVM GetBookingDetailByGuid(Guid guid);
        IEnumerable<BookingDetailVM> GetAllBookingDetail();



    }
}
