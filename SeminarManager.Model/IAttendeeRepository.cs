using System.Collections.Generic;

namespace SeminarManager.Model
{
    public interface IAttendeeRepository
    {
        void Save(int seminar_id, List<int> attendees);
        List<int> Get(int seminar_id);
    }
}