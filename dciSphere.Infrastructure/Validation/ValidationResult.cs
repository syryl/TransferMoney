using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Validation;
public class ValidationResultDto
{
    public ValidationResultDto(List<ValidationFailure> validationResult)
    {
        foreach (var error in validationResult)
        {
            Errors.Add(new ValidationError(error.PropertyName, error.ErrorMessage));
        }
    }
    public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
}
