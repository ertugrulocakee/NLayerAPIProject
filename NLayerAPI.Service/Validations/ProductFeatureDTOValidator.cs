using FluentValidation;
using NLayerAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Service.Validations
{
    public class ProductFeatureDTOValidator : AbstractValidator<ProductFeatureDTO>
    {

        public ProductFeatureDTOValidator()
        {

            RuleFor(x => x.Color).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.Color).NotEmpty().WithErrorCode("{PropertyName} is required!");

            RuleFor(x => x.Width).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");
            RuleFor(x => x.Height).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");
            RuleFor(x => x.ProductId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");

        }

    }
}
