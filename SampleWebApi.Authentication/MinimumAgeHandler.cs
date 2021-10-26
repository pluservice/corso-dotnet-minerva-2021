using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using SampleWebApi.DataAccessLayer;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleWebApi.Authentication
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly DataContext dataContext;

        public MinimumAgeHandler(IConfiguration configuration, DataContext dataContext)
        {
            var setting1 = configuration.GetValue<string>("ApplicationOptions:Setting1");
            this.dataContext = dataContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                var dateString = context.User.FindFirst(ClaimTypes.DateOfBirth).Value;
                var date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var age = DateTime.UtcNow.Date.Year - date.Year;
                if (age >= requirement.MinimumAge)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
