using $ext_projectname$.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace $safeprojectname$
{
    /// <summary>
    /// Abstraction for Database (EntityFramework)
    /// </summary>
    public interface IApplicationDbContext
    {
        #region System

        DbSet<Log> Logs { get; set; }

        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        #endregion
    }
}