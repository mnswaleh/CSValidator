using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSValidator
{
    class Employee
    {
        [Key]
        public string Id { get; set; }
        public string Manager { get; set; }
        public int Salary { get; set; }
    }
}
