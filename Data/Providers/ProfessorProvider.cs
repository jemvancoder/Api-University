﻿using Data.Entities;
using Data.Exceptions;
using Data.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Providers
{
    /// <summary>
    ///		Represents a professor provider.
    /// </summary>
    public class ProfessorProvider
        : IProfessorProvider
    {
        private readonly UniversityContext _context;
        private readonly IValidator<Professor> _validator;

        ///  <summary>
        /// 		Creates a new instance of a <see cref="ProfessorProvider"/>. 
        ///  </summary>
        ///  <param name="context"> The instance of a <see cref="UniversityContext"/>. </param>
        ///  <param name="validator"> The instance of a <see cref="IValidator{T}"/>. </param>
        public ProfessorProvider(UniversityContext context, IValidator<Professor> validator)
        {
            _context = context;
            _validator = validator;
        }

        /// <summary>
        ///		Creates a new professor. 
        /// </summary>
        /// <param name="professor"> The professor object to create. </param>
        public void Create(Professor professor)
        {
            // Validate the student. 
            if (!_validator.IsValid(professor, out var errors))
                throw new ValidationException(errors);

            foreach (var courseProfessor in professor.CourseProfessors)
                _context.Attach(courseProfessor);

            _context.Professors.Add(professor);
            _context.SaveChanges();
        }

        /// <summary>
        ///		Gets all professors. 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Professor> GetAll()
        {
            return _context.Professors.ToList();
        }
        /// <summary>
        /// Get Professor by ProfessorId
        /// </summary>
        /// <param name="professorId"></param>
        /// <returns></returns>
        public Professor GetProfessorById(int professorId)
        {
            if (professorId <= 0)
                throw new ArgumentException($"{nameof(professorId)} should be greater than 0.");

            return _context.Professors
                .FirstOrDefault(c => c.Id == professorId);
        }
    }
}
