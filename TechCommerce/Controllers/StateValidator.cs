using System.ComponentModel.DataAnnotations;

namespace TechCommerce.Controllers
{
    public class StateValidator
    {
        private static readonly string[] AllowedStates = { "Shipped", "Processing", "Delivered", "Cancelled" };

        public static ValidationResult ValidateState(string state, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(state))
                return new ValidationResult("State is required.");

            if (AllowedStates.Contains(state))
                return ValidationResult.Success;

            return new ValidationResult($"Invalid state. Allowed states are: {string.Join(", ", AllowedStates)}");
        }
    }
}
