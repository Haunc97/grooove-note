using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalNotes.Service;
using PersonalNotesAPI.Extensions;
using PersonalNotesAPI.Model.Models;
using PersonalNotesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalNotesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        INotebookService _notebookService;
        INoteService _noteService;
        public NotebookController(INotebookService _notebookService, INoteService _noteService)
        {
            this._notebookService = _notebookService;
            this._noteService = _noteService;
        }
        // GET: api/Todo
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<NotebookVM>> GetTodoItems()
        {
            var listNotebook = _notebookService.GetAll();           
            return Mapper.Map<List<NotebookVM>>(listNotebook);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<NotebookVM> Get(int id)
        {
            var noteBook = _notebookService.GetById(id);
            return Mapper.Map<NotebookVM>(noteBook);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<NotebookVM> Post([FromBody]NotebookVM data)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            else
            {
                Notebook newNotebook = new Notebook();
                newNotebook.UpdateNotebook(data);
                _notebookService.Add(newNotebook);
                _notebookService.Save();
                return Mapper.Map<NotebookVM>(newNotebook);
            }           
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<NotebookVM> Put(int id, [FromBody]NotebookVM data)
        {
            if(id != data.Id || !ModelState.IsValid)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            var notebook = _notebookService.GetById(id);
            notebook.UpdateNotebook(data);
            notebook.UpdatedOn = DateTime.Now;
            _notebookService.Update(notebook);
            _notebookService.Save();

            return Mapper.Map<NotebookVM>(notebook);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            else
            {
                _notebookService.Delete(id);
                _noteService.SoftDeleteMulti(x => x.NotebookId == id);
                _notebookService.Save();
                return new StatusCodeResult((int)HttpStatusCode.OK);
            }
        }
    }
}
