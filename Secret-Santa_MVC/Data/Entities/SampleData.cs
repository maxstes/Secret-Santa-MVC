using Microsoft.CodeAnalysis.CodeFixes;
using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.Data.Entities
{
    public class SampleData : BaseEntity
    {
        public DateTime SamplingDate { get; set; }
        public SampleType SampleType { get; set; }
        public FixationType FixationType { get; set; }
        public string Localization { get; set; } = null!;
        public string LesionDescription { get; set; } = null!;
        public string? Anamnesis { get; set; }
    }
}
