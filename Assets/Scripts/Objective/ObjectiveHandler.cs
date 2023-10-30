namespace Objective
{
    public class ObjectiveHandler
    {
        public delegate void ObjectiveReached(System.Type type);

        public static ObjectiveReached OnObjectiveReached;
    }
}