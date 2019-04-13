using System;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels.CityViewModels
{
    /// <summary>
    /// City ViewModel fo View
    /// </summary>
    public class CityViewModel : ViewModelBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Country identifier
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string CountryName { get; set; }
    }
}