using Microsoft.AspNetCore.Mvc.Filters;
using PersonalNotes.Service;
using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalNotesAPI.Validations
{
  
    public class NotebookForeignAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _notebookService = (INotebookService)validationContext.GetService(typeof(INotebookService));
            int notebookID = Convert.ToInt32(value);
            if (notebookID == 0)
                return new ValidationResult("Foreign key can not be null");
            else
            {
                var obj = _notebookService.GetById(notebookID);
                if (obj == null)
                   return new ValidationResult("Foreign key is not exist");
            }
            return ValidationResult.Success;
        }

    }
}
