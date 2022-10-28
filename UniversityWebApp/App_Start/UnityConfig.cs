using BL.Services;
using DAL.Models;
using DAL.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace UniversityWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType(typeof(IRepository<>), typeof(IRepository<Student>));
            container.RegisterType(typeof(IRepository<>), typeof(IRepository<Subject>));
            container.RegisterType(typeof(IRepository<>), typeof(IRepository<SubjectResult>));
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<ISubjectService, SubjectService>();
            container.RegisterType<ISubjectResultService, SubjectResultService>();


            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}