namespace Objective
{
    public class ObjectiveHandler
    {
        public delegate void ObjectiveReached();

        public static ObjectiveReached OnObjectiveReached;
    }
}