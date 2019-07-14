using PersonalNotesAPI.Data.Infrastructure;
using PersonalNotesAPI.Model.Models;

namespace PersonalNotesAPI.Data.Repositories
{
    public interface INotebookRepos : IRepository<Notebook>
    {
    }

    public class NotebookRepos : RepositoryBase<Notebook>, INotebookRepos
    {
        public NotebookRepos(PersonalNotesDbContext dbContext, IUserResolverService userResolverService) : base(dbContext, userResolverService)
        {

        }
    }
}
