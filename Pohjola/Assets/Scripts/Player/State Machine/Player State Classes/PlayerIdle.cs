public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerController _playerController) : base(_playerController)
    {
    }

    public override void ChangeState()
    {
        if (playerController.movementDirection.x == 0 && playerController.movementDirection.y == 0)
        {
            playerController.playerAnimations = PlayerAnimations.Player_Idle_Placeholder;
            playerController.ChangeAnimationState(playerController.playerAnimations.ToString());
        }
        else
        {
            playerController.SetState(new PlayerWalk(playerController));
        }
    }
}
