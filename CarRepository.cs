using ABP_Entity.Entities.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP_Dal.Repositories
{
    public class CarRepository : ICarRepository<Car>
    {
        public Task<Car> Add(Car entity, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Car> Delete(int Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetById(int Id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> GetFull(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> Parse()
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> Print()
        {
            throw new NotImplementedException();
        }

        public Task<Car> Update(Car entity, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
