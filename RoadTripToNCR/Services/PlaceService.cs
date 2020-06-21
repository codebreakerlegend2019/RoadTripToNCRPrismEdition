using RoadTripToNCR.Helpers.JdsClientTool;
using RoadTripToNCR.Interfaces;
using RoadTripToNCR.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripToNCR.Services
{
    public class PlaceService : IGetAllAsync<Place>
    {
        public async Task<List<Place>> GetAllAsync()
        {
            var placeRequest = await JdsClient.GetManyAsync<Place>("places.json");
            if (placeRequest.StatusCode == HttpStatusCode.OK)
                return placeRequest.Data;
            return new List<Place>();
        }
    }
}
