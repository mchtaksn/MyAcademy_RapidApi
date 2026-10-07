using Microsoft.AspNetCore.Mvc;
using RapidApi.Web.Models;
using RapidApi.Web.Models.Booking;
using RapidApi.Web.Services;

namespace RapidApi.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IBookingService _bookingService;

        public HotelController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET /Hotel/List?destination=Milano&checkin=2026-10-28&checkout=2026-10-31&adults=2&children=0&currency=EUR
        [HttpGet]
        public async Task<IActionResult> List(string destination, string checkin, string checkout,
            int adults = 2, int children = 0, string currency = "EUR")
        {
            var vm = new HotelListViewModel
            {
                Form = new SearchFormModel
                {
                    Destination = destination,
                    Checkin = checkin,
                    Checkout = checkout,
                    Adults = adults,
                    Children = children,
                    Currency = currency
                }
            };

            if (string.IsNullOrWhiteSpace(destination))
            {
                vm.ErrorMessage = "Lütfen bir şehir girin.";
                return View(vm);
            }

            try
            {
                var dest = await _bookingService.SearchDestinationAsync(destination);
                var hotels = await _bookingService.SearchHotelsAsync(
                    dest.DestId, dest.SearchType, checkin, checkout, adults, children, currency);

                vm.Hotels = hotels.Select(MapToCard).ToList();
            }
            catch (Exception ex)
            {
                // Hata yönetimi: case'in istediği "şehir bulunamadı / ağ hatası" senaryosu burada yakalanıyor.
                vm.ErrorMessage = ex.Message;
            }

            return View(vm);
        }

        // GET /Hotel/Details/12345
        [HttpGet]
        public async Task<IActionResult> Details(string id, string? checkin, string? checkout)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            // Liste sayfasından tarih gelmezse (ör. linke doğrudan girildiyse)
            // makul bir varsayılan tarih aralığı kullan.
            checkin ??= DateTime.Today.AddDays(21).ToString("yyyy-MM-dd");
            checkout ??= DateTime.Today.AddDays(24).ToString("yyyy-MM-dd");

            try
            {
                var detail = await _bookingService.GetHotelDetailsAsync(id, checkin, checkout);
                return View(MapToDetail(detail));
            }
            catch (Exception ex)
            {
                return View(new HotelDetailViewModel { ErrorMessage = ex.Message });
            }
        }

        private static HotelCardViewModel MapToCard(HotelSearchItemDto hotel)
        {
            var p = hotel.Property;
            var price = p?.PriceBreakdown?.GrossPrice;

            return new HotelCardViewModel
            {
                HotelId = hotel.HotelId,
                Name = p?.Name ?? "İsimsiz Otel",
                PhotoUrl = p?.PhotoUrls?.FirstOrDefault()
                    ?? "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800&q=80&auto=format&fit=crop",
                Stars = p?.PropertyClass ?? 0,
                LocationText = p?.WishlistName ?? "",
                Description = hotel.AccessibilityLabel ?? "",
                ReviewScoreWord = p?.ReviewScoreWord ?? "",
                ReviewCount = p?.ReviewCount ?? 0,
                ReviewScore = p?.ReviewScore,
                PriceText = price is null ? "Fiyat yok" : FormatPrice(price.Value, price.Currency)
            };
        }

        private static HotelDetailViewModel MapToDetail(HotelDetailDto detail)
        {
            // Genel bir açıklama alanı gelmediği için adresten/bölgeden kısa
            // bir konum metni oluşturuyoruz.
            var locationParts = new[] { detail.District, detail.City }
                .Where(s => !string.IsNullOrWhiteSpace(s));
            var locationText = string.Join(", ", locationParts);

            // Önce family_facilities (düz liste), yoksa facilities_block'tan isimler.
            var facilities = (detail.FamilyFacilities?.Any() == true)
                ? detail.FamilyFacilities
                : detail.FacilitiesBlock?.Facilities?
                    .Select(f => f.Name)
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Select(n => n!)
                    .ToList()
                  ?? new List<string>();

            // Genel galeri yok; ilk odanın ilk fotoğrafını kapak görseli yapıyoruz.
            var photoUrl = detail.Rooms?.Values
                .SelectMany(r => r.Photos ?? new List<RoomPhotoDto>())
                .Select(ph => ph.UrlMax1280 ?? ph.UrlOriginal)
                .FirstOrDefault(u => !string.IsNullOrEmpty(u))
                ?? "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=1200&q=80&auto=format&fit=crop";

            var nightly = detail.ProductPriceBreakdown?.GrossAmountPerNight;

            return new HotelDetailViewModel
            {
                HotelId = detail.HotelId,
                Name = detail.HotelName ?? "Otel",
                LocationText = locationText,
                PhotoUrl = photoUrl,
                ReviewScore = null, // bu endpoint genel bir puan döndürmüyor
                ReviewCount = detail.ReviewNr ?? 0,
                Description = detail.Address is { Length: > 0 }
                    ? $"{detail.Address}, {locationText}"
                    : locationText,
                Facilities = facilities,
                PriceText = nightly is null ? "Fiyat yok" : FormatPrice(nightly.Value, nightly.Currency)
            };
        }

        private static string FormatPrice(double value, string currency)
        {
            var symbol = currency switch
            {
                "EUR" => "€",
                "USD" => "$",
                "TRY" => "₺",
                "GBP" => "£",
                _ => ""
            };
            return $"{symbol}{Math.Round(value)}";
        }
    }
}
