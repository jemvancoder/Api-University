using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Api.ViewModel
{
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
