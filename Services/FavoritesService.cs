using ATP.Data;
using ATP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATP.Services
{
    public interface IFavoritesService
    {
        bool UpdateFavorites(IList<Player> players, int favoriteId);
        bool UpdateFavorite(Player player, bool selected, string ipAddress);
        Favorite GetFavorite(string ipAddress);
    }

    public class FavoritesService : IFavoritesService
    {
        private readonly FavoritesContext _context;
        private readonly ILogger<FavoritesService> _logger;

        public FavoritesService(ILogger<FavoritesService> logger, FavoritesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Favorite GetFavorite(string ipAddress)
        {
            return _context.Set<Favorite>()
             .Include(f => f.Players)
             .FirstOrDefault(f => f.IPAddress.Equals(ipAddress));
        }

        public bool UpdateFavorites(IList<Player> players, int favoriteId)
        {
            try
            {
                var favorite = _context.Set<Favorite>()
                    .Include(f => f.Players)
                    .FirstOrDefault(f => f.ID == favoriteId);

                if (favorite == null)
                {
                    // technically this should never happen, since we are already updating favorite players
                    return false;
                }

                // update favRank received from UI
                foreach (var player in favorite.Players)
                {
                    var rank = players.First(p => p.ID == player.ID).FavRank;
                    player.FavRank = rank;
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update favorite", ex);
                return false;
            }
        }
        public bool UpdateFavorite(Player player, bool selected, string ipAddress)
        {
            try
            {
                var favorite = _context.Set<Favorite>()
                    .Include(f => f.Players)
                    .FirstOrDefault(f => f.IPAddress.Equals(ipAddress));

                // check if favorite for given IP exist, if not, create a new instance with emppy list of players
                if (favorite == null)
                {
                    favorite = new Favorite
                    {
                        IPAddress = ipAddress,
                        Players = new List<Player>()
                    };
                    _context.Add(favorite);
                }

                // if adding a new player to favorites
                if (selected)
                {
                    // make sure same player doesnt already exists - shouldnt
                    if (!favorite.Players.Any(p => p.PlayerId == player.PlayerId))
                    {
                        // need to keep track of FavRank
                        player.FavRank = favorite.Players.Count + 1;
                        favorite.Players.Add(player);
                    }
                }
                else
                {
                    var playerToRemove = favorite.Players.SingleOrDefault(p => p.PlayerId == player.PlayerId);
                    if (playerToRemove != null)
                    {
                        favorite.Players.Remove(playerToRemove);
                        _context.Players.Remove(playerToRemove);
                    }

                    // update favRanks 
                    var sortedPlayes = favorite.Players.OrderBy(p => p.FavRank).ToList();
                    var i = 1;
                    foreach (var sp in sortedPlayes)
                    {
                        sp.FavRank = i++;
                    }
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update favorite", ex);
                return false;
            }
        }
    }
}
