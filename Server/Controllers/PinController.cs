using LogoPi.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogoPi.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PinController
    {
        private static readonly string[] Names = new[]
        {"Pin1", "Pin2", "Pin3", "Pin4", "Pin5", "Pin6", "Pin7", "Pin8", "Pin9", "Pin10" };

        private static List<Pin> Pinns = new List<Pin>();

        private readonly ILogger<PinController> logger;

        public PinController(ILogger<PinController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Pin> Get()
        {
            if (Pinns?.Count == 0)
            {

                var rng = new Random();
                Pinns.AddRange(Enumerable.Range(1, 5).Select(index => new Pin
                {
                    Name = Names[rng.Next(Names.Length)],
                    PinNumber = index,
                    State = false
                })
                .ToArray());
            }

            return Pinns;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Pin pinToDelete = Pinns.Where(p => p.PinNumber == id).FirstOrDefault();

            if (pinToDelete != null)
                Pinns.Remove(pinToDelete);
        }
    }
}
