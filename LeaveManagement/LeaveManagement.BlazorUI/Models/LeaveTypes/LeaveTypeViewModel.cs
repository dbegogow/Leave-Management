namespace LeaveManagement.BlazorUI.Models.LeaveTypes;

using System.ComponentModel.DataAnnotations;

public class LeaveTypeViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Default Number of Days")]
    public int DefaultDays { get; set; }
}
