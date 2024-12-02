namespace VideoTheque.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int IdFirstActor { get; set; }
        public int IdDirector { get; set; }
        public int IdScenarist { get; set; }
        public int IdAgeRating { get; set; }
        public int IdGenre { get; set; }
        public bool IsAvailable { get; set; }
        public int IdOwner { get; set; }
    }
}