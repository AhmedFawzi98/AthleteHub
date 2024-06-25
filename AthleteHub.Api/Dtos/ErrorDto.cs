namespace AthleteHub.Api.Dtos
{
    public class ErrorDto
    {
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
