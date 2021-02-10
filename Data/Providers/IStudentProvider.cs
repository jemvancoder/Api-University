using Data.Common;
using Data.Entities;
using System.Collections.Generic;

namespace Data.Providers
{
	/// <summary>
	///		Represents a student provider.
	/// </summary>
	public interface IStudentProvider
	{
		/// <summary>
		///		Creates a new student. 
		/// </summary>
		/// <param name="student"> The student object to create. </param>
		void Create(Student student);

		Student GetStudentById(int studentId);
		IReadOnlyCollection<Student> GetAll();
	}
}
