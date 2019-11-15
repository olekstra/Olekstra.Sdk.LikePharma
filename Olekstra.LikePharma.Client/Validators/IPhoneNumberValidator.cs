namespace Olekstra.LikePharma.Client.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Validator interface for phone numbers.
    /// </summary>
    public interface IPhoneNumberValidator
    {
        /// <summary>
        /// Method to validate phone number.
        /// </summary>
        /// <param name="value">Phone number value to validate.</param>
        /// <returns>Result of validation (<see cref="ValidationResult.Success"/> if validation successful).</returns>
        ValidationResult ValidatePhoneNumber(string? value);
    }
}
