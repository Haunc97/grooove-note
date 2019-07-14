using AutoMapper;
using PersonalNotesAPI.Controllers;
using PersonalNotesAPI.Model.Models;
using PersonalNotesAPI.Models;

namespace PersonalNotesAPI
{
    public partial class Startup
    {
        public class OrganizationProfile : Profile
        {
            public OrganizationProfile()
            {
                CreateMap<Notebook, NotebookVM>();
                CreateMap<Note, NoteVM>();
            }
        }
        public void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<OrganizationProfile>();
            });
        }
    }
}
