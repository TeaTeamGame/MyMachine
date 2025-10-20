namespace Generals.StateMachine.State
{
    public interface IState
    {
        void OnEnter();
        void OnExit();

        void OnUpdate(float deltaTime);
    }
}