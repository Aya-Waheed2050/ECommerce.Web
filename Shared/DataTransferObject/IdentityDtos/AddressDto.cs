namespace Shared.DataTransferObject.IdentityDtos
{
    public record AddressDto
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string City { get; init; } = default!;
        public string Street { get; init; } = default!;
    }
}
