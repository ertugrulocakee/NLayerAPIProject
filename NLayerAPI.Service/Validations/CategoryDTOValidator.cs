using FluentValidation;
using NLayerAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Service.Validations
{
    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {

        public CategoryDTOValidator()
        {

            RuleFor(x => x.Name).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("{PropertyName} is required!");

        }

    }
}
