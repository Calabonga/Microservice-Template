using System;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels.ConfigurationViewModels
{
    /// <summary>
    /// ViewModel for Configuration Create
    /// </summary>
    public class ConfigurationCreateViewModel
    {
        /// <summary>
        /// Question answer limited time 180 seconds
        /// </summary>
        public TimeSpan TimeLimit { get; set; } = TimeSpan.FromSeconds(180);

        /// <summary>
        /// Total try count by default equals 10
        /// </summary>
        public int TryLimit { get; set; } = 10;

        /// <summary>
        /// Question score for rating calculation
        /// </summary>
        public int Score { get; set; } = 100;
    }
}
