using System.ComponentModel.DataAnnotations;

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
