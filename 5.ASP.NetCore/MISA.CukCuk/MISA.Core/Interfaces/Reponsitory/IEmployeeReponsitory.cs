using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Reponsitory
{
    public interface IEmployeeReponsitory
    {
        public interface IEmployeeReponsitory
        {
            /// <summary>
            /// Danh sách nhân viên
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            /// CreateBy: DT.Trinh
            List<Employee> Get();

            /// <summary>
            /// Lấy nhân viên theo ID
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            /// CreateBy: DT.Trinh
            Employee GetById(Guid employeeId);

            /// <summary>
            /// Thêm mới nhân viên
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            /// CreateBy: DT.Trinh
            int Add(Employee employee);

            /// <summary>
            /// Sửa nhân viên
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            /// CreateBy: DT.Trinh
            int Update(Employee employee, Guid employeeId);

            /// <summary>
            /// Xóa nhân viên
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            /// CreateBy: DT.Trinh
            int Delete(Guid employeeId);

            /// <summary>
            /// Lấy nhân viên theo mã khách hàng để ktra trùng lặp
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            /// CreateBy: DT.Trinh
            Employee GetByCode(string employeeCode);

        }
    }
}
