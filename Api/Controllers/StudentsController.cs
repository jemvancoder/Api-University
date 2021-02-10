using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.ViewModel;
using Data.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentProvider _studentProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentProvider"></param>
        public StudentsController(IStudentProvider studentProvider)
        {
            this._studentProvider = studentProvider ?? throw new ArgumentNullException(nameof(studentProvider));            
        }

        [HttpGet]
        [Route("{studentId}/courses")]
        public IActionResult GetStudentCourses(int studentId)
        {
            if (studentId <= 0)
            {
                ModelState.AddModelError(nameof(studentId), "Invalid Student ID.");

                return BadRequest(ModelState);
            };

            var student = this._studentProvider.GetStudentById(studentId);
            if (student == null)
            {
                ModelState.AddModelError(nameof(studentId), "Invalid Student ID.");

                return BadRequest(ModelState);
            };
            var coursesSelected = student.Enrollments.Select(e => new CourseModel
            {
                Id = e.CourseId,
                Name = e.Course.Name,
                Professors = e.Course
                            .CourseProfessors
                            .Select(p => new ProfessorModel { 
                                Id = p.Professor.Id, 
                                Name = p.Professor.Name 
                            })
            });
            return Ok(coursesSelected);
        }
    }
}
    