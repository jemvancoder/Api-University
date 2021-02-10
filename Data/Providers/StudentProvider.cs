using Data.Common;
using Data.Entities;
using Data.Exceptions;
using Data.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Providers
{
    /// <summary>
    ///		Represents a student provider.
    /// </summary>
    public class StudentProvider
        : IStudentProvider
    {
        private readonly UniversityContext _context;
        private readonly IValidator<Student> _validator;

        ///  <summary>
        /// 		Creates a new instance of a <see cref="StudentProvider"/>. 
        ///  </summary>
        ///  <param name="context"> The instance of a <see cref="UniversityContext"/>. </param>
        ///  <param name="validator"> The instance of a <see cref="IValidator{T}"/>. </param>
        public StudentProvider(UniversityContext context, IValidator<Student> validator)
        {
            _context = context;
            _validator = validator;
        }

        /// <summary>
        ///		Creates a new student. 
        /// </summary>
        /// <param name="student"> The student object to create. </param>
        public void Create(Student student)
        {
            // Validate the student. 
            if (!_validator.IsValid(student, out var errors))
                throw new ValidationException(errors);

            // Create the student. 
            var studentEntity = student as Student ?? new Student { Id = student.Id, Name = student.Name };
            _context.Students.Add(studentEntity);
            _context.SaveChanges();
        }

        public IReadOnlyCollection<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int studentId)
        {
            if (studentId <= 0)
                throw new ArgumentException($"{nameof(studentId)} should be greater than 0.");

            return _context.Students
                .Include(x => x.Enrollments)
                .ThenInclude(e => e.Course)
                .ThenInclude(e => e.CourseProfessors)
                .ThenInclude(cp => cp.Professor)
                .FirstOrDefault(c => c.Id == studentId);
        }
    }
}
