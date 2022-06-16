using FluentValidation;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Service.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {

        public ProductDTOValidator()
        {

            RuleFor(x => x.Name).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("{PropertyName} is required!");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");

        }

    }    
}
