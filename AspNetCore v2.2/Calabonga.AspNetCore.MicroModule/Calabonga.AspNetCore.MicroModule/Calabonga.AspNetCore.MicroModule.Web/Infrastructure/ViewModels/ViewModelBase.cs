using System;
using Calabonga.AspNetCore.MicroModule.Models.Base;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Factories.Base;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.ViewModels
{
    /// <summary>
    /// ViewModelBase for WritableController
    /// </summary>
    public class ViewModelBase : IViewModel, IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
