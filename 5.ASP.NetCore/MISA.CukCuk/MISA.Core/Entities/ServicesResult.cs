using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        public object Data { get; set; }

        public string Messager { get; set; }

        public bool IsValid { get; set; } = true;
    }
}
