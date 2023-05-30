using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackServer.Contexts;
using DbEntity;

namespace BackServer.Repositories
{
    public class ProjectsRepository
    {
        private readonly TestContext _context;

        public ProjectsRepository(TestContext context)
        {
            _context = context;
        }
        public IEnumerable<Project> GetAll()
        {
            return _context.Projects;
        }

        public bool Add(Project project)
        {
            // try
            // {
            //     using (TestContext db = new())
            //     {
            //         db.Projects.Add(project);
            //         db.SaveChanges();
            //         return true;
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            //     return false;
            // }
            throw new NotImplementedException();
        }
        
        public bool Delete(Project project)
        {
            // try
            // {
            //     using (TestContext db = new())
            //     {
            //         db.Projects.Remove(project);
            //         db.SaveChanges();
            //         return true;
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            //     return false;
            // }
            throw new NotImplementedException();
        }
    }
}