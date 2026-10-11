using Microsoft.EntityFrameworkCore;
using SlavinIgorkt_31_23.Database;
using SlavinIgorkt_31_23.Models;
using SlavinIgorkt_31_23.Filters;
using SlavinIgorkt_31_23.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SlavinIgorKt_31_23.Tests
{
    public class StudentIntegrationTests
    {
        private readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public StudentIntegrationTests()
        {
            // Для каждого теста создается своя уникальная пустая БД в памяти
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetStudentsAsync_sp1_TwoObjects()
        {
            // Arrange
            using var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);

            var specialties = new List<Specialty>
            {
                new Specialty { Title = "сп1", Code = "1" },
                new Specialty { Title = "сп2", Code = "2" }
            };
            ctx.Specialtys.AddRange(specialties);
            ctx.SaveChanges();

            var groups = new List<Group>
            {
                new Group { Name = "гр1", Course = 4, SpecialtyId = 1, IsDeleted = false },
                new Group { Name = "гр2", Course = 3, SpecialtyId = 1, IsDeleted = false },
                new Group { Name = "гр3", Course = 4, SpecialtyId = 2, IsDeleted = false }
            };
            ctx.Groups.AddRange(groups);
            ctx.SaveChanges();

            var students = new List<Student>
            {
                new Student { FirstName = "Иван",  LastName = "иванов",  GroupId = 1, IsDeleted = false },
                new Student { FirstName = "",  LastName = "петров",  GroupId = 2, IsDeleted = false }, 
                new Student { FirstName = "петр", LastName = "иванов", GroupId = 1, IsDeleted = false },
                new Student { FirstName = "",  LastName = "сидоров", GroupId = 3, IsDeleted = false }
            };
            ctx.Students.AddRange(students);
            ctx.SaveChanges();

            // Act
            var result = await studentService.GetLastNamesBySpecialtyAsync("сп1", CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public async Task GetGroupsStudentsCountBySpecialtyAsync_sp1_TwoGroupsCorrectCounts()
        {
            // Arrange
            using var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);

            var specialties = new List<Specialty>
            {
                new Specialty { Title = "сп1", Code = "1" },
                new Specialty { Title = "сп2", Code = "2" }
            };
            await ctx.Specialtys.AddRangeAsync(specialties);
            await ctx.SaveChangesAsync();

            var groups = new List<Group>
            {
                new Group { Name = "гр1", Course = 4, SpecialtyId = 1, IsDeleted = false },
                new Group { Name = "гр2", Course = 3, SpecialtyId = 1, IsDeleted = false }
            };
            await ctx.Groups.AddRangeAsync(groups);
            await ctx.SaveChangesAsync();

            var students = new List<Student>
            {
                new Student { FirstName = "Иван", LastName = "Иванов", GroupId = 1, IsDeleted = false },
                new Student { FirstName = "Петр", LastName = "Петров", GroupId = 1, IsDeleted = false },
                new Student { FirstName = "Олег", LastName = "Удален", GroupId = 1, IsDeleted = true }, 
                new Student { FirstName = "Анна", LastName = "Смирнова", GroupId = 2, IsDeleted = false }
            };
            await ctx.Students.AddRangeAsync(students);
            await ctx.SaveChangesAsync();

            // Act
            var result = await studentService.GetGroupsStudentsCountBySpecialtyAsync("сп1", CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);

            Assert.Equal("гр1: 2", result[0]);

            Assert.Equal("гр2: 1", result[1]);
        }

    }
}