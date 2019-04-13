namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels.CountryViewModels
{
    /// <summary>
    /// Country View model
    /// </summary>
    public class CountryViewModel : ViewModelBase
    {
        /// <summary>
        /// Country name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Two symbol code
        /// </summary>
        public string Code2 { get; set; }

        /// <summary>
        /// Three symbol code
        /// </summary>
        public string Code3 { get; set; }
    }
}
