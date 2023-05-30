using System.Collections.Generic;
using BackServer.Repositories;

namespace BackServer.Services
{
    public class HeadersService
    {
        private readonly IHeadersRepository _repository;
        public HeadersService(IHeadersRepository repository)
        {
            _repository = repository;
        }
    }
}