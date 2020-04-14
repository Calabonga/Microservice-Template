using System.Collections.Generic;

namespace Calabonga.Microservice.Module.Core
{
    /// <summary>
    /// Static data container
    /// </summary>
    public static partial class AppData
    {
        /// <summary>
        /// Current service name
        /// </summary>
        public const string ServiceName = "Module";

        /// <summary>
        /// "SystemAdministrator"
        /// </summary>
        public const string SystemAdministratorRoleName = "Administrator";

        /// <summary>
        /// "BusinessOwner"
        /// </summary>
        public const string ManagerRoleName = "Manager";

        /// <summary>
        /// Roles
        /// </summary>
        public static IEnumerable<string> Roles
        {
            get
            {
                yield return SystemAdministratorRoleName;
                yield return ManagerRoleName;
            }
        }

        /// <summary>
        /// IdentityServer4 path
        /// </summary>
        public const string AuthUrl = "/auth";
    }
}
