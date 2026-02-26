namespace OrdersService.Validators;

using FluentValidation;
using OrdersService.DTOs;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Status)
            .NotEmpty()
            .MaximumLength(50);
    }
}