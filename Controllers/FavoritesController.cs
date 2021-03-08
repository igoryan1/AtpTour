using ATP.Models;
using ATP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ATP.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favService)
        {
            _favoritesService = favService;

        }
        public IActionResult Index()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            var favorite = _favoritesService.GetFavorite(remoteIpAddress.ToString());

            if (favorite != null && favorite.Players.Any())
            {
                // sort by Favorite Rank for UI
                favorite.Players = favorite.Players.OrderBy(p => p.FavRank).ToList();
            }

            return View(favorite);
        }

        [HttpPost]
        public IActionResult UpdateFavorites(IList<Player> players, int favoriteId)
        {
            var updateResult = _favoritesService.UpdateFavorites(players, favoriteId);
            return Json(updateResult);
        }
    }
}
