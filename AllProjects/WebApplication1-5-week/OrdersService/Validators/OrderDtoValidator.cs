namespace OrdersService.Validators
{
    using FluentValidation;
    using OrdersService.DTOs;
    using OrdersService.Models;

    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 200 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");

            RuleFor(x => x.Status)
                .Must(BeValidStatus)
                .WithMessage("Invalid status value.");
        }

        private static bool BeValidStatus(string status)
        {
            return Enum.TryParse<OrderStatus>(status, true, out _);
        }
    }
}