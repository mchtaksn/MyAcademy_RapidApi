using RapidApi.Web.Models.Booking;

namespace RapidApi.Web.Services
{
    public interface IBookingService
    {
        Task<DestinationDto> SearchDestinationAsync(string cityName);

        Task<List<HotelSearchItemDto>> SearchHotelsAsync(
            string destId, string searchType, string checkin, string checkout,
            int adults, int children, string currency);

        Task<HotelDetailDto> GetHotelDetailsAsync(string hotelId, string checkin, string checkout);
    }
}
