using System;

namespace Calabonga.AspNetCore.Micro.Entities.Base
{
    /// <summary>
    /// Identifier
    /// </summary>
    public abstract class Identity: IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
