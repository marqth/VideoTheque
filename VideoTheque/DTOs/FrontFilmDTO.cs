namespace VideoTheque.DTOs
{
    public class FrontFilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string MainActor { get; set; }
        public string Director { get; set; }
        public string Scenarist { get; set; }
        public string AgeRating { get; set; }
        public string Genre { get; set; }
        public string Support { get; set; }
        public bool IsAvailable { get; set; }
        public int? IdOwner { get; set; }
    }
}
