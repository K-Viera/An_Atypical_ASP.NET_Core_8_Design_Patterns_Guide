using FluentValidation;

namespace OptionsValidationFluentValidation
{
    public class MyOptionsValidator : AbstractValidator<MyOptions>
    {
        public MyOptionsValidator() { 
            RuleFor(x=>x.Name).NotEmpty();
        }
    }
}
