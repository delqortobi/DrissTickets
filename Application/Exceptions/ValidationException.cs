using FluentValidation.Results;

namespace Application.Exceptions;

public class ValidationException:ApplicationException
{
    public List<string> ValidationErrors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = new();

        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
    }
}