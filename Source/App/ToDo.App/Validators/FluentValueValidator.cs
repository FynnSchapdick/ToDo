﻿using FluentValidation;
using FluentValidation.Results;

namespace ToDo.App.Validators;

public sealed class FluentValueValidator<T> : AbstractValidator<T>
{
    public Func<T, IEnumerable<string>> Validation => ValidateValue;
    
    public FluentValueValidator(Action<IRuleBuilderInitial<T, T>> rule)
    {
        rule(RuleFor(x => x));
    }

    private IEnumerable<string> ValidateValue(T arg)
    {
        ValidationResult result = Validate(arg);
        return result.IsValid
            ? Array.Empty<string>()
            : result.Errors.Select(e => e.ErrorMessage);
    }
}
