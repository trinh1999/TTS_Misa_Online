using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseServices
    {
        /// <summary>
        /// Thêm mới 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult Add<MISAEntity>(MISAEntity employee);

        /// <summary>
        /// Cập nhật thông tin 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeId"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult Update<MISAEntity>(MISAEntity employee, Guid employeeId);

        /// <summary>
        /// Xóa 
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        ServiceResult Delete<MISAEntity>(Guid employeeId);

        /// <summary>
        /// Lấy danh sách 
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <returns></returns>
        ServiceResult Get<MISAEntity>();

        /// <summary>
        /// Xóa theo ID
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <param name="employeeId"></param>
        /// <returns></returns>
    }
}
