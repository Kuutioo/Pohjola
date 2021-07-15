public abstract class PlayerState
{
    protected PlayerController playerController;

    public PlayerState(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public virtual void ChangeState()
    {
        return;
    }
}
