using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SlavinIgorkt_31_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public int SpecialtyId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Specialty? Specialty { get; set; }
        public bool IsValidGroupName()
        {
            return Regex.IsMatch(Name ?? string.Empty, @"^[A-ZА-Я]{2,4}-\d{2}-\d{2}$");
        }
    }
}