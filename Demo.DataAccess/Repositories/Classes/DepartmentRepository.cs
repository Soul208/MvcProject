using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Moodels.DepartmentModel;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext dbContext) :GenericRepository<Department>(dbContext), IDepartmentRepository
    {
       

    }
}
