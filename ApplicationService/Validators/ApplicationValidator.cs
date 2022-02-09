using Application.Service.Models;
using FluentValidation;
using NLog;

namespace Application.Service.Validators
{
    public class ApplicationValidator: AbstractValidator<ApplicationQuery>
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public ApplicationValidator()
        {
            RuleFor(x => x.ApplicationNum).NotEmpty().WithMessage("Номер заявки не может быть пустым");
            RuleFor(x => x.Applicant).NotEmpty();
            RuleFor(x => x.RequestedCredit).NotEmpty();
            RuleFor(x => x.RequestedCredit.MonthSalary)
                .Must(SalarySize).WithMessage("Низкий уровень заработной платы");
        }

        private bool SalarySize(double salary)
        {
            if (salary <= 0)
            {
                _logger.Error("Низкий уровень заработной платы");
                return false;
            }

            return true;
        }
    }
}