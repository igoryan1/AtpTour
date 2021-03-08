using ATP.Models;
using ATP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ATP.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IFavoritesService _favoriteService;
        private readonly string _baseUrl;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICacherService _cacheService;

        public PlayersController(IFavoritesService favService, IConfiguration configuration, IHttpClientFactory clientFactory, ICacherService cacherService)
        {
            _favoriteService = favService;
            _baseUrl = configuration.GetValue<string>("AtpBaseUrl");
            _clientFactory = clientFactory;
            _cacheService = cacherService;
        }

        public async Task<IActionResult> Index(int? fromRank, int? toRank)
        {
            var urlParams = $"/rankings.ranksglrollrange?fromrank={fromRank ?? 1}&torank={toRank ?? 10}";
            var client = _clientFactory.CreateClient();
            var results = await client.GetFromJsonAsync<PlayerModel>($"{_baseUrl}{urlParams}");
            var topRatedPlayers = results.Data.Rankings.Players;

            // mark the favorite ones
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            var favoritePlayers = _favoriteService.GetFavorite(remoteIpAddress.ToString())?.Players?.Select(p => p.PlayerId);
            if (favoritePlayers != null)
            {
                topRatedPlayers.Where(p => favoritePlayers.Contains(p.PlayerId)).ToList().ForEach(p => p.Favorite = true);
            }

            return View(topRatedPlayers);
        }

        [HttpGet]
        public async Task<IActionResult> PlayerBioAsync(string playerId)
        {
            // check cache first before making http request
            var playerModel = _cacheService.CacheTryGetValue(playerId);

            if (playerModel != null)
            {
                return Json(playerModel);
            }

            var urlParams = $"/players.PlayerProfileBio?playerid={playerId}";
            var client = _clientFactory.CreateClient();
            var results = await client.GetFromJsonAsync<PlayerModel>($"{_baseUrl}{urlParams}");

            // update cache
            _cacheService.SetCacheValue(playerId, results);

            return Json(results);
        }

        [HttpPost]
        public IActionResult UpdateFavorite(Player player, bool selected)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var success = _favoriteService.UpdateFavorite(player, selected, remoteIpAddress.ToString());

            return Json(success);
        }
    }
}
