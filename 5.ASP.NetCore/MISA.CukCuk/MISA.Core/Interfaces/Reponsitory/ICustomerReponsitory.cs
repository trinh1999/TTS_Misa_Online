using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Reponsitory
{
    public interface ICustomerReponsitory
    {
        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        List<Customer> Get();

        /// <summary>
        /// Lấy khách hàng theo ID
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        Customer GetById(Guid customerId);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        int Add(Customer customer);

        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        int Update(Customer customer, Guid customerId);

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        int Delete(Guid customerId);

        /// <summary>
        /// Lấy khách hàng theo mã khách hàng để ktra trùng lặp
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        Customer GetByCode(string customerCode);

    }
}
