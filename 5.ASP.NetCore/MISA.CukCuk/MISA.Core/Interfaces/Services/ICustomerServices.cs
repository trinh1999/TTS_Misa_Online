using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerServices
    {
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult Add(Customer customer);

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult Update(Customer customer, Guid customerId);

        /// <summary>
        /// Xóa thông tin khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult Delete(Guid customerId);

        /// <summary>
        /// lấy ds khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult GetCustomers();

        /// <summary>
        /// Khách hàng theo id
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns>ServiceResult - Kq xử lý nghiệp vụ</returns>
        /// CreateBy: DT.Trinh
        ServiceResult GetCustomerById(Guid customerId);

        
    }
}
