using PersonalNotesAPI.Data.Infrastructure;
using PersonalNotesAPI.Data.Repositories;
using PersonalNotesAPI.Model.Models;
using System.Collections.Generic;

namespace PersonalNotes.Service
{

    public interface INotebookService
    {
        Notebook Add(Notebook notebook);

        void Update(Notebook notebook);

        void Delete(int id);

        IEnumerable<Notebook> GetAll();

        IEnumerable<Notebook> GetAllByParentId(int parentId);

        Notebook GetById(int id);

        void Save();     
    }

    public class NotebookService : INotebookService
    {
        private INotebookRepos _notebookRepos;
        private IUnitOfWork _unitOfWork;    

        public NotebookService(INotebookRepos notebookRepos, IUnitOfWork unitOfWork)
        {
            this._notebookRepos = notebookRepos;
            this._unitOfWork = unitOfWork;            
        }

        public Notebook Add(Notebook notebook)
        {
            return _notebookRepos.Add(notebook);
        }

        public void Delete(int id)
        {
            var notebook = _notebookRepos.GetSingleById(id);
            notebook.Deleted = true;
            _notebookRepos.Update(notebook);
        }

        public IEnumerable<Notebook> GetAll()
        {
            return _notebookRepos.GetMulti(x=>x.Deleted==false);
        }

        public IEnumerable<Notebook> GetAllByParentId(int parentId)
        {
            return _notebookRepos.GetMulti(x => x.ParentId == parentId);
        }

        public Notebook GetById(int id)
        {
            return _notebookRepos.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Notebook notebook)
        {
            _notebookRepos.Update(notebook);
        }
    }

}
