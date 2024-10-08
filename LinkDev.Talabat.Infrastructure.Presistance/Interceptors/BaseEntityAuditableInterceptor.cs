using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Presistance.Interceptors
{
	// Inherit : SaveChangesInterceptor
	// We Need ILoggedInService To Know the Current User
	// Service - Go to Wep App (API) WHY?? So I have Access to IHttpContextAccssor To make the sevice
	// Contract Service In App.Abstraction

	//Allow DI for ISaveChangesInterceptor to give u   BaseEntityAuditableInterceptor

	// Then Build The Interceptor 
	// Override SaveChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
	// private method
	// Call method in SaveChanges 
	public class BaseEntityAuditableInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public BaseEntityAuditableInterceptor(ILoggedInUserService loggedInUserService)
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
		public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);
			return base.SavedChangesAsync(eventData, result, cancellationToken);
		}


		private void UpdateEntities(DbContext? dbContext)
		{
			if (dbContext == null) return;

			var utcNow = DateTime.UtcNow;

			foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
			{
				//if (entry.State==EntityState.Added ||  entry.State==EntityState.Modified)
				//
				if (entry is { State: EntityState.Added or EntityState.Modified })
				{
					if (entry.State == EntityState.Added)
					{
						entry.Entity.CreatedBy = "";
						entry.Entity.CreatedOn = utcNow;

					}

					entry.Entity.LastModifiedBy = "";
					entry.Entity.LastModifiedOn = utcNow;	
				}
			}
		}
	}
}
