using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSValidator
{
    public class Validator
    {
        private readonly List<Employee> _employees;
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
            }
            catch (Exception e)
            {
                throw new ArgumentException(message: "Invalid File!");
            }

            if (_employees.Count(e => e.Manager == null) > 1)
            {
                throw new ArgumentException(message: "Invalid File!");
            }

            _employees.ForEach(e =>
            {
                if (!_employees.Any(emp => emp.Id == e.Manager) && e.Manager != null)
                {
                    throw new ArgumentException(message: "Invalid File!");
                }
            });

            foreach (Employee employee in _employees)
            {
                HashSet<Employee> visited = new();
                if (FindCycle(employee, visited))
                {
                    throw new ArgumentException(message: "Invalid File!");
                }
            }
        }

        private bool FindCycle(Employee employee, HashSet<Employee> stackVisited)
        {
            Employee current = employee;
            if (stackVisited.Contains(current))
            {
                return true;
            }

            List<Employee> employees = _employees.Where(e => e.Manager == current.Id).ToList();
            if (employees != null)
            {
                foreach (Employee employeee in employees)
                {
                    HashSet<Employee> visited = new(stackVisited);
                    visited.Add(current);
                    if (FindCycle(employeee, visited))
                    {
                        return true;
                    }
                }
            }
            else
            {
                stackVisited.Add(current);
            }
            return false;
        }

        public int Budget(string manager)
        {
            Employee employeeManager = _employees.Find(e => e.Id == manager);
            int managerBudget = employeeManager.Salary;
            List<Employee> employees = _employees.Where(e => e.Manager == employeeManager.Id).ToList();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    managerBudget += Budget(employee.Id);
                }
            }
            return managerBudget;
        }
    }
}
