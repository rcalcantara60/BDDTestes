using System;
using System.Collections.Generic;

namespace Modelo
{
    public partial class Departments
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
            JobHistory = new HashSet<JobHistory>();
        }

        public byte DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public byte? LocationId { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Employees Manager { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<JobHistory> JobHistory { get; set; }
    }
}
