using FullCourtInsights.Models;
using FullCourtInsights.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCourtInsights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController(BasketballApiService service) : ControllerBase
    {

        private readonly BasketballApiService _service = service;

        [HttpGet]
        [Route("players")]
        public async Task<IActionResult> GetPlayersByName([FromQuery] string? name, [FromQuery] string? id)
        {
            // Request should have a query "name" or "id"

            if (Request.Query.Count != 1)
            {
                return BadRequest(new { message = "Please provide a name paramter OR an id parameter." });
            }

            IEnumerable<PlayerRequest>? players;

            if (name != null && ValidName(name))
            {
                players = await _service.GetPlayersByName(name);
            } else if (id != null && ValidNum(id))
            {
                players = await _service.GetPlayerById(id);
            } else 
            {
                players = [];    
            }

            return Ok(new { results = players.ToList().Count, response = players });
        }

        [HttpGet]
        [Route("players/statistics")]
        public async Task<IActionResult> GetPlayerStatistics([FromQuery] string? id, [FromQuery] string? season, [FromQuery] string? page)
        {

            if (id == null || season == null)
            {
                return BadRequest(new { message = "Please provide an id, season, or page number (optional)." });
            }

            if (Request.Query.Count > 3)
            {
                return BadRequest(new { message = "Too many parameters." });
            }

            if (Request.Query.Count == 3 && page == null)
            {
                return BadRequest(new { message = "Please provide an id, season, or page number (optional)." });
            }

            if (!ValidNum(id) || !ValidNum(season))
            {
                return BadRequest(new { message = "Please provide valid parameters for id and season." });
            }

            IEnumerable<PlayerStatRequest>? p;

            p = await _service.GetPlayerStats(id, season);

            int count = p.ToList().Count;
            int initialIndex = 0;
            int finalIndex = 9;


            if (p.ToList().Count > 10)
            {

                int pageNo = 1;

                try
                {
                    if (page != null)
                    {
                        pageNo = int.Parse(page);

                        if (pageNo < 0)
                        {
                            pageNo = 1;
                        }
                    }
                } catch (FormatException)
                {
                    pageNo = 1;
                }

                initialIndex = (pageNo - 1) * 10;
                finalIndex = pageNo * 10 - 1;

                int maxIndex = p.ToList().Count - 1;


                if (finalIndex > maxIndex && initialIndex < maxIndex)
                {
                    finalIndex = maxIndex;
                }

                if (initialIndex > maxIndex)
                {
                    initialIndex = 0;
                    finalIndex = 9;
                }



                p = p.ToList()[initialIndex..finalIndex];
            }

            return Ok(new { results = count, range = $"{initialIndex} - {finalIndex}", response = p });
        }

        private static bool ValidName(string name)
        {
            string validName = name.Trim().Replace("%", "").Replace(" ", "");

            if (validName.All(Char.IsLetter))
            {
                return true;
            }
            return false;
        }

        private static bool ValidNum(string id)
        {
            string trimmedId = id.Trim().Replace("%", "").Replace(" ", "");

            if (trimmedId.All(Char.IsNumber))
            {
                return true;
            }
            return false;
        }
    }
}
