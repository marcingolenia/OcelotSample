using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<InvoiceDto> Get() =>
            Enumerable.Range(1, 5).Select(index => new InvoiceDto
            {
                Id = index,
                IssueDate = DateTime.Today.AddDays(-index),
                Amount = new Random().Next(-2000, 2000),
                ClientId = 1,
                Number = $"FV/{index}"
            });
        
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<InvoiceDto> GetBy(int clientId) =>
            Enumerable.Range(1, 3).Select(index => new InvoiceDto
            {
                Id = index,
                IssueDate = DateTime.Today.AddDays(-index),
                Amount = new Random().Next(-2000, 2000),
                ClientId = clientId,
                Number = $"FV/{index}"
            });
    }
}