using System.ComponentModel.DataAnnotations;

namespace Calabonga.Microservices.Core.Tests
{
    public enum TestType
    {
        [Display(Name = "Не определено")]
        None,
        
        [DisplayNames("Значение","Значение 2","Значение3")]
        Value,
        
        [DisplayNames("Простой1","Простой2","Простой3")]
        [Display(Name = "Простой")]
        Simple,
        
        [Display(Name = "Множественный")]
        Multiple,
        
        [Display(Name = "Другие")]
        Other
    }
}