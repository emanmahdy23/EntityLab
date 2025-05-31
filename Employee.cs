using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDay5
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
}
