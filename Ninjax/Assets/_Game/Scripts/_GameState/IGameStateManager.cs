namespace GameStates.Interfaces
{
    public interface IGameStatesManager
    {
        public void ChangeState(IGameState newState);
    }
}