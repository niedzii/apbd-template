namespace kolokwium_niedzii.App.Models.DTOs
{
    public class TripInformation
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int maxPeople { get; set; }
        public IEnumerable<TripInformationCountry> Countries { get; set; }
        public IEnumerable<TripInformationClient> Clients { get; set; }
    }

    public class TripInformationClient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TripInformationCountry
    {
        public string name { get; set; }
    }
}
