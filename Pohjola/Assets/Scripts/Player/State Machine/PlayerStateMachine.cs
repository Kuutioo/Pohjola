using System;
using UnityEngine;
public abstract class PlayerStateMachine : MonoBehaviour
{
    protected PlayerState playerState;

    public void SetState(PlayerState _playerState)
    {
        playerState = _playerState;
        playerState.ChangeState();
    }
}
