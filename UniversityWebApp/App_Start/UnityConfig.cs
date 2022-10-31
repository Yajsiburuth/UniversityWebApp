using BL.Services;
using DAL.Models;
using DAL.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace UniversityWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            container.RegisterType<ISubjectResultRepository, SubjectResultRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<ISubjectService, SubjectService>();
            container.RegisterType<ISubjectResultService, SubjectResultService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}