using Data.Entities;
using System;
using System.Collections.Generic;

namespace Data.Validation
{
	public class EnrollmentCountRule
		: IValidationRule<Enrollment>
	{
		private readonly UniversityContext _context;

		/// <summary>
		///		Represents an enrollment count rule. 
		/// </summary>
		/// <param name="context"></param>
		public EnrollmentCountRule(UniversityContext context)
		{
			_context = context;
		}

		/// <summary>
		///		Determines if the target is valid based on this validation rule.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target"> The object to be validated. </param>
		/// <param name="errors"> The key/value pair representing the collection of errors. </param>
		/// <returns> The value indicating whether the target object passed the validation rule. </returns>
		public void Enforce(Enrollment target, IDictionary<string, string> errors)
		{
			throw new NotImplementedException();
		}
	}

}
