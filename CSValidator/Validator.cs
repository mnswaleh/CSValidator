using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSValidator
{
    public class Validator
    {
        private readonly List<Employee> _employees;
        private bool _isvalid = true;
        public Validator(String dataFile)
        {
            try
            {
                _employees = File.ReadAllLines(dataFile)
                       .Select(x => x.Split(','))
                       .Select(dataRow => new Employee
                       {
                           Id = dataRow[0],
                           Manager = string.IsNullOrWhiteSpace(dataRow[1]) ? null : dataRow[1],
                           Salary = dataRow.Length == 2 ? int.Parse(dataRow[1]) : int.Parse(dataRow[2]),
                       }).ToList();

                if (_employees.Count(e => e.Manager == null) > 1)
                {
                    _isvalid = false;
                }

                _employees.ForEach(e =>
                {
                    if ((!_employees.Any(emp => emp.Id == e.Manager) && e.Manager != null) || _employees.Count(emp => emp.Id == e.Id) > 1)
                    {
                        _isvalid = false;
                    }
                });
            }
            catch (Exception e)
            {
                _isvalid = false;
            }
        }

        public string Budget(string manager)
        {
            if (!_isvalid)
            {
                return "Invalid CSV file!";
            }

            Employee employeeManager = _employees.Find(e => e.Id == manager);
            int managerBudget = employeeManager.Salary;
            List<Employee> employees = _employees.Where(e => e.Manager == employeeManager.Id).ToList();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    managerBudget += int.Parse(Budget(employee.Id));
                }
            }
            return managerBudget.ToString();
        }
    }
}
