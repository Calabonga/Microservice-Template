using System;

namespace Calabonga.AspNetCore.MicroModule.Models.Base
{
    /// <summary>
    /// Identifier
    /// </summary>
    public abstract class Identity : IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
