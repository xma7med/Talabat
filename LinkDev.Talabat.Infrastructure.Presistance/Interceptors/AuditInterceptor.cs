using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Presistance.Interceptors
{
	// To se Audit Data  
	// Inherit : SaveChangesInterceptor
	// We Need ILoggedInService To Know the Current User
	// Service - Go to Wep App (API) WHY?? So I have Access to IHttpContextAccssor To make the sevice
	// Contract Service In App.Abstraction

	//Allow DI for ISaveChangesInterceptor to give u   BaseEntityAuditableInterceptor

	// Then Build The Interceptor 
	// Override SaveChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
	// private method
	// Call method in SaveChanges 
	public class AuditInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public AuditInterceptor(ILoggedInUserService loggedInUserService)
        {
			_loggedInUserService = loggedInUserService;
		}

		// non async 
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities(eventData.Context);

			return base.SavingChanges(eventData, result);
		}
        //async 

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
			UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        


		private void UpdateEntities(DbContext? dbContext)
		{
			if (dbContext == null) return;

            //var entries = dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>() // Must any Entity inherit from BaseAuditableEntity Be Authenticated But The Problem is key <int> , So we make IBaseAuditableEntity
            var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>() // Must any Entity inherit from BaseAuditableEntity Be Authenticated 
				.Where(entity => entity.State is EntityState.Added or EntityState.Modified);


            foreach (var entry in entries)
			{
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;
				}
				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;
			}
		}
	}
}
