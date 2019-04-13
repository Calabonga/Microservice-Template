using System;
using Calabonga.AspNetCore.Micro.Models.Base;
using $safeprojectname$.Infrastructure.Factories.Base;

namespace $safeprojectname$.Infrastructure.ViewModels
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
