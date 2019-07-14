using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalNotes.Service;
using PersonalNotesAPI.Extensions;
using PersonalNotesAPI.Filters;
using PersonalNotesAPI.Model.Models;
using PersonalNotesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalNotesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteService _noteService;
        public NoteController(INoteService _noteService)
        {
            this._noteService = _noteService;
        }
        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<NoteVM>> GetTodoItems()
        {
            var listNote = _noteService.GetAll();
            return Mapper.Map<List<NoteVM>>(listNote);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<NoteVM> Get(int id)
        {
            var note = _noteService.GetById(id);
            return Mapper.Map<NoteVM>(note);
        }
        // POST api/<controller>
        [ChromeVersionFilter]
        [HttpPost]
        public ActionResult<NoteVM> Post([FromBody]NoteVM data)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            else
            {
                Note newNote = new Note();
                newNote.UpdateNote(data);
                _noteService.Add(newNote);
                _noteService.Save();
                return Mapper.Map<NoteVM>(newNote);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<NoteVM> Put(int id, [FromBody]NoteVM data)
        {
            if (id != (int)data.Id || !ModelState.IsValid)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            var note = _noteService.GetById(id);
            note.UpdateNote(data);
            _noteService.Update(note);
            _noteService.Save();

            return Mapper.Map<NoteVM>(note);
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
                _noteService.Delete(id);
                _noteService.Save();
                return new StatusCodeResult((int)HttpStatusCode.OK);
            }
        }
    }
}
