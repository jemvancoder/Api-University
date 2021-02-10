using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
			_context = context ?? throw new ArgumentNullException(nameof(context));
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
			if (target == null)
				throw new ArgumentNullException(nameof(target));

			if (errors == null)
				throw new ArgumentNullException(nameof(errors));

			var student = _context.Students
				.Include(s => s.Enrollments)
				.FirstOrDefault(s => s.Id == target.StudentId);

			if (student?.Enrollments?.Count >= 2)
				errors.Add("Student", "A student should only be able to enroll in a maximum of 2 courses.");

			var course = _context.Courses
				.Include(c => c.CourseProfessors)
				.FirstOrDefault(c => c.Id == target.CourseId);

			if (course == null || !course.CourseProfessors.Any(c => c.ProfessorId == target.ProfessorId))
			{
				errors.Add("Professor", "A student cannot enroll in a course that the specified professor does not teach.");
			};
		}
	}

}
