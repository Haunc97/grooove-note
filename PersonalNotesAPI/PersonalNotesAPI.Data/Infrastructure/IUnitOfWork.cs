using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalNotesAPI.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
