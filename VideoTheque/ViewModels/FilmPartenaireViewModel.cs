namespace VideoTheque.ViewModels
{
    public class FilmPartenaireViewModel
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public GenrePartenaireViewModel Genre { get; set; }
        public PersonnePartenaireViewModel Director { get; set; }
        public PersonnePartenaireViewModel FirstActor { get; set; }
        public PersonnePartenaireViewModel Scenarist { get; set; }
        public AgeRatingPartenaireViewModel AgeRating { get; set; }
    }

    public class GenrePartenaireViewModel
    {
        public string Name { get; set; }
    }

    public class PersonnePartenaireViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class AgeRatingPartenaireViewModel
    {
        public string Name { get; set; }
        public string Abreviation { get; set; }
    }
}