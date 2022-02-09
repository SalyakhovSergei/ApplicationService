using Application.Service.Models;
using FluentValidation;

namespace Application.Service.Validators
{
    public class ApplicationValidator: AbstractValidator<ApplicationQuery>
    {
        public ApplicationValidator()
        {
            RuleFor(x => x.ApplicationNum).NotEmpty().WithMessage("Номер заявки не может быть пустым");
            RuleFor(x => x.Applicant).NotEmpty();
            RuleFor(x => x.RequestedCredit).NotEmpty();
            RuleFor(x => x.RequestedCredit.MonthSalary)
                .Must(e => e > 0).WithMessage("Низкий уровень заработной платы");
        }
    }
}