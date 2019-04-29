using System;

namespace $safeprojectname$.Base
{
    /// <summary>
    /// Represents 'Audit-able' table from the Property Database
    /// </summary>
    public abstract class Auditable: Identity, IAuditable
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

    }
}
