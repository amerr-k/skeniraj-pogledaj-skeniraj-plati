using System.Threading.Tasks;

namespace SPSP.Services.Base
{
    public interface ICRUDService<T, TSearch, TInsert, TUpdate> 
        : IService<T, TSearch> 
        where TSearch : class 
        where TInsert : class 
        where TUpdate : class
    {
        Task<T> Insert(TInsert insert);
        Task<T> Update(int id, TUpdate update);
    }
}
