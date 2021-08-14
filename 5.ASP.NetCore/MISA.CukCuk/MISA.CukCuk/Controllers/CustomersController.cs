using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MISA.Core.Interfaces.Services;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerServices _customerService;
        public CustomersController(ICustomerServices customerServices)
        {
            _customerService = customerServices;
        }
        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                var serviceResult = _customerService.GetCustomers();
                //4. Trả về cho client
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, erroObj);
            }

        }


        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            try
            {
                var serviceResult = _customerService.GetCustomerById(customerId);
                //4. Trả về cho client
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
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
              
                //4. Trả về cho client
                var serviceResult = _customerService.Add(customer);
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
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

                //4. Trả về cho client
                var serviceResult = _customerService.Delete(customerId);
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
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

                //4. Trả về cho client
                var serviceResult = _customerService.Update(customer,customerId);
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {
                var erroObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErroMsg,
                    errorCode = "misa-001",
                    moreInfo = @"https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, erroObj);
            }



        }

    }
}
