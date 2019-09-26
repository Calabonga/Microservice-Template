using System;
using Calabonga.Microservice.Module.Models.Base;
using Calabonga.Microservice.Module.Web.Infrastructure.Factories.Base;

namespace Calabonga.Microservice.Module.Web.Infrastructure.ViewModels
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
