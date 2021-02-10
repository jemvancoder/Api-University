using Api.ViewModel;
using Data.Exceptions;
using Data.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Api.Controllers
{
    /// <summary>
    ///		An API controller used to manage student enrollments. 
    /// </summary>
    [Route("api/enrollment")]
    public class EnrollmentsController : Controller
    {
        private readonly IStudentProvider _studentProvider;
        private readonly IProfessorProvider _professorProvider;
        private readonly ICourseProvider _courseProvider;
        private readonly IEnrollmentProvider _enrollmentProvider;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentProvider"></param>
        /// <param name="professorProvider"></param>
        /// <param name="courseProvider"></param>
        /// <param name="enrollmentProvider"></param>
        public EnrollmentsController(IStudentProvider studentProvider,
            IProfessorProvider professorProvider,
            ICourseProvider courseProvider,
            IEnrollmentProvider enrollmentProvider,
            ILogger<EnrollmentsController> logger)
        {
            this._studentProvider = studentProvider ?? throw new ArgumentNullException(nameof(studentProvider));
            this._professorProvider = professorProvider ?? throw new ArgumentNullException(nameof(professorProvider));
            this._courseProvider = courseProvider ?? throw new ArgumentNullException(nameof(courseProvider));
            this._enrollmentProvider = enrollmentProvider ?? throw new ArgumentNullException(nameof(enrollmentProvider));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost]
        [Route("enroll")]
        public IActionResult Enroll([FromBody] EnrollViewModel model)
        {
            try
            {
                TryValidateModel(model);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = this._studentProvider.GetStudentById(model.StudentId);
                var course = this._courseProvider.GetById(model.CourseId);
                var professor = this._professorProvider.GetProfessorById(model.ProfessorId);

                if (student == null)
                    ModelState.AddModelError(nameof(model.StudentId), "Does not exist.");
                if (course == null)
                    ModelState.AddModelError(nameof(model.CourseId), "Does not exist.");
                if (professor == null)
                    ModelState.AddModelError(nameof(model.ProfessorId), "Does not exist.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var enrollment = this._enrollmentProvider.Enroll(student, course, professor);

                return Ok(enrollment);

            }
            catch (ValidationException ex)
            {
                foreach (var e in ex.Errors)
                {
                    ModelState.AddModelError(e.Key, e.Value);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }


    }
}