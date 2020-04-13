using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMVCTest2.DBUtil
{
    public interface IManage<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        bool Post(T elem);
        bool Put(int id, T elem);
        bool Delete(int id);
    }
}
