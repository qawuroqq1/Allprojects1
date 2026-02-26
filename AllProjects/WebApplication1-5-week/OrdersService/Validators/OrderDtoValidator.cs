namespace OrdersService.Validators
{
    using FluentValidation;
    using OrdersService.DTOs;

    /// <summary>
    /// Определяет правила валидации для OrderDto.
    /// </summary>
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDtoValidator"/> class.
        /// Инициализирует новый экземпляр валидатора OrderDto.
        /// </summary>
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
                .NotEmpty()
                .WithMessage("Status is required.");
        }
    }
}