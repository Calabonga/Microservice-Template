using System;

namespace Calabonga.AspNetCore.Micro.Entities.Base
{
    public interface IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        Guid Id { get; set; }
    }
}