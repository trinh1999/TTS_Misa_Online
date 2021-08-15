using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Reponsitory;
using MISA.Core.Interfaces.Services;
using MISA.Core.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Reponsitory
{
    public class EmployeeReponsitory : BaseReponsitory, IEmployeeReponsitory
    {
        IEmployeeServices _employeeServices;
        ServiceResult _serviceResult;
        public EmployeeReponsitory(IBaseServices baseServices):base(baseServices)
        { 
            _serviceResult = new ServiceResult();
            //_employeeRepository = employeeReponsitory;

        }
        /// <summary>
        /// Thêm nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// CreatedBy: DT. Trinh
        //    public int Add(Employee employee)
        //    {
        //        //Khởi tạo Id ms cho đối tượng
        //        employee.EmployeeId = Guid.NewGuid();
        //        //Truy cập vào database
        //        //1. Khởi tạo thông tin kết nối database
        //        var connectionString = "Host= 47.241.69.179;" +
        //            "Database = MISA.CukCuk_Demo_NVMANH;" +
        //            "User Id = dev;" +
        //            "Password = 12345678;";

        //        //2. Khởi tạo đối tượng kết nối với database
        //        IDbConnection dbConnection = new MySqlConnection(connectionString);

        //        //Khai báo dyanamicParam:
        //        var dyanamicParam = new DynamicParameters();

        //        //3. Thêm dữ liệu vào trong database
        //        var columnsName = string.Empty;

        //        var columnsParam = string.Empty;

        //        //Đọc từng property của object:
        //        var properties = employee.GetType().GetProperties();
        //        foreach (var prop in properties)
        //        {
        //            //Lấy tên của prop:
        //            var propName = prop.Name;

        //            //Lấy value của prop
        //            var propValue = prop.GetValue(employee);

        //            //Lấy kiểu dữ liệu của prop
        //            var propType = prop.PropertyType;

        //            //Thêm param tương ứng với mỗi property của đối tượng
        //            dyanamicParam.Add($"@{propName}", propValue);

        //            columnsName += $"{propName},";

        //            columnsParam += $"@{propName},";

        //        }

        //        columnsName = columnsName.Remove(columnsName.Length - 1, 1);

        //        columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

        //        var sqlCommand = $"INSERT INTO Employee({columnsName}) VALUES({columnsParam})";

        //        var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);
        //        return rowsEffects;
        //    }

        //    /// <summary>
        //    /// Xóa nhân viên
        //    /// </summary>
        //    /// <param name="employeeId"></param>
        //    /// <returns></returns>
        //    /// CreatedBy: DT. Trinh
        //    public int Delete(Guid employeeId)
        //    {
        //        //Truy cập vào database
        //        //1. Khởi tạo thông tin kết nối database
        //        var connectionString = "Host= 47.241.69.179;" +
        //            "Database = MISA.CukCuk_Demo_NVMANH;" +
        //            "User Id = dev;" +
        //            "Password = 12345678;";


        //        //2. Khởi tạo đối tượng kết nối với database
        //        IDbConnection dbConnection = new MySqlConnection(connectionString);

        //        var res = dbConnection.Execute("Proc_DeleteEmployeeById", new { EmployeeId = employeeId }, commandType: CommandType.StoredProcedure);//chỉ cần tìm được 1 thằng trùng với employeeCode nên dùng FirstOrDefault
        //        return res;
        //    }

        //    /// <summary>
        //    /// Lấy ds nhân viên
        //    /// </summary>
        //    /// <returns></returns>
        //    /// CreatedBy: DT. Trinh
        //    public List<Employee> Get()
        //    {
        //        //Truy cập vào database
        //        //1. Khởi tạo thông tin kết nối database
        //        var connectionString = "Host= 47.241.69.179;" +
        //            "Database = MISA.CukCuk_Demo_NVMANH;" +
        //            "User Id = dev;" +
        //            "Password = 12345678;";
        //        //2. Khởi tạo đối tượng kết nối với database
        //        IDbConnection dbConnection = new MySqlConnection(connectionString);

        //        //3. Lấy dữ liệu
        //        var sqlCommand = "SELECT * FROM Employee";
        //        var employees = dbConnection.Query<object>(sqlCommand);
        //        return (List<Employee>)employees;
        //    }

        //    /// <summary>
        //    /// Lấy ds nhân viên theo ID
        //    /// </summary>
        //    /// <param name="employeeId"></param>
        //    /// <returns></returns>
        //    /// CreatedBy: DT.Trinh
        //    public Employee GetById(Guid employeeId)
        //    {
        //        //Truy cập vào database
        //        //1. Khởi tạo thông tin kết nối database
        //        var connectionString = "Host= 47.241.69.179;" +
        //            "Database = MISA.CukCuk_Demo_NVMANH;" +
        //            "User Id = dev;" +
        //            "Password = 12345678;";

        //        //2. Khởi tạo đối tượng kết nối với database
        //        IDbConnection dbConnection = new MySqlConnection(connectionString);

        //        //3. Lấy dữ liệu
        //        var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId= @EmployeeIdParam";

        //        //De trach loi SQL Injection           
        //        DynamicParameters parameters = new DynamicParameters();

        //        parameters.Add("@EmployeeIdParam", employeeId);

        //        var employee = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);
        //        return employee;


        //    }

        //    /// <summary>
        //    /// Sửa nhân viên
        //    /// </summary>
        //    /// <param name="employee"></param>
        //    /// <param name="employeeId"></param>
        //    /// <returns></returns>
        //    /// CreatedBy: DT.Trinh
        //    public int Update(Employee employee, Guid employeeId)
        //    {
        //        //Truy cập vào database
        //        //1. Khởi tạo thông tin kết nối database
        //        var connectionString = "Host= 47.241.69.179;" +
        //            "Database = MISA.CukCuk_Demo_NVMANH;" +
        //            "User Id = dev;" +
        //            "Password = 12345678;";
        //        //2. Khởi tạo đối tượng kết nối với database
        //        IDbConnection dbConnection = new MySqlConnection(connectionString);

        //        //Khai báo dyanamicParam:
        //        var dyanamicParam = new DynamicParameters();

        //        //3. Thêm dữ liệu vào trong database
        //        var columnsUpadateParam = string.Empty;

        //        //Đọc từng property của object:         
        //        var properties = employee.GetType().GetProperties();
        //        foreach (var prop in properties)
        //        {
        //            //Lấy tên của prop:
        //            var propName = prop.Name;

        //            //Lấy value của prop
        //            var propValue = prop.GetValue(employee);

        //            //Lấy kiểu dữ liệu của prop
        //            var propType = prop.PropertyType;

        //            //Thêm param tương ứng với mỗi property của đối tượng
        //            dyanamicParam.Add($"@{propName}", propValue);

        //            columnsUpadateParam += $"{propName} = '@{ propName}' ,";

        //        }

        //        columnsUpadateParam = columnsUpadateParam.Remove(columnsUpadateParam.Length - 1, 1);

        //        var sqlCommand = $"UPDATE Employee SET {columnsUpadateParam} WHERE EmployeeId = @employeeId";
        //        dyanamicParam.Add("@employeeId", employeeId);

        //        var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);
        //        return rowsEffects;
        //    }

        //    /// <summary>
        //    /// Lấy mã nhân viên để kiểm tra trùng lặp
        //    /// </summary>
        //    /// <param name="employeeCode"></param>
        //    /// <returns></returns>
        //    public Employee GetByCode(string employeeCode)
        //    {
        //        var connectionString = "Host= 47.241.69.179;" +
        //          "Database = MISA.CukCuk_Demo_NVMANH;" +
        //          "User Id = dev;" +
        //          "Password = 12345678;";

        //        //2. Khởi tạo đối tượng kết nối với database
        //        var dbConnection = new MySqlConnection(connectionString);
        //        var res = dbConnection.Query<Employee>("Proc_GetEmployeeByCode", new { EmployeeCode = employeeCode }, commandType: CommandType.StoredProcedure).FirstOrDefault();//chỉ cần tìm được 1 thằng trùng với cusCode nên dùng FirstOrDefault
        //        return res;
        //    }
    }
}
