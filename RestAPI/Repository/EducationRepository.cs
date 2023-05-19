﻿using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class EducationRepository : IUniversityRepository<Education>
    {
        private readonly BookingManagementContext _context;

        public EducationRepository(BookingManagementContext context)
        {
            _context = context;
        }
        public Education Create(Education education)
        {
            try
            {
                _context.Set<Education>().Add(education);
                _context.SaveChanges();
                return education;
            }
            catch
            {
                return new Education();
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var education = GetByGuid(guid);
                if (education is null)
                {
                    return false;
                }
                _context.Set<Education>().Remove(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().ToList();
        }

        public Education GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Find(guid);
        }

        public bool Update(Education education)
        {
            try
            {
                _context.Set<Education>().Update(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
