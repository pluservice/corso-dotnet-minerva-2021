using FluentValidation;
using Microsoft.Extensions.Configuration;
using SampleWebApi.BusinessLayer.Resources;
using SampleWebApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApi.BusinessLayer.Validations
{
    public class SavePersonRequestValidator : AbstractValidator<SavePersonRequest>
    {
        public SavePersonRequestValidator(IConfiguration configuration)
        {
            var minimumAge = configuration.GetValue<int>("ApplicationOptions:MinimumAge");

            RuleFor(p => p.FirstName).NotEmpty().WithMessage(Messages.FieldRequired)
                .MaximumLength(50).WithMessage(Messages.MaxLength)
                .WithName(Messages.FirstName);

            RuleFor(p => p.LastName).NotEmpty().WithMessage(Messages.FieldRequired)
                .MaximumLength(50).WithMessage(Messages.MaxLength)
                .WithName(Messages.LastName);

            RuleFor(p => p.Age).GreaterThanOrEqualTo(minimumAge);

            RuleFor(p => p.BirthDate).LessThan(DateTime.UtcNow);

            RuleFor(p => p.FiscalCode).Must(BeAValidFiscalCode).When(p => p.Country == "IT");

            RuleFor(p => p.Discount).NotEmpty().GreaterThan(0).When(p => p.HasDiscount);

            RuleFor(p => p.Categories).NotEmpty().Must(MustBeUnique);
            RuleForEach(p => p.Categories).ChildRules(category =>
            {
                category.RuleFor(c => c.ProductIds).NotEmpty();
            });
        }

        private bool MustBeUnique(IEnumerable<FavoriteCategory> categories)
        {
            var duplicates = categories.GroupBy(x => x.CategoryId)
                                         .Where(g => g.Count() > 1)
                                         .Select(x => x.Key);

            return !duplicates.Any();
        }

        private bool BeAValidFiscalCode(string fiscalCode)
        {
            return fiscalCode?.Length == 16;
        }
    }
}
