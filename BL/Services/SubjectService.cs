using DAL.Models;
using DAL.Repositories;
using System.Collections.Generic;

namespace BL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<Subject> _subjectRepository;
        public SubjectService(IRepository<Subject> subjectRepository) => _subjectRepository = subjectRepository;

        public IEnumerable<Subject> GetSubjects() => _subjectRepository.GetAll();
    }
}