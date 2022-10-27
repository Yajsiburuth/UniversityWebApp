using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class SubjectService
    {

        private readonly IRepository<Subject> _subjectRepository;

        public SubjectService(IRepository<Subject> subjectRepository) => _subjectRepository = subjectRepository;

        public IEnumerable<Subject> GetSubjects()
        {
            return _subjectRepository.GetAll();
        }
    }
}