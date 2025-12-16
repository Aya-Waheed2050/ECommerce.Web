namespace Domain.Models.OrderModule
{
    public class OrderAddress
    {
        public OrderAddress()
        {
            
        }
        public OrderAddress(string firstName, string lastName, string country ,string city, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

    }
}
