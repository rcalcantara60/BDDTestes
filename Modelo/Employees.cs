﻿using System;
using System.Collections.Generic;

namespace Modelo
{
    public partial class Employees
    {
        public Employees()
        {
            Departments = new HashSet<Departments>();
            InverseManager = new HashSet<Employees>();
            JobHistory = new HashSet<JobHistory>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string JobId { get; set; }
        public decimal? Salary { get; set; }
        public decimal? CommissionPct { get; set; }
        public int? ManagerId { get; set; }
        public byte? DepartmentId { get; set; }

        public virtual Departments Department { get; set; }
        public virtual Jobs Job { get; set; }
        public virtual Employees Manager { get; set; }
        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<Employees> InverseManager { get; set; }
        public virtual ICollection<JobHistory> JobHistory { get; set; }
    }
}
