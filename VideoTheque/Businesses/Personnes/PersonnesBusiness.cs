using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Personnes
{
    public class PersonnesBusiness : IPersonnesBusiness
    {
        private readonly IPersonnesRepository _personneDao;

        public PersonnesBusiness(IPersonnesRepository personneDao)
        {
            _personneDao = personneDao;
        }

        public Task<List<PersonneDto>> GetPersonnes() => _personneDao.GetPersonnes();

        public PersonneDto GetPersonne(int id)
        {
            var personne = _personneDao.GetPersonne(id).Result;

            if (personne == null)
            {
                throw new NotFoundException($"Personne '{id}' non trouvée");
            }

            return personne;
        }

        public Task InsertPersonne(PersonneDto personne) => _personneDao.InsertPersonne(personne);

        public Task UpdatePersonne(int id, PersonneDto personne) => _personneDao.UpdatePersonne(id, personne);

        public Task DeletePersonne(int id) => _personneDao.DeletePersonne(id);
    }
}