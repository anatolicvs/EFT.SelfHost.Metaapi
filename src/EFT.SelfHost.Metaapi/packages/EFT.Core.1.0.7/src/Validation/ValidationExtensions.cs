using EFT.Core.Domains;
using EFT.Core.Services;
using EFT.Core.Services.Errors.Consts;
using EFT.Core.Services.Extensions;
using EFT.Core.Services.Interfaces;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFT.Core.Validation
{
    public static class ValidationExtensions
    {
        public static IServiceResult<TEntity> Validate<TEntity>(this TEntity entity) where TEntity : class, new()
        {
            var result = new ServiceResult<TEntity>();
            var attr = GetValidationAtribute<TEntity>();
            if (attr != null)
            {
                var validator = (IValidator<TEntity>)Activator.CreateInstance(attr.ValidatorType);
                var validateResult = validator.Validate(entity);

                foreach (var item in validateResult.Errors)
                    result.Errors.Add(GlobalErrors.NotValidation, item.ErrorMessage);
            }
            return result;
        }
        public static ValidatorAttribute GetValidationAtribute<T>()
        {
            var dnAttribute = typeof(T).GetCustomAttributes(
                typeof(ValidatorAttribute), true
            ).FirstOrDefault() as ValidatorAttribute;
            if (dnAttribute != null)
            {
                return dnAttribute;
            }
            return null;
        }
    }
}

