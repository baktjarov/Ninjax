namespace GameStates.Interfaces
{
    public interface IGameState
    {
        public void Enter();
        public void Exit();
        public void Update();
    }
}