using ABP_Entity.Entities.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP_Dal.Repositories
{
    public interface ICarRepository <T>: IModelRepository<Car>
    {
    }
}
