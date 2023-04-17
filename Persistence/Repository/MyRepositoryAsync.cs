using Application.Interfaces.IRepositories;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly SICAContext dbContext;

        public MyRepositoryAsync(SICAContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
