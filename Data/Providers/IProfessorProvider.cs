﻿using System.Collections.Generic;
using Data.Entities;

namespace Data.Providers
{
	/// <summary>
	///		Represents a professor provider.
	/// </summary>
	public interface IProfessorProvider
	{
		/// <summary>
		///		Creates a new professor. 
		/// </summary>
		/// <param name="professor"> The professor object to create. </param>
		void Create(Professor professor);

		Professor GetProfessorById(int professorId);

		/// <summary>
		///		Gets all professors. 
		/// </summary>
		/// <returns></returns>
		IReadOnlyCollection<Professor> GetAll(); 
	}
}
