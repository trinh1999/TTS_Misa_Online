using MISA.Core.Entities;
using MISA.Core.Interfaces.Reponsitory;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeServices : BaseServices, IEmployeeServices
    {
        IEmployeeReponsitory _employeeRepository;
        ServiceResult _serviceResult;
        public EmployeeServices(IBaseReponsitory baseReponsitory) :base(baseReponsitory)
        {
            _serviceResult = new ServiceResult();
            //_employeeRepository = employeeReponsitory;

        }
        protected override bool ValidateCustomer<MISAEntity>(MISAEntity entity)
        {
            return base.ValidateCustomer(entity);
        }
        //public ServiceResult Add(Employee employee)
        //{
        //    return base.Add<Employee>(employee);
        //}

        //public ServiceResult Delete(Guid employeeId)
        //{
        //    throw new NotImplementedException();
        //}

        //public ServiceResult GetEmployee()
        //{
        //    throw new NotImplementedException();
        //}

        //public ServiceResult GetEmployeeById(Guid employeeId)
        //{
        //    throw new NotImplementedException();
        //}

        //public ServiceResult Update(Employee employee, Guid employeeId)
        //{
        //    return base.Update<Employee>(employee, employeeId);
        //}
    }
}
