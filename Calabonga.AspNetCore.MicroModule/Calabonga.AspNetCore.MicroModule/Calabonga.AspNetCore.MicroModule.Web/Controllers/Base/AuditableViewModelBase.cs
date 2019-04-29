using System;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.ViewModels;

namespace Calabonga.AspNetCore.MicroModule.Web.Controllers.Base
{
    /// <summary>
    /// Audit-able View Model Base
    /// </summary>
    public abstract class AuditableViewModelBase : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeletedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ArchivedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ArchivedBy { get; set; }
    }
}
