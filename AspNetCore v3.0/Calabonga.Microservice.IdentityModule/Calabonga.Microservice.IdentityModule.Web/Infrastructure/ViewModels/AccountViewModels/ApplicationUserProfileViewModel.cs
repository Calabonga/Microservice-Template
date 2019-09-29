using System;
using System.Collections.Generic;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.AccountViewModels
{
    /// <summary>
    /// Application User Profile
    /// </summary>
    public class ApplicationUserProfileViewModel
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// Returns User email verified
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Returns Preferred UserName
        /// </summary>
        public string PreferredUserName { get; set; }

        /// <summary>
        /// User PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Returns is PhoneNumber Verified
        /// </summary>
        public bool PhoneNumberVerified { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Additional Emails for subscription
        /// </summary>
        public string AdditionalEmails { get; set; }
    }
}