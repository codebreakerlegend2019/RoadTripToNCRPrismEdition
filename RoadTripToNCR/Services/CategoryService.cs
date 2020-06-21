using RoadTripToNCR.Interfaces;
using RoadTripToNCR.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadTripToNCR.Services
{
    public class CategoryService : IGetAll<Category>
    {
        public List<Category> GetAll()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Name = "Church"
                },
                new Category()
                {
                    Name= "Malls"
                },
                new Category()
                {
                    Name = "Park"
                },
                new Category()
                {
                    Name= "Historical"
                }
            };
        }
    }
}
