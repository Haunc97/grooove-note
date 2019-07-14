using PersonalNotesAPI.Data.Infrastructure;
using PersonalNotesAPI.Data.Repositories;
using PersonalNotesAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PersonalNotes.Service
{
    public interface INoteService
    {
        Note Add(Note note);

        void Update(Note note);

        void Delete(int id);


        IEnumerable<Note> GetAll();

        Note GetById(int id);

        void SoftDeleteMulti(Expression<Func<Note, bool>> where);

        void Save();
        
    }

    public class NoteService : INoteService
    {
        private INoteRepos _noteRepos;
        private IUnitOfWork _unitOfWork;      

        public NoteService(INoteRepos noteRepos, IUnitOfWork unitOfWork)
        {
            this._noteRepos = noteRepos;
            this._unitOfWork = unitOfWork;            
        }

        public Note Add(Note note)
        {
            return _noteRepos.Add(note);
        }

        public void Delete(int id)
        {
            var note = _noteRepos.GetSingleById(id);
            note.Deleted = true;
            _noteRepos.Update(note);
        }
       
        public IEnumerable<Note> GetAll()
        {
            //return _noteRepos.GetMulti(x => x.Deleted == false);
            return _noteRepos.GetAll();
        }

        public Note GetById(int id)
        {
            return _noteRepos.GetSingleById(id);
        }

        public void Update(Note note)
        {
            _noteRepos.Update(note);
        }
        public void SoftDeleteMulti(Expression<Func<Note, bool>> where)
        {
            _noteRepos.SoftDeleteMulti(where);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
