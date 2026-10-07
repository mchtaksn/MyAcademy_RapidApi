using System.Text.Json;
using System.Text.Json.Serialization;

namespace RapidApi.Web.Models.Booking
{
    // RapidAPI (booking-com15) yanıtlarının hepsi bu zarfın içinde geliyor.
    // GERÇEK YANITI GÖRÜNCE kontrol et: "status"/"data" isimleri genelde
    // sabit kalıyor ama emin olmak için Postman'dan bir kez bak.
    public class BookingApiResponse<T>
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        // "message" alanı API'de bazen string, bazen obje/dizi geliyor.
        // Biz hiç kullanmıyoruz zaten; JsonElement ile her şekli kabul edip
        // deserialize hatasını önlüyoruz.
        [JsonPropertyName("message")]
        public JsonElement Message { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }

    public class DestinationDto
    {
        [JsonPropertyName("dest_id")]
        public string DestId { get; set; } = "";

        [JsonPropertyName("search_type")]
        public string SearchType { get; set; } = "CITY";

        [JsonPropertyName("city_name")]
        public string? CityName { get; set; }

        [JsonPropertyName("dest_type")]
        public string? DestType { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }

    public class HotelSearchDataDto
    {
        [JsonPropertyName("hotels")]
        public List<HotelSearchItemDto> Hotels { get; set; } = new();
    }

    public class HotelSearchItemDto
    {
        [JsonPropertyName("hotel_id")]
        public long HotelId { get; set; }

        [JsonPropertyName("property")]
        public HotelPropertyDto? Property { get; set; }

        [JsonPropertyName("accessibilityLabel")]
        public string? AccessibilityLabel { get; set; }
    }

    public class HotelPropertyDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("photoUrls")]
        public List<string>? PhotoUrls { get; set; }

        [JsonPropertyName("reviewScore")]
        public double? ReviewScore { get; set; }

        [JsonPropertyName("reviewCount")]
        public int? ReviewCount { get; set; }

        [JsonPropertyName("reviewScoreWord")]
        public string? ReviewScoreWord { get; set; }

        [JsonPropertyName("wishlistName")]
        public string? WishlistName { get; set; }

        [JsonPropertyName("propertyClass")]
        public int? PropertyClass { get; set; }

        [JsonPropertyName("priceBreakdown")]
        public PriceBreakdownDto? PriceBreakdown { get; set; }
    }

    public class PriceBreakdownDto
    {
        [JsonPropertyName("grossPrice")]
        public PriceDto? GrossPrice { get; set; }
    }

    public class PriceDto
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "EUR";
    }

    // Get Hotel Details yanıtı Search Hotels'ten FARKLI bir şekle sahip:
    // "property" sarmalayıcısı yok, her şey düz (flat) geliyor, ve genel bir
    // açıklama/galeri alanı da yok — fotoğraflar sadece "rooms" içinde.
    public class HotelDetailDto
    {
        [JsonPropertyName("hotel_id")]
        public long HotelId { get; set; }

        [JsonPropertyName("hotel_name")]
        public string? HotelName { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("district")]
        public string? District { get; set; }

        [JsonPropertyName("review_nr")]
        public int? ReviewNr { get; set; }

        // Düz (flat) facility isimleri — en kolay kullanılabilecek liste bu.
        [JsonPropertyName("family_facilities")]
        public List<string>? FamilyFacilities { get; set; }

        [JsonPropertyName("facilities_block")]
        public FacilitiesBlockDto? FacilitiesBlock { get; set; }

        [JsonPropertyName("product_price_breakdown")]
        public ProductPriceBreakdownDto? ProductPriceBreakdown { get; set; }

        // room_id -> oda detayı. Genel galeri fotoğrafı olmadığı için ilk
        // odanın ilk fotoğrafını kapak görseli olarak kullanıyoruz.
        [JsonPropertyName("rooms")]
        public Dictionary<string, RoomDto>? Rooms { get; set; }
    }

    public class FacilitiesBlockDto
    {
        [JsonPropertyName("facilities")]
        public List<FacilityItemDto>? Facilities { get; set; }
    }

    public class FacilityItemDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class ProductPriceBreakdownDto
    {
        [JsonPropertyName("gross_amount_per_night")]
        public PriceDto? GrossAmountPerNight { get; set; }

        [JsonPropertyName("gross_amount")]
        public PriceDto? GrossAmount { get; set; }
    }

    public class RoomDto
    {
        [JsonPropertyName("photos")]
        public List<RoomPhotoDto>? Photos { get; set; }
    }

    public class RoomPhotoDto
    {
        [JsonPropertyName("url_max1280")]
        public string? UrlMax1280 { get; set; }

        [JsonPropertyName("url_original")]
        public string? UrlOriginal { get; set; }
    }
}
