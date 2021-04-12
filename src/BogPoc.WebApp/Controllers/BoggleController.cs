using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BogPoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoggleController : ControllerBase
    {
        private readonly ILogger<BoggleController> _logger;

        public BoggleController(ILogger<BoggleController> logger)
        {
            _logger = logger;
        }

        [HttpGet("solve")]
        public WordFinder.Solution Solve (string boardLetters)
        {
            return WordFinder.Board.Solve(boardLetters);
        }
    }
}
