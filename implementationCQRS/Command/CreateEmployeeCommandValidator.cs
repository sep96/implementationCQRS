using FluentValidation;

namespace implementationCQRS.Command
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        /// <summary>
      
        /// </summary>
        public CreateEmployeeCommandValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty();
            RuleFor(customer => customer.LastName).NotEmpty();
        }
    }
}
