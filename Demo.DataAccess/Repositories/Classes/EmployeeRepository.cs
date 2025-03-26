﻿using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Moodels.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext dbContext): GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        
    }
}
