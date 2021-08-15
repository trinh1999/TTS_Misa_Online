using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Reponsitory;
using MISA.Core.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeServices _employeeService;
        IEmployeeReponsitory _employeeRepository;
        public EmployeeController(IEmployeeServices employeeServices, IEmployeeReponsitory employeeRepository)
        {
            _employeeService = employeeServices;
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            try
            {
                var serviceResult = _employeeService.Get<Employee>();
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


        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(Guid employeeId)
        {
            try
            {
                var serviceResult = _employeeService.GetById<Employee>(employeeId);
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

      
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                employee.EmployeeId = Guid.NewGuid();
                //4. Trả về cho client
                var serviceResult = _employeeService.Add<Employee>(employee);
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
        /// xóa nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployeeById(Guid employeeId)
        {
            try
            {

                //4. Trả về cho client
                var serviceResult = _employeeService.Delete<Employee>(employeeId);
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
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(Guid employeeId, Employee employee)
        {
            try
            {

                //4. Trả về cho client
                var serviceResult = _employeeService.Update(employee, employeeId);
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
