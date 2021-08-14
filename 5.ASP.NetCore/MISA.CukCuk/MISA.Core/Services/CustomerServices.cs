using MISA.Core.Entities;
using MISA.Core.Interfaces.Reponsitory;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerServices : ICustomerServices
    {
        ICustomerReponsitory _customerRepository;
        public CustomerServices(ICustomerReponsitory customerReponsitory)
        {
            _customerRepository = customerReponsitory;

        }
        ServiceResult _serviceResult;
        public CustomerServices()
        {
            _serviceResult = new ServiceResult();
        }

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public ServiceResult Add(Customer customer)
        {
            //Xử lý nghiệp vụ
            //Kiểm tra thông tin khách hàng đã hợp lệ hay chưa?
            //1. Mã KH bắt buộc
            if (customer.CustomerCode == "" || customer.CustomerCode == null)
            {
                var erroObj = new
                {
                    userMsg = Properties.Resources.ErroCustomerCode,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = erroObj;
                return _serviceResult;
            }
            //2. Email phải đúng định dạng
            var pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var isMatch = Regex.IsMatch(customer.Email, pattern, RegexOptions.IgnoreCase);
            if (isMatch == false)
            {
                var erroObj = new
                {
                    userMsg = Properties.Resources.ErroCustomerEmail,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = erroObj;
                return _serviceResult;
            }
            //3.Check trùng lặp
            var res = _customerRepository.GetByCode(customer.CustomerCode);
            if(res != null){
                var erroObj = new
                {
                    userMsg = Properties.Resources.ErroCustomerDuplicate,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = erroObj;
                return _serviceResult;
            }

            //Tương tác kết nối Database:
            _serviceResult.Data = _customerRepository.Add(customer);
            return _serviceResult;
        }

        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public ServiceResult Update(Customer customer, Guid customerId)
        {
            _serviceResult.Data = _customerRepository.Update(customer, customerId);
            return _serviceResult;
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public ServiceResult Delete(Guid customerId)
        {
            _serviceResult.Data = _customerRepository.Delete(customerId);
            return _serviceResult;
        }

        /// <summary>
        /// Lấy all khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public ServiceResult GetCustomers()
        {
            _serviceResult.Data = _customerRepository.Get();
            return _serviceResult;
        }

        /// <summary>
        /// Lấy khách hàng theo ID
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public ServiceResult GetCustomerById(Guid customerId)
        {
            _serviceResult.Data = _customerRepository.GetById(customerId);
            return _serviceResult;
        }
    }
}
