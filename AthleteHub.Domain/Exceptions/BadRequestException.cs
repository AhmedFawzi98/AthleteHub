namespace AthleteHub.Domain.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("One or more registeration errors occurred.")
    {
        Errors = new List<ValidationError>();
    }

    public BadRequestException(IEnumerable<ValidationError> errors) : this()
    {
        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }

    public List<ValidationError> Errors { get; set; }
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
