using RoadTripToNCR.Interfaces;
using RoadTripToNCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadTripToNCR.Services
{
    public class CityService:IGetAll<City>
    {
        public List<City> GetAll()
        {
            return new List<City>()
            {
                new City()
                {
                    Name = "Caloocan",
                },
                new City()
                {
                    Name = "Las Piñas",
                },
                new City()
                {
                    Name = "Makati"
                },
                new City()
                {
                    Name = "Malabon"
                },
                new City()
                {
                    Name = "Marikina",
                },
                new City()
                {
                    Name = "Parañaque",
                },
                new City()
                {
                    Name = "Pasay",
                },
                new City()
                {
                    Name = "Pasig",
                },
                new City()
                {
                    Name = "Quezon",
                },
                new City()
                {
                    Name = "San Juan",
                },
                new City()
                {
                    Name = "Taguig "
                },
                new City()
                {
                    Name = "Valenzuela",
                },
                new City()
                {
                    Name = "Mandaluyong",
                },
                new City()
                {
                    Name = "Manila",
                },
                new City()
                {
                    Name = "Muntinlupa",
                },
                new City()
                {
                    Name = "Navotas"
                },
            }.OrderBy(x => x.Name).ToList();
        }
    }
}
