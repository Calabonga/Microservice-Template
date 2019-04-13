using System;

namespace Calabonga.AspNetCore.Micro.Models.Base
{
    public interface IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        Guid Id { get; set; }
    }
}