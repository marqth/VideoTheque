namespace VideoTheque.DTOs
{
    public class BluRayDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Duration { get; set; }
        public int FirstActorId { get; set; }
        public int DirectorId { get; set; }
        public int ScenaristId { get; set; }
        public int AgeRatingId { get; set; }
        public int GenreId { get; set; }
        public bool IsAvailable { get; set; }
        public int? IdOwner { get; set; }
    }
}