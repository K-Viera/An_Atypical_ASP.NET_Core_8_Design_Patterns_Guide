using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappers
{
    public interface IMapper<TSource,TDestination>
    {
        TDestination Map(TSource entity);
    }
}
