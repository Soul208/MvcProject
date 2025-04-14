﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IunitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get;}
        public IDepartmentRepository DepartmentRepository { get;}

        int SaveChanges();
    }
}
