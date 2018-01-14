namespace AddressLibraryClient
{
    public class AddressClient
    {
        private int id;
        private string country;
        private string region;
        private string city;
        private string street;
        private string house;

        public int Id { get => id; set => id = value; }
        public string Country { get => country; set => country = value; }
        public string Region { get => region; set => region = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public string House { get => house; set => house = value; }

        public AddressClient(int id, string country, string region, string city, string street, string house)
        {
            this.Id = id;
            this.Country = country;
            this.Region = region;
            this.City = city;
            this.Street = street;
            this.House = house;
        }

        public AddressClient()
        { }
    }
}