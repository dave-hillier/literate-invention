using System.Text.Json.Serialization;

namespace MinicabitMcp.Models;

public class QuoteRequest
{
    public string FromProperty { get; set; } = string.Empty;
    public string ToProperty { get; set; } = string.Empty;
    public string PickUpDate { get; set; } = string.Empty;
    public string PickUpTime { get; set; } = string.Empty;
    public int NumOfPassengers { get; set; }
    public string TripType { get; set; } = string.Empty;
    public string? ReturnDate { get; set; }
    public string? ReturnTime { get; set; }
    public string? Luggage { get; set; }
    public string? Vias { get; set; }
}

public class CheckRouteRequest
{
    public string FromProperty { get; set; } = string.Empty;
    public string ToProperty { get; set; } = string.Empty;
    public string Vias { get; set; } = string.Empty;
}

public class Quote
{
    [JsonPropertyName("phoId")]
    public string PhoId { get; set; } = string.Empty;
    
    [JsonPropertyName("rating")]
    public int Rating { get; set; }
    
    [JsonPropertyName("locationIsBase")]
    public string LocationIsBase { get; set; } = string.Empty;
    
    [JsonPropertyName("fleetLabel")]
    public string FleetLabel { get; set; } = string.Empty;
    
    [JsonPropertyName("filters")]
    public int[] Filters { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("discount")]
    public string? Discount { get; set; }
    
    [JsonPropertyName("starRating")]
    public double StarRating { get; set; }
    
    [JsonPropertyName("numberOfRatings")]
    public int NumberOfRatings { get; set; }
    
    [JsonPropertyName("carType")]
    public CarType CarType { get; set; } = new();
    
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
    
    [JsonPropertyName("finalPrice")]
    public double FinalPrice { get; set; }
    
    [JsonPropertyName("paymentsTypeAccepted")]
    public string PaymentsTypeAccepted { get; set; } = string.Empty;
    
    [JsonPropertyName("priceType")]
    public string PriceType { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("milesToPho")]
    public double MilesToPho { get; set; }
    
    [JsonPropertyName("qvr")]
    public double Qvr { get; set; }
}

public class CarType
{
    [JsonPropertyName("seatNo")]
    public string SeatNo { get; set; } = string.Empty;
    
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}

public class QuotesResponse
{
    [JsonPropertyName("outboundQuotes")]
    public Quote[] OutboundQuotes { get; set; } = Array.Empty<Quote>();
    
    [JsonPropertyName("returnQuotes")]
    public Quote[] ReturnQuotes { get; set; } = Array.Empty<Quote>();
    
    [JsonPropertyName("searchLogId")]
    public SearchLogId SearchLogId { get; set; } = new();
    
    [JsonPropertyName("exposedQuotes")]
    public ExposedQuotes ExposedQuotes { get; set; } = new();
    
    [JsonPropertyName("redirectInfo")]
    public RedirectInfo RedirectInfo { get; set; } = new();
}

public class SearchLogId
{
    [JsonPropertyName("outbound")]
    public int Outbound { get; set; }
    
    [JsonPropertyName("inbound")]
    public int Inbound { get; set; }
}

public class ExposedQuotes
{
    [JsonPropertyName("outbound")]
    public ExposedQuotesDetail Outbound { get; set; } = new();
    
    [JsonPropertyName("inbound")]
    public ExposedQuotesDetail Inbound { get; set; } = new();
}

public class ExposedQuotesDetail
{
    [JsonPropertyName("bestRated")]
    public Quote? BestRated { get; set; }
    
    [JsonPropertyName("cheapest")]
    public Quote? Cheapest { get; set; }
    
    [JsonPropertyName("cheapestNearby")]
    public Quote? CheapestNearby { get; set; }
    
    [JsonPropertyName("cheapestExecutive")]
    public Quote? CheapestExecutive { get; set; }
}

public class RedirectInfo
{
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class RouteComparisonResponse
{
    [JsonPropertyName("originalRoute")]
    public Route OriginalRoute { get; set; } = new();
    
    [JsonPropertyName("cheaperRoute")]
    public Route CheaperRoute { get; set; } = new();
}

public class Route
{
    [JsonPropertyName("vias")]
    public Via[] Vias { get; set; } = Array.Empty<Via>();
    
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
    
    [JsonPropertyName("distance")]
    public double Distance { get; set; }
}

public class Via
{
    [JsonPropertyName("no")]
    public string No { get; set; } = string.Empty;
    
    [JsonPropertyName("property")]
    public string Property { get; set; } = string.Empty;
    
    [JsonPropertyName("postCode")]
    public string PostCode { get; set; } = string.Empty;
    
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; } = string.Empty;
    
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; } = string.Empty;
}

public class BookingRequest
{
    [JsonPropertyName("tripInfo")]
    public TripInfo TripInfo { get; set; } = new();
    
    [JsonPropertyName("riderDetails")]
    public RiderDetails RiderDetails { get; set; } = new();
    
    [JsonPropertyName("quotes")]
    public BookingQuotes Quotes { get; set; } = new();
    
    [JsonPropertyName("userAgent")]
    public string? UserAgent { get; set; }
    
    [JsonPropertyName("searchLogId")]
    public SearchLogId? SearchLogId { get; set; }
    
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }
    
    [JsonPropertyName("landingUrl")]
    public string? LandingUrl { get; set; }
}

public class TripInfo
{
    [JsonPropertyName("pickUpDate")]
    public string PickUpDate { get; set; } = string.Empty;
    
    [JsonPropertyName("pickUpTime")]
    public string PickUpTime { get; set; } = string.Empty;
    
    [JsonPropertyName("numOfPassengers")]
    public string NumOfPassengers { get; set; } = string.Empty;
    
    [JsonPropertyName("tripType")]
    public string TripType { get; set; } = string.Empty;
    
    [JsonPropertyName("luggage")]
    public string? Luggage { get; set; }
    
    [JsonPropertyName("journeyDurationMin")]
    public double JourneyDurationMin { get; set; }
    
    [JsonPropertyName("vias")]
    public string[] Vias { get; set; } = Array.Empty<string>();
    
    [JsonPropertyName("returnDate")]
    public string? ReturnDate { get; set; }
    
    [JsonPropertyName("returnTime")]
    public string? ReturnTime { get; set; }
    
    [JsonPropertyName("from")]
    public LocationInfo From { get; set; } = new();
    
    [JsonPropertyName("to")]
    public LocationInfo To { get; set; } = new();
}

public class LocationInfo
{
    [JsonPropertyName("fromProperty")]
    public string? FromProperty { get; set; }
    
    [JsonPropertyName("fromPostCode")]
    public string? FromPostCode { get; set; }
    
    [JsonPropertyName("fromStreet")]
    public string? FromStreet { get; set; }
    
    [JsonPropertyName("fromTown")]
    public string? FromTown { get; set; }
    
    [JsonPropertyName("fromCounty")]
    public string? FromCounty { get; set; }
    
    [JsonPropertyName("fromCatId")]
    public string? FromCatId { get; set; }
    
    [JsonPropertyName("toProperty")]
    public string? ToProperty { get; set; }
    
    [JsonPropertyName("toPostCode")]
    public string? ToPostCode { get; set; }
    
    [JsonPropertyName("toStreet")]
    public string? ToStreet { get; set; }
    
    [JsonPropertyName("toTown")]
    public string? ToTown { get; set; }
    
    [JsonPropertyName("toCounty")]
    public string? ToCounty { get; set; }
    
    [JsonPropertyName("toCatId")]
    public string? ToCatId { get; set; }
    
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

public class RiderDetails
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("customerRegAccountId")]
    public int CustomerRegAccountId { get; set; }
    
    [JsonPropertyName("passengerTitle")]
    public string PassengerTitle { get; set; } = string.Empty;
    
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("mobileNum")]
    public string MobileNum { get; set; } = string.Empty;
    
    [JsonPropertyName("receiveSms")]
    public int ReceiveSms { get; set; }
    
    [JsonPropertyName("receiveNewsletters")]
    public int ReceiveNewsletters { get; set; }
    
    [JsonPropertyName("team")]
    public string? Team { get; set; }
    
    [JsonPropertyName("payments")]
    public PaymentDetails? Payments { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("comments")]
    public string? Comments { get; set; }
    
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }
    
    [JsonPropertyName("flightInfo")]
    public string? FlightInfo { get; set; }
    
    [JsonPropertyName("acceptedTAndCs")]
    public int AcceptedTAndCs { get; set; }
    
    [JsonPropertyName("reasonForTravel")]
    public string? ReasonForTravel { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
    
    [JsonPropertyName("isChauffeurit")]
    public string? IsChauffeurit { get; set; }
    
    [JsonPropertyName("signUpNewsletter")]
    public int SignUpNewsletter { get; set; }
}

public class PaymentDetails
{
    [JsonPropertyName("billingAddress")]
    public BillingAddress? BillingAddress { get; set; }
    
    [JsonPropertyName("bin")]
    public string? Bin { get; set; }
    
    [JsonPropertyName("cardType")]
    public string? CardType { get; set; }
    
    [JsonPropertyName("cardholderName")]
    public string? CardholderName { get; set; }
    
    [JsonPropertyName("debit")]
    public string? Debit { get; set; }
    
    [JsonPropertyName("default")]
    public string? Default { get; set; }
    
    [JsonPropertyName("expirationMonth")]
    public string? ExpirationMonth { get; set; }
    
    [JsonPropertyName("expirationYear")]
    public string? ExpirationYear { get; set; }
    
    [JsonPropertyName("expired")]
    public string? Expired { get; set; }
    
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }
    
    [JsonPropertyName("last4")]
    public string? Last4 { get; set; }
    
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    
    [JsonPropertyName("maskedNumber")]
    public string? MaskedNumber { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("isCreditCard")]
    public int IsCreditCard { get; set; }
    
    [JsonPropertyName("isPingit")]
    public string? IsPingit { get; set; }
    
    [JsonPropertyName("isPaypal")]
    public string? IsPaypal { get; set; }
    
    [JsonPropertyName("deviceData")]
    public string? DeviceData { get; set; }
    
    [JsonPropertyName("paymentTransactionType")]
    public string? PaymentTransactionType { get; set; }
    
    [JsonPropertyName("details")]
    public PaymentDetailsInfo? Details { get; set; }
}

public class PaymentDetailsInfo
{
    [JsonPropertyName("nonceValue")]
    public string? NonceValue { get; set; }
    
    [JsonPropertyName("provider")]
    public string? Provider { get; set; }
}

public class BillingAddress
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("customerId")]
    public string? CustomerId { get; set; }
    
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }
    
    [JsonPropertyName("company")]
    public string? Company { get; set; }
    
    [JsonPropertyName("streetAddress")]
    public string? StreetAddress { get; set; }
    
    [JsonPropertyName("extendedAddress")]
    public string? ExtendedAddress { get; set; }
    
    [JsonPropertyName("locality")]
    public string? Locality { get; set; }
    
    [JsonPropertyName("region")]
    public string? Region { get; set; }
    
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }
    
    [JsonPropertyName("countryCodeAlpha2")]
    public string? CountryCodeAlpha2 { get; set; }
    
    [JsonPropertyName("countryCodeAlpha3")]
    public string? CountryCodeAlpha3 { get; set; }
    
    [JsonPropertyName("countryCodeNumeric")]
    public string? CountryCodeNumeric { get; set; }
    
    [JsonPropertyName("countryName")]
    public string? CountryName { get; set; }
    
    [JsonPropertyName("createdAt")]
    public string? CreatedAt { get; set; }
    
    [JsonPropertyName("updatedAt")]
    public string? UpdatedAt { get; set; }
}

public class BookingQuotes
{
    [JsonPropertyName("outbound")]
    public OutboundQuote? Outbound { get; set; }
    
    [JsonPropertyName("quoteId")]
    public string? QuoteId { get; set; }
    
    [JsonPropertyName("finalPrice")]
    public string? FinalPrice { get; set; }
    
    [JsonPropertyName("promocode")]
    public string? Promocode { get; set; }
}

public class OutboundQuote
{
    [JsonPropertyName("phoId")]
    public string? PhoId { get; set; }
    
    [JsonPropertyName("rating")]
    public double Rating { get; set; }
    
    [JsonPropertyName("locationIsBase")]
    public string? LocationIsBase { get; set; }
    
    [JsonPropertyName("fleetLabel")]
    public string? FleetLabel { get; set; }
    
    [JsonPropertyName("filters")]
    public string[] Filters { get; set; } = Array.Empty<string>();
    
    [JsonPropertyName("discount")]
    public string? Discount { get; set; }
    
    [JsonPropertyName("starRating")]
    public double StarRating { get; set; }
    
    [JsonPropertyName("numberOfRatings")]
    public string? NumberOfRatings { get; set; }
    
    [JsonPropertyName("carType")]
    public string? CarType { get; set; }
    
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
    
    [JsonPropertyName("finalPrice")]
    public double FinalPrice { get; set; }
    
    [JsonPropertyName("paymentsTypeAccepted")]
    public string? PaymentsTypeAccepted { get; set; }
    
    [JsonPropertyName("priceType")]
    public string? PriceType { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("milesToPho")]
    public double MilesToPho { get; set; }
    
    [JsonPropertyName("originalPrice")]
    public double OriginalPrice { get; set; }
}

public class BookingResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("bookingRef")]
    public string BookingRef { get; set; } = string.Empty;
    
    [JsonPropertyName("bookingDate")]
    public DateTime BookingDate { get; set; }
    
    [JsonPropertyName("pickup")]
    public DateTime Pickup { get; set; }
    
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;
    
    [JsonPropertyName("fromPostcode")]
    public string FromPostcode { get; set; } = string.Empty;
    
    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;
    
    [JsonPropertyName("toPostcode")]
    public string ToPostcode { get; set; } = string.Empty;
    
    [JsonPropertyName("tripReason")]
    public string? TripReason { get; set; }
    
    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; set; }
    
    [JsonPropertyName("price")]
    public double Price { get; set; }
    
    [JsonPropertyName("totalVat")]
    public double TotalVat { get; set; }
    
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("grossAmount")]
    public double GrossAmount { get; set; }
    
    [JsonPropertyName("bookingType")]
    public string? BookingType { get; set; }
    
    [JsonPropertyName("bookingStatus")]
    public string? BookingStatus { get; set; }
    
    [JsonPropertyName("cancellationDate")]
    public DateTime? CancellationDate { get; set; }
    
    [JsonPropertyName("latestEarlyCancellationTime")]
    public DateTime LatestEarlyCancellationTime { get; set; }
    
    [JsonPropertyName("latestCancellationTime")]
    public DateTime LatestCancellationTime { get; set; }
    
    [JsonPropertyName("returnEta")]
    public DateTime? ReturnEta { get; set; }
    
    [JsonPropertyName("bookingFees")]
    public double BookingFees { get; set; }
    
    [JsonPropertyName("ccCharges")]
    public double CcCharges { get; set; }
    
    [JsonPropertyName("tripCharges")]
    public double TripCharges { get; set; }
    
    [JsonPropertyName("transactionId")]
    public string? TransactionId { get; set; }
    
    [JsonPropertyName("cabOperator")]
    public CabOperator? CabOperator { get; set; }
    
    [JsonPropertyName("tripDetails")]
    public TripDetails? TripDetails { get; set; }
    
    [JsonPropertyName("passengerDetails")]
    public PassengerDetails? PassengerDetails { get; set; }
    
    [JsonPropertyName("poiAttributes")]
    public object? PoiAttributes { get; set; }
}

public class CabOperator
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("rating")]
    public double Rating { get; set; }
    
    [JsonPropertyName("fleetLabel")]
    public string? FleetLabel { get; set; }
}

public class TripDetails
{
    [JsonPropertyName("noOfPassengers")]
    public int NoOfPassengers { get; set; }
    
    [JsonPropertyName("carType")]
    public string? CarType { get; set; }
    
    [JsonPropertyName("additionalInfo")]
    public string? AdditionalInfo { get; set; }
    
    [JsonPropertyName("flightInfo")]
    public string? FlightInfo { get; set; }
    
    [JsonPropertyName("luggage")]
    public object? Luggage { get; set; }
}

public class PassengerDetails
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }
    
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
} 