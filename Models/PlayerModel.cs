using System;
using System.Collections.Generic;

namespace ATP.Models
{
    public class PlayerModel
    {
        public Content Content { get; set; }
        public Data Data { get; set; }
    }

    public class Content 
    {
        public object Advert { get; set; }
        public object RankingsContentModel { get; set; }
        public PlayerProfileDetails PlayerProfileDetails { get; set; }
    }

    public class Ranking
    {
        public DateTime RankDate { get; set; }
        public string Type { get; set; }
        public List<Player> Players { get; set; }
    }

    public class Data : PlayerBio
    {
        public Ranking Rankings { get; set; }
    }

}
