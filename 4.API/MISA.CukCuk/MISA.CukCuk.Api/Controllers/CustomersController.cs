using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCustomers(string name, int? age)
        {
            try
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

                //4. Trả về cho client
                if (customer.Count()>0){
                    var response = StatusCode(200, customers);
                    return response;
                }
                else{
                    return StatusCode(204, customers);
                }
                
            }
            catch(Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId= ""
                };
                return StatusCode(500, erroObj);
            }
           
        }


        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
        {
              try
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

           
                //4. Trả về cho client
                var response = StatusCode(200, customers);
                return response;
                
            }
            catch(Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId= ""
                };
                return StatusCode(500, erroObj);
            }
           

        }

        /// <summary>
        /// API thêm mới 1 bản ghi nhân viên
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertCustomer(Customer customer)
        {
             try
            {
            //Kiểm tra thông tin khách hàng đã hợp lệ hay chưa?
            //1. Mã KH bắt buộc
            if(customer.CustomerCode == "" || customer.CustomerCode == null)
            {
                 var erroObj = new
                {
                    userMsg = Properties.Resource.ErroCustomerCode,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return BadRequest(erroObj);
            }
            //2. Email phải đúng định dạng
            var pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";    
            var isMatch = Regex.IsMatch(customer.Email, pattern, RegexOptions.IgnoreCase);
            if(isMatch == false){
                var erroObj = new
                {
                    userMsg = Properties.Resource.ErroCustomerEmail,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return BadRequest(erroObj);
            }
        
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

            //4. Trả về cho client
            if(rowsEffects>0){
                return StatusCode(201, rowsEffects);
            }else{
                return StatusCode(204);
            }
                
            }
            catch(Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId= ""
                };
                return StatusCode(500, erroObj);
            }
          

        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomerById(Guid customerId)
        {
             try
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
            var sqlCommand = $"DELETE FROM Customer WHERE CustomerId= @CustomerIdParam";

            //De trach loi SQL Injection           
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CustomerIdParam", customerId);

            var customer = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand, param: parameters);

            //4. Trả về cho client
            var response = StatusCode(200, customer);
            return response;
                
            }
            catch(Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId= ""
                };
                return StatusCode(500, erroObj);
            }
            
           
        }

        /// <summary>
        /// Sửa 
        /// </summary>
        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomer(Guid customerId, Customer customer)
        {
             try
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

            //4. Trả về cho client
            var response = StatusCode(200, rowsEffects);
            return response;
                
            }
            catch(Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId= ""
                };
                return StatusCode(500, erroObj);
            }
            

           
        }

    }
}
