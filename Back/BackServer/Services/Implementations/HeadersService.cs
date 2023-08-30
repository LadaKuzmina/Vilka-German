using System.Collections.Generic;
using System.Threading.Tasks;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;
using Entity;

namespace BackServer.Services
{
    public class HeadersService : IHeadersService
    {
        private readonly IHeadersVisitor _visitor;
        private readonly IHeadersChanger _changer;

        public HeadersService(IHeadersVisitor visitor, IHeadersChanger changer)
        {
            _visitor = visitor;
            _changer = changer;
        }

        public async Task<IEnumerable<Entity.HeadingOne>> GetAllHeadingsOne()
        {
            return await _visitor.GetAllHeadingsOneAsync();
        }

        public async Task<IEnumerable<Entity.HeadingTwo>> GetAllHeadingsTwo()
        {
            return await _visitor.GetAllHeadingsTwoAsync();
        }
        
        public async Task<IEnumerable<Entity.HeadingThree>> GetAllHeadingsThree()
        {
            return await _visitor.GetAllHeadingsThree();
        }

        public async Task<IEnumerable<Entity.HeadingTwo>> GetHeadingsTwoByHeadingsOne(string headingOneTitle)
        {
            return await _visitor.GetHeadingsTwoByHeadingsOneAsync(headingOneTitle);
        }

        public async Task<IEnumerable<Entity.HeadingThree>> GetHeadingsThreeByHeadingsTwoAsync(string headingTwoTitle)
        {
            return await _visitor.GetHeadingsThreeByHeadingsTwoAsync(headingTwoTitle);
        }

        public async Task<IEnumerable<HeadingOne>> GetHeadingsOneBySubstringsAsync(string[] substrings)
        {
            return await _visitor.GetHeadingsOneBySubstringsAsync(substrings);
        }

        public async Task<IEnumerable<HeadingTwo>> GetHeadingsTwoBySubstringsAsync(string[] substrings)
        {
            return await _visitor.GetHeadingsTwoBySubstringsAsync(substrings);
        }

        public async Task<IEnumerable<HeadingThree>> GetHeadingsThreeBySubstringsAsync(string[] substrings)
        {
            return await _visitor.GetHeadingsThreeBySubstringsAsync(substrings);
        }

        public async Task<bool> AddHeadingOne(HeadingOne headingOne)
        {
            if ((await _visitor.GetAllHeadingsOneAsync()).FirstOrDefault(x => x.Title == headingOne.Title) != null)
                return true;

            return await _changer.AddHeadingOne(headingOne);
        }

        public async Task<bool> AddHeadingTwo(HeadingTwo headingTwo)
        {
            if ((await _visitor.GetAllHeadingsTwoAsync()).FirstOrDefault(x => x.Title == headingTwo.Title) != null)
                return true;
            
            return await _changer.AddHeadingTwo(headingTwo);
        }

        public async Task<bool> AddHeadingThree(HeadingThree headingThree)
        {
            if ((await _visitor.GetAllHeadingsThree()).FirstOrDefault(x => x.Title == headingThree.Title) != null)
                return true;
            
            return await _changer.AddHeadingThree(headingThree);
        }

        public async Task<bool> DeleteHeadingOne(string title)
        {
            return await _changer.DeleteHeadingOne(title);
        }

        public async Task<bool> DeleteHeadingTwo(string title)
        {
            return await _changer.DeleteHeadingTwo(title);
        }

        public async Task<bool> DeleteHeadingThree(string title)
        {
            return await _changer.DeleteHeadingThree(title);
        }

        public async Task<bool> UpdateHeadingOne(string oldHeadingOneTitle, HeadingOne headingOne)
        {
            // if (!CheckCorrectHeadingOne(headingOne))
            //     return false;

            return await _changer.UpdateHeadingOne(oldHeadingOneTitle, headingOne);
        }

        public async Task<bool> UpdateHeadingTwo(string oldHeadingTwoTitle, HeadingTwo headingTwo)
        {
            // if (!CheckCorrectHeadingTwo(headingTwo))
            //     return false;

            return await _changer.UpdateHeadingTwo(oldHeadingTwoTitle, headingTwo);
        }

        public async Task<bool> UpdateHeadingThree(string oldHeadingThreeTitle, HeadingThree headingThree)
        {
            // if (!CheckCorrectHeadingThree(headingThree))
            //     return false;

            return await _changer.UpdateHeadingThree(oldHeadingThreeTitle, headingThree);
        }
    }
}