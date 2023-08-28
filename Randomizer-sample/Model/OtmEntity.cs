using System.ComponentModel.DataAnnotations;

namespace OTM_sample.Model;

public class OtmEntity
{
    [Key] public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}