using System;
using Calabonga.EntityFrameworkCore.Entities.Base;
using IViewModel = Calabonga.Microservice.Module.Web.Infrastructure.Factories.Base.IViewModel;

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
