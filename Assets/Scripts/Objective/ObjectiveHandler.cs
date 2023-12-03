using Data.objective;

namespace Objective
{
    public class ObjectiveHandler
    {
        public delegate void ObjectiveReached(ObjectiveData data);

        public static ObjectiveReached OnObjectiveReached;
    }
}