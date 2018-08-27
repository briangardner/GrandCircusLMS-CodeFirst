using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using GrandCircusLMS.Data.Interfaces;
using GrandCircusLMS.Data.Maps;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data
{
    public class GrandCircusLmsContext : DbContext, IGrandCircusLmsContext
    {
        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        public GrandCircusLmsContext(): base("GrandCircusLMS")
        {
            //Drop the database and recreate on each run
            //Database.SetInitializer(new DropCreateDatabaseAlways<GrandCircusLmsContext>());
            // Create the DB if it doesn't exist.  
            //Database.SetInitializer(new CreateDatabaseIfNotExists<GrandCircusLmsContext>());
            //Will Drop and recreate if model changes.
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GrandCircusLmsContext>());
            //Custom Initializer
            Database.SetInitializer(new GrandCircusLmsInitializer());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new EnrollmentMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new InstructorMap());
            modelBuilder.Configurations.Add(new ProgramManagerMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            base.OnModelCreating(modelBuilder);
            
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }


        public void BeginTransaction()
        {
            _objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (_objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }
            _objectContext.Connection.Open();
            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public int Commit()
        {
            var saveChanges = SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public Task<int> CommitAsync()
        {
            var saveChangesAsync = SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                {
                    _objectContext.Connection.Close();
                }

                _objectContext?.Dispose();
                _transaction?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
