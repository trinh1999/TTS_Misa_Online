using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Reponsitory;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Reponsitory
{
    public class CustomerReponsitory : ICustomerReponsitory
    {
        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public int Add(Customer customer)
        {
            //Khởi tạo Id ms cho đối tượng
            customer.CustomerId = Guid.NewGuid();
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";

            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //Khai báo dyanamicParam:
            var dyanamicParam = new DynamicParameters();

            //3. Thêm dữ liệu vào trong database
            var columnsName = string.Empty;

            var columnsParam = string.Empty;

            //Đọc từng property của object:
            var properties = customer.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(customer);

                //Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi property của đối tượng
                dyanamicParam.Add($"@{propName}", propValue);

                columnsName += $"{propName},";

                columnsParam += $"@{propName},";

            }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);

            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

            var sqlCommand = $"INSERT INTO Customer({columnsName}) VALUES({columnsParam})";

            var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);
            return rowsEffects;
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public int Delete(Guid customerId)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";


            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var res = dbConnection.Execute("Proc_DeleteCustomerById", new { CustomerId = customerId }, commandType: CommandType.StoredProcedure);//chỉ cần tìm được 1 thằng trùng với cusCode nên dùng FirstOrDefault
            return res;
        }

        /// <summary>
        /// Lấy all khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public List<Customer> Get()
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Customer";
            var customers = dbConnection.Query<object>(sqlCommand);
            return (List<Customer>)customers;
        }

        /// <summary>
        /// Lấy khách hàng theo ID
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public Customer GetById(Guid customerId)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";

            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId= @CustomerIdParam";

            //De trach loi SQL Injection           
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CustomerIdParam", customerId);

            var customer = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand, param: parameters);
            return customer;


        }

        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public int Update(Customer customer, Guid customerId)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //Khai báo dyanamicParam:
            var dyanamicParam = new DynamicParameters();

            //3. Thêm dữ liệu vào trong database
            var columnsUpadateParam = string.Empty;

            //Đọc từng property của object:         
            var properties = customer.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(customer);

                //Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi property của đối tượng
                dyanamicParam.Add($"@{propName}", propValue);

                columnsUpadateParam += $"{propName} = '@{ propName}' ,";

            }

            columnsUpadateParam = columnsUpadateParam.Remove(columnsUpadateParam.Length - 1, 1);

            var sqlCommand = $"UPDATE Customer SET {columnsUpadateParam} WHERE CustomerId = @customerId";
            dyanamicParam.Add("@customerId", customerId);

            var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);
            return rowsEffects;
        }

        /// <summary>
        /// Lấy mã khách hàng để kiểm tra trùng lặp
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public Customer GetByCode(string customerCode)
        {
            var connectionString = "Host= 47.241.69.179;" +
              "Database = MISA.CukCuk_Demo_NVMANH;" +
              "User Id = dev;" +
              "Password = 12345678;";

            //2. Khởi tạo đối tượng kết nối với database
            var dbConnection = new MySqlConnection(connectionString);
            var res = dbConnection.Query<Customer>("Proc_GetCustomerByCode", new { CustomerCode = customerCode }, commandType: CommandType.StoredProcedure).FirstOrDefault();//chỉ cần tìm được 1 thằng trùng với cusCode nên dùng FirstOrDefault
            return res;
        }
    }
}
