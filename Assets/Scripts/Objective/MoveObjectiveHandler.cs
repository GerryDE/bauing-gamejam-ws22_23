using Data.objective;

namespace Objective
{
    public class MoveObjectiveHandler : ObjectiveHandler
    {
        private MoveObjectiveData _data;
        private bool _movedLeft;
        private bool _movedRight;

        public MoveObjectiveHandler(MoveObjectiveData data)
        {
            _data = data;

            GameInputHandlerComponent.OnMoveCalled += OnMoveCalled;
        }

        private void OnMoveCalled(float direction)
        {
            _movedLeft = direction < 0f || _movedLeft;
            _movedRight = direction > 0f || _movedRight;

            var moveLeftGoalReached = (_data.shallMoveLeft && _movedLeft) || !_data.shallMoveLeft;
            var moveRightGoalReached = (_data.shallMoveRight && _movedRight) || !_data.shallMoveRight;

            if (moveLeftGoalReached && moveRightGoalReached)
            {
                OnObjectiveReached?.Invoke(_data.GetType());
                GameInputHandlerComponent.OnMoveCalled -= OnMoveCalled;
            }
        }
    }
}