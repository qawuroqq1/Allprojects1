namespace OrdersService.Validation
{
    using System.ComponentModel.DataAnnotations;
    using OrdersService.Models;

    /// <summary>
    /// Проверяет, что строковое значение статуса соответствует поддерживаемому значению OrderStatus.
    /// </summary>
    public sealed class OrderStatusValidationAttribute : ValidationAttribute
    {
        public OrderStatusValidationAttribute()
        {
            this.ErrorMessage = "Invalid status value.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            if (value is not string statusText)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(statusText))
            {
                return new ValidationResult(this.ErrorMessage);
            }

            bool isValid = Enum.TryParse<OrderStatus>(statusText.Trim(), true, out _);
            return isValid ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
        }
    }
}
