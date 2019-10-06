using System;
using System.Collections.Generic;

namespace $safeprojectname$.Infrastructure.ViewModels.AccountViewModels
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
        /// User PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Position Name
        /// </summary>
        public string PositionName { get; set; }
    }
}