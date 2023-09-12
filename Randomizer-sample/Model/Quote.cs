using System.ComponentModel.DataAnnotations;

namespace OTM_sample.Model;

public class Quote
{
    [Key] public Guid Id { get; set; }

    public string Text { get; set; } = string.Empty;
}