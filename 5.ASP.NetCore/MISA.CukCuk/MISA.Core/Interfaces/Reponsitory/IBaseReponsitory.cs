using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Reponsitory
{
    public interface IBaseReponsitory
    {
        List<MISAEntity> GetAll<MISAEntity>();
        int Add<MISAEntity>(MISAEntity entity);
        int Update<MISAEntity>(MISAEntity entity, Guid entityId);
        int Delete<MISAEntity>(Guid entityId);

    }
}
