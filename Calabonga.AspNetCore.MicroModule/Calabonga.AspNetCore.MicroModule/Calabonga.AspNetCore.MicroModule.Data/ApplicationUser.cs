using System;
using Calabonga.AspNetCore.MicroModule.Models.Base;

namespace Calabonga.AspNetCore.MicroModule.Data
{
    /// <summary>
    /// Default user for application.
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser: Identity
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Additional email for subscriptions
        /// </summary>
        public string AdditionalEmails { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email confirmed
        /// </summary>
        public string EmailConfirmed { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumberConfirmed { get; set; }
    }
}