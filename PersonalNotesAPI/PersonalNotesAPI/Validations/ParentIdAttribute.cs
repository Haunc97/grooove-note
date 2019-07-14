using PersonalNotes.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalNotesAPI.Validations
{
    public class ParentIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _notebookService = (INotebookService)validationContext.GetService(typeof(INotebookService));
            int parentID = Convert.ToInt32(value);
            if (parentID != 0)
            {
                var obj = _notebookService.GetById(parentID);
                if(obj==null)
                    return new ValidationResult("Parent ID is not exist.");
            }
            return ValidationResult.Success;
        }
    }
}
