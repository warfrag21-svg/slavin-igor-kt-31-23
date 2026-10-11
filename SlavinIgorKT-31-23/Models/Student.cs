using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SlavinIgorkt_31_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Group? Group { get; set; }
    }
}