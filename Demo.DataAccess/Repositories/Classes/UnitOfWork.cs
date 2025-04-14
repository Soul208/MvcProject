using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IunitOfWork , IDisposable
    {
        private readonly Lazy< IDepartmentRepository> _DepartmentRepository;
        private readonly Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            this._dbContext = dbContext;
            _DepartmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _EmployeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext)); 

        }
        public IEmployeeRepository EmployeeRepository => _EmployeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository.Value ;

        public int SaveChanges() =>  _dbContext.SaveChanges();

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
