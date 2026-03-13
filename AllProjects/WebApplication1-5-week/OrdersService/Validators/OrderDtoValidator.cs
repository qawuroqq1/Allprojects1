namespace OrdersService.Validators;

using FluentValidation;
using DTOs;


public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    
    public OrderDtoValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        this.RuleFor(x => x.Price)
            .GreaterThan(0);

        this.RuleFor(x => x.Status)
            .NotEmpty();
    }
}