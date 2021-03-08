namespace ATP.Models
{
    // Bio specific classes
    public class PlayerBio
    {
        public string PlayerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int HeroRank { get; set; }
        public bool DblSpecialist { get; set; }
        public string NatlId { get; set; }
        public string BioPersonal { get; set; }
        public string BioYearToDate { get; set; }
        public string BioCareerHighlights { get; set; }
        public string Type { get; set; }
    }

    public class PlayerProfileDetails
    {
        public string PlayerGladiatorUrl { get; set; }
        public string PlayerHeadshotUrl { get; set; }
        public bool HasGladiator { get; set; }
    }

}
