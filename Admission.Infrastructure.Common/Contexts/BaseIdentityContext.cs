using Admission.Infrastructure.Common.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admission.Infrastructure.Common.Contexts;

public class BaseIdentityContext<TUser,TRole,TKey,TUserClaim,TUserRole,TUserLogin,TRoleClaim,TUserToken>: IdentityDbContext
    <TUser,TRole,TKey,TUserClaim,TUserRole,TUserLogin,TRoleClaim,TUserToken>
    where TUser: IdentityUser<TKey>
    where TRole: IdentityRole<TKey>
    where TKey: IEquatable<TKey>
    where TUserClaim: IdentityUserClaim<TKey>
    where TUserRole: IdentityUserRole<TKey>
    where TUserLogin: IdentityUserLogin<TKey>
    where TRoleClaim: IdentityRoleClaim<TKey>
    where TUserToken: IdentityUserToken<TKey>
{
    protected BaseIdentityContext(DbContextOptions options): base(options) {}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        EntityStateHelper.SetTimestamps(ChangeTracker);
        return base.SaveChangesAsync(cancellationToken);
    }
}