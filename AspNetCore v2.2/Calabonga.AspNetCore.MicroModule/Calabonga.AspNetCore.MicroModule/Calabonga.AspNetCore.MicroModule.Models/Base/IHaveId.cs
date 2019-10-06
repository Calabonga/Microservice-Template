using System;

namespace Calabonga.AspNetCore.MicroModule.Models.Base
{
    public interface IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        Guid Id { get; set; }
    }
}