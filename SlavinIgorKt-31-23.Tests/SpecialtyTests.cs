using SlavinIgorkt_31_23.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlavinIgorKt_31_23.Tests
{
    public class SpecialtyTests
    {
        [Fact]
        public void IsValidSpecialtyTitle_Correct_True()
        {
            // arrange
            var testSpecialty = new Specialty
            {
                Title = "Программная инженерия"
            };

            // act
            var result = testSpecialty.IsValidSpecialtyTitle();

            // assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidSpecialtyTitle_Incorrect_False()
        {
            // arrange
            var testSpecialty = new Specialty
            {
                Title = "сп1"
            };

            // act
            var result = testSpecialty.IsValidSpecialtyTitle();

            // assert
            Assert.False(result);
        }
    }

}
