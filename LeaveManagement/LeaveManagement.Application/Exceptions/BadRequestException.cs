namespace LeaveManagement.Application.Exceptions;

using FluentValidation.Results;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, ValidationResult validationResult)
        : base(message)
        => this.ValidationErrors = validationResult
            .Errors
            .Select(e => e.ErrorMessage)
            .ToList();


    public IReadOnlyCollection<string>? ValidationErrors { get; }
}
