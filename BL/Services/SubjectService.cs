using DAL.Models;
using DAL.Repositories;
using System.Collections.Generic;

namespace BL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository) => _subjectRepository = subjectRepository;

        public IEnumerable<Subject> GetSubjects() => _subjectRepository.GetAll();
    }
}