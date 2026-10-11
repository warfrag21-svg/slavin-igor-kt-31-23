using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SlavinIgorkt_31_23.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string Title { get; set; }
        
        public string Code { get; set; }

        public bool IsValidSpecialtyTitle()
        {
            return Regex.Match(Title, @"^[А-ЯA-Z][а-яa-z]+( [а-яa-z]+)*$").Success;
        }
    }
}