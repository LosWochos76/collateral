using System.Collections.Generic;

namespace SeminarManager.Model
{
      public interface IPersonRepository
      {
            List<Person> All(int from=0, int max=1000);
            Person ById(int id);
            void Save(Person obj);
            void Delete(int id);
            Person FindAdminByEmailAndPassword(LoginModel login);
      }
}