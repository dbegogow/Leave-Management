namespace LeaveManagement.Api.Models;

using Microsoft.AspNetCore.Mvc;

public class CustomProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
