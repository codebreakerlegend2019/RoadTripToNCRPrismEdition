using System;
using System.Collections.Generic;
using System.Text;

namespace RoadTripToNCR.Interfaces
{
    public interface IGetAll<T>
    {
        List<T> GetAll();
    }
}
