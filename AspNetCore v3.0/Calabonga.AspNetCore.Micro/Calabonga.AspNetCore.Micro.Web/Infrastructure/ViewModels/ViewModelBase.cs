using System;
using Calabonga.AspNetCore.Micro.Models.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Factories.Base;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels
{
    /// <summary>
    /// ViewModelBase for WritableController
    /// </summary>
    public class ViewModelBase: IViewModel, IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
