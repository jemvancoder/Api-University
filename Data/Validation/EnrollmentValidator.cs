using Data.Entities;
using System;
using System.Collections.Generic;

namespace Data.Validation
{
	/// <summary>
	///		Represents an enrollment validator. 
	/// </summary>
	public class EnrollmentValidator
		: IValidator<Enrollment>
	{
		/// <summary>
		///		Creates a new instance of a <see cref="EnrollmentValidator"/>.
		/// </summary>
		public EnrollmentValidator(IEnumerable<IValidationRule<Enrollment>> rules)
		{
			
		}

		/// <summary>
		///		Determines if the target object is valid. 
		/// </summary>
		/// <param name="target"> The object to be validated. </param>
		/// <param name="errors"> The key/value pairs indicating the collection of errors. </param>
		/// <returns> The value indicating whether the object is valid. </returns>
		public bool IsValid(Enrollment target, out Dictionary<string, string> errors)
		{
			throw new NotImplementedException();
		}
	}
}
