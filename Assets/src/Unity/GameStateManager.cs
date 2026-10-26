using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Menu, Playing, GameOver
    }

    public static GameStateManager Instance {  get; private set; }
    public event Action<GameState> OnGameStateChanged;

    private GameState _state;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }


        Instance = this;
    }

    public void ChangeState(GameState state)
    {
        if (_state == state) 
            return;

        _state = state;
        OnGameStateChanged?.Invoke(state);
    }
}
