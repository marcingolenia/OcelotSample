using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ClientsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ClientDto> Get() =>
            Enumerable.Range(1, 3).Select(index => new ClientDto
            {
                Id = index,
                Address = "New York City",
                Name = $"Trump Tower {index}",
                VatId = Guid.NewGuid().ToString()
            });
        
        [HttpGet]
        [Route("{id}")]
        public ClientDto Get(int id) =>
             new ClientDto
            {
                Id = id,
                Address = "New York City",
                Name = "Trump Tower",
                VatId = Guid.NewGuid().ToString()
            };
    }
}