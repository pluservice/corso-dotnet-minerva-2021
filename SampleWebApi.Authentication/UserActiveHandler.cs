using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.DataAccessLayer;
using System.Threading.Tasks;

namespace SampleWebApi.Authentication
{
    public class UserActiveHandler : AuthorizationHandler<UserActiveRequirement>
    {
        private readonly DataContext dataContext;

        public UserActiveHandler(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserActiveRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userName = context.User.Identity.Name;
                var user = await dataContext.People.FirstOrDefaultAsync(p => p.FirstName == userName);
                if (user?.IsActive ?? false)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
