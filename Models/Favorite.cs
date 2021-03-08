using System.Collections.Generic;

namespace ATP.Models
{
    public class Favorite
    {
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
