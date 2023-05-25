using System;
using System.Collections.Generic;
using BackServer.Contexts;
using DbEntity;

namespace BackServer.Repositories
{
    public class ProjectsRepository : IRepos
    {
        public IEnumerable<Project> GetAll()
        {
            using (TestContext db = new())
            {
                return db.Projects;
            }
        }

        public bool Add(Project project)
        {
            try
            {
                using (TestContext db = new())
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public bool Delete(Project project)
        {
            try
            {
                using (TestContext db = new())
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}