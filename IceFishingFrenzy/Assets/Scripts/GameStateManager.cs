using UnityEngine;

public enum GameState
{
    Idle,
    Casting,
    Dropping,
    Reeling,
    Scoring,
    UpgradeMenu
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState CurrentState = GameState.Idle;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
    }

    public void SetIdleState()
    {
        CurrentState = GameState.Idle;
    }

    public bool IsState(GameState state)
    {
        return CurrentState == state;
    }
}
