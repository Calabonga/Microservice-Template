using System;

namespace $safeprojectname$.Infrastructure.ViewModels.TimezoneViewModels
{
    /// <summary>
    /// Timezone ViewModel
    /// </summary>
    public class TimezoneViewModel : ViewModelBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Time zone offset
        /// </summary>
        public TimeSpan TimeZoneOffset { get; set; }
    }
}
