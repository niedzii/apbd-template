using kolokwium_niedzii.App.Models;
using kolokwium_niedzii.App.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kolokwium_niedzii.App.Services
{
    public interface ICountriesService
    {
        public Task<IEnumerable<TripInformation>> GetExampleData();
    }

    public class CountriesService : ICountriesService
    {
        private readonly ApbdDbContext _context;

        public CountriesService(ApbdDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripInformation>> GetExampleData()
        {
            return await _context.Trips.Select(trip => new TripInformation
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                maxPeople = trip.MaxPeople,
                Clients = trip.ClientTrips.Select(clientTrip => new TripInformationClient
                {
                    FirstName = clientTrip.IdClientNavigation.FirstName,
                    LastName = clientTrip.IdClientNavigation.LastName,
                }).ToList(),
                Countries = trip.IdCountries.Select(country => new TripInformationCountry{
                    name = country.Name
                }).ToList()
            }).OrderByDescending(trip => trip.DateFrom).ToListAsync();
        }
    }
}
