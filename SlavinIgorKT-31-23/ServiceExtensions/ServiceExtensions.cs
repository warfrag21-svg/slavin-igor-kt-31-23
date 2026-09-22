using SlavinIgorkt_31_23.Interfaces;
using SlavinIgorkt_31_23.Services;

namespace SlavinIgorkt_31_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();

            return services;
        }
    }
}