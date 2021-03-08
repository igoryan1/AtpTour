using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATP.Models
{
    public class Player
    {
        public int ID { get; set; }
        [NotMapped]
        public bool Favorite { get; set; }
        public int FavRank { get; set; }

        public string PlayerId { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string NatlId { get; set; }
        [Display(Name = "Country")]
        public string CountryName { get; set; }
        public int AgeAtRankDate { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public bool IsTie { get; set; }
        public int NbrEventsPlayed { get; set; }
        public int PrevRank { get; set; }
        public int PrevPoints { get; set; }
        public int PointsDropping { get; set; }
        public int NextBestPoints { get; set; }
        public int LastWeekPosMove { get; set; }
        public string PlayerGladiatorImageCmsUrl { get; set; }
        [Display(Name = "Picture")]
        public string PlayerHeadshotImageCmsUrl { get; set; }
    }

}
