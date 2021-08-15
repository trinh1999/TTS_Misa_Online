using Dapper;
using MISA.Core.Interfaces.Reponsitory;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Reponsitory
{
    public class BaseReponsitory : IBaseReponsitory
    {
        public BaseReponsitory(IBaseServices baseServices)
        {
        }

        public int Add<MISAEntity>(MISAEntity entity)
        {
            var className = typeof(MISAEntity).Name;
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
            var properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(entity);

                if (propName == $"{className}Id" && prop.PropertyType == typeof(Guid))
                {
                    propValue = Guid.NewGuid();
                }

                //Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi property của đối tượng
                dyanamicParam.Add($"@{propName}", propValue);

                columnsName += $"{propName},";

                columnsParam += $"@{propName},";

            }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);

            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
           

            var sqlCommand = $"INSERT INTO {className}({columnsName}) VALUES({columnsParam})";

            var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);
            return rowsEffects;
        }

        public int Delete<MISAEntity>(Guid entityId)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";


            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var res = dbConnection.Execute("Proc_DeleteEntityById", new { EntityId = entityId }, commandType: CommandType.StoredProcedure);//chỉ cần tìm được 1 thằng trùng với cusCode nên dùng FirstOrDefault
            return res;
        }

        public List<MISAEntity> GetAll<MISAEntity>()
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
            var sqlCommand = "SELECT * FROM {className}";
            var res = dbConnection.Query<object>(sqlCommand);
            return (List<MISAEntity>)res;
        }

        public int Update<MISAEntity>(MISAEntity entity, Guid entityId)
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
            var properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(entity);

                //Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi property của đối tượng
                dyanamicParam.Add($"@{propName}", propValue);

                columnsUpadateParam += $"{propName} = '@{ propName}' ,";

            }

            columnsUpadateParam = columnsUpadateParam.Remove(columnsUpadateParam.Length - 1, 1);

            var sqlCommand = $"UPDATE className SET {columnsUpadateParam} WHERE EntityId = @entityId";
            dyanamicParam.Add("@entityId", entity);

            var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);
            return rowsEffects;
        }
    }
}
