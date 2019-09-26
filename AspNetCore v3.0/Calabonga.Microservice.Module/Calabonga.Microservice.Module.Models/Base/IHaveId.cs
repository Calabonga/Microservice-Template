using System;

namespace Calabonga.Microservice.Module.Models.Base
{
    public interface IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        Guid Id { get; set; }
    }
}