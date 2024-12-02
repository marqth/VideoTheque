using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Personnes
{
    public interface IPersonnesBusiness
    {
        Task<List<PersonneDto>> GetPersonnes();

        PersonneDto GetPersonne(int id);

        Task InsertPersonne(PersonneDto personne);

        Task UpdatePersonne(int id, PersonneDto personne);

        Task DeletePersonne(int id);
    }
}