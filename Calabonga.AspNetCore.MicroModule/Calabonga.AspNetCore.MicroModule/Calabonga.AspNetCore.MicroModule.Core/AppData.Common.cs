using System.Collections.Generic;

namespace Calabonga.AspNetCore.MicroModule.Core
{
    /// <summary>
    /// Static data container
    /// </summary>
    public static partial class AppData
    {
        /// <summary>
        /// "SystemAdministrator"
        /// </summary>
        public const string SystemAdministratorRoleName = "Administrator";

        /// <summary>
        /// "BusinessOwner"
        /// </summary>
        public const string CompanyRoleName = "Company";

        /// <summary>
        /// Roles
        /// </summary>
        public static IEnumerable<string> Roles
        {
            get
            {
                yield return SystemAdministratorRoleName;
                yield return CompanyRoleName;
            }
        }

        /// <summary>
        /// IdentityServer4 path
        /// </summary>
        public const string AuthUrl = "/auth";
    }
}
