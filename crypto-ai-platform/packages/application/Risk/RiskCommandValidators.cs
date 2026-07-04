using FluentValidation;

namespace CryptoAIPlatform.Application.Risk;

public class CreateRiskProfileCommandValidator : AbstractValidator<CreateRiskProfileCommand>
{
    public CreateRiskProfileCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
    }
}

public class ValidateOrderRiskCommandValidator : AbstractValidator<ValidateOrderRiskCommand>
{
    public ValidateOrderRiskCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.PortfolioId).NotEmpty();
    }
}

public class UpdateExposureCommandValidator : AbstractValidator<UpdateExposureCommand>
{
    public UpdateExposureCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
    }
}

public class TriggerMarginCallCommandValidator : AbstractValidator<TriggerMarginCallCommand>
{
    public TriggerMarginCallCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
    }
}

public class UpdatePortfolioRiskCommandValidator : AbstractValidator<UpdatePortfolioRiskCommand>
{
    public UpdatePortfolioRiskCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
    }
}
