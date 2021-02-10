using System.ComponentModel.DataAnnotations;



namespace Api.ViewModel
{
    /// <summary>
    /// Added ViewModel to handle Ids
    /// </summary>
    public class EnrollViewModel
    {
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Student ID should be greater than 0.")]
        public int StudentId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID should be greater than 0.")]
        public int CourseId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Professor ID should be greater than 0.")]
        public int ProfessorId { get; set; }


     
    }
}
