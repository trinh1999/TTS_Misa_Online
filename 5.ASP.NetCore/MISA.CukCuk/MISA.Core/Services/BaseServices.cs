using MISA.Core.Entities;
using MISA.Core.Interfaces.Reponsitory;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseServices:IBaseServices
    {
        IBaseReponsitory _baseReponsitory;
        ServiceResult _serviceResult;

        public BaseServices(IBaseReponsitory baseReponsitory)
        {
            _baseReponsitory = baseReponsitory;
            _serviceResult = new ServiceResult();
        }

        public ServiceResult Add<MISAEntity>(MISAEntity entity)
        {
            //validate dl và xử lý nghiệp vụ
            var isValid = ValidateData<MISAEntity>(entity);
            if(isValid == true)
            {
                isValid = ValidateCustomer<MISAEntity>(entity);
            }
            //Thêm mới
            if(isValid == true)
            {
                _serviceResult.Data = _baseReponsitory.Add<MISAEntity>(entity);
            }
            return _serviceResult;

        }

        /// <summary>
        /// Xử lý Validate dlieu chung
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns>true-dl hợp lệ or ngược lại</returns>
        /// CreatedBy: DT.Trinh
        private bool ValidateData<MISAEntity>(MISAEntity entity)
        {
            var isValid = true;
            // Thực hiện validate
            return isValid;
        }
        protected virtual bool ValidateCustomer<MISAEntity>(MISAEntity entity)
        {
            return true;
        }

        public ServiceResult Update<MISAEntity>(MISAEntity entity, Guid entityId)
        {
            _serviceResult.Data = _baseReponsitory.Update<MISAEntity>(entity,entityId);
            return _serviceResult;
        }

        public ServiceResult Delete<MISAEntity>(Guid entityId)
        {
            _serviceResult.Data = _baseReponsitory.Delete<MISAEntity>(entityId);
            return _serviceResult;
        }

        public ServiceResult Get<MISAEntity>()
        {
            _serviceResult.Data = _baseReponsitory.GetAll<MISAEntity>();
            return _serviceResult;
        }
    }
}
