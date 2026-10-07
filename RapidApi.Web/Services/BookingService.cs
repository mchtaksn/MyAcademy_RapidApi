using System.Net.Http.Json;
using System.Text.Json;
using RapidApi.Web.Models.Booking;

namespace RapidApi.Web.Services
{
    // Program.cs'de AddHttpClient<IBookingService, BookingService> ile DI'a
    // eklendi; BaseAddress ve x-rapidapi-key/host header'ları orada set
    // ediliyor, bu sınıf sadece relative path'lerle istek atıyor.
    public class BookingService : IBookingService
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public BookingService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DestinationDto> SearchDestinationAsync(string cityName)
        {
            var url = $"hotels/searchDestination?query={Uri.EscapeDataString(cityName)}";
            var response = await _http.GetFromJsonAsync<BookingApiResponse<List<DestinationDto>>>(url, JsonOptions);

            // GERÇEK YANITI KONTROL ET: "data" dizisinin ilk elemanı en iyi eşleşme.
            var first = response?.Data?.FirstOrDefault();
            if (first is null)
                throw new InvalidOperationException($"\"{cityName}\" için lokasyon bulunamadı.");

            return first;
        }

        public async Task<List<HotelSearchItemDto>> SearchHotelsAsync(
            string destId, string searchType, string checkin, string checkout,
            int adults, int children, string currency)
        {
            var query = new Dictionary<string, string?>
            {
                ["dest_id"] = destId,
                ["search_type"] = searchType,
                ["arrival_date"] = checkin,
                ["departure_date"] = checkout,
                ["adults"] = adults.ToString(),
                ["room_qty"] = "1",
                ["currency_code"] = currency,
            };

            if (children > 0)
                query["children_age"] = string.Join(",", Enumerable.Repeat("8", children));

            var url = "hotels/searchHotels?" + string.Join("&",
                query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value ?? "")}"));

            var response = await _http.GetFromJsonAsync<BookingApiResponse<HotelSearchDataDto>>(url, JsonOptions);

            // GERÇEK YANITI KONTROL ET: "data.hotels" dizisi.
            var hotels = response?.Data?.Hotels;
            if (hotels is null || hotels.Count == 0)
                throw new InvalidOperationException("Bu kriterlere uygun otel bulunamadı.");

            return hotels;
        }

        public async Task<HotelDetailDto> GetHotelDetailsAsync(string hotelId, string checkin, string checkout)
        {
            var url = "hotels/getHotelDetails"
                + $"?hotel_id={Uri.EscapeDataString(hotelId)}"
                + $"&arrival_date={Uri.EscapeDataString(checkin)}"
                + $"&departure_date={Uri.EscapeDataString(checkout)}";
            var response = await _http.GetFromJsonAsync<BookingApiResponse<HotelDetailDto>>(url, JsonOptions);

            // GERÇEK YANITI KONTROL ET: "data" tek bir obje.
            if (response?.Data is null)
                throw new InvalidOperationException("Otel detayı bulunamadı.");

            return response.Data;
        }
    }
}
