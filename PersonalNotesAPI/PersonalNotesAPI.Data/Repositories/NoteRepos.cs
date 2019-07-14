using PersonalNotesAPI.Data.Infrastructure;
using PersonalNotesAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PersonalNotesAPI.Data.Repositories
{
    public interface INoteRepos : IRepository<Note>
    {
        void SoftDeleteMulti(Expression<Func<Note, bool>> where);
    }
    public class NoteRepos : RepositoryBase<Note>, INoteRepos
    {
        public NoteRepos(PersonalNotesDbContext dbContext, IUserResolverService userResolverService) : base(dbContext,userResolverService)
        {

        }

        public void SoftDeleteMulti(Expression<Func<Note, bool>> where)
        {
            IEnumerable<Note> notes = GetMulti(where);
            foreach (Note note in notes)
            {
                note.Deleted = true;
                Update(note);
            }
        }
    }
}
