namespace RapidApi.Web.Models
{
    public class SearchFormModel
    {
        public string Destination { get; set; } = "";
        public string Checkin { get; set; } = "";   // yyyy-MM-dd (input type="date")
        public string Checkout { get; set; } = "";  // yyyy-MM-dd
        public int Adults { get; set; } = 2;
        public int Children { get; set; } = 0;
        public string Currency { get; set; } = "EUR";
    }

    public class HotelCardViewModel
    {
        public long HotelId { get; set; }
        public string Name { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
        public int Stars { get; set; }
        public string LocationText { get; set; } = "";
        public string Description { get; set; } = "";
        public string ReviewScoreWord { get; set; } = "";
        public int ReviewCount { get; set; }
        public double? ReviewScore { get; set; }
        public string PriceText { get; set; } = "";
    }

    public class HotelListViewModel
    {
        public SearchFormModel Form { get; set; } = new();
        public List<HotelCardViewModel> Hotels { get; set; } = new();
        public string? ErrorMessage { get; set; }
    }

    public class HotelDetailViewModel
    {
        public long HotelId { get; set; }
        public string Name { get; set; } = "";
        public string LocationText { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
        public double? ReviewScore { get; set; }
        public int ReviewCount { get; set; }
        public string Description { get; set; } = "";
        public List<string> Facilities { get; set; } = new();
        public string PriceText { get; set; } = "";
        public string? ErrorMessage { get; set; }
    }
}
