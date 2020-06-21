using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripToNCR.Interfaces
{
    public interface IGetAllAsync<T>
    {
        Task<List<T>> GetAllAsync();
    }
}
