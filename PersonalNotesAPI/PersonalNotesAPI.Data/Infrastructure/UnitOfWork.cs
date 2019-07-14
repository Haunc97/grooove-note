using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalNotesAPI.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private PersonalNotesDbContext dbContext;

        public UnitOfWork(PersonalNotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }

    }
}
