using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        StateMachine.onGameStateChanged += onGameStateChange;
        StateMachine.setGameState(GameState.MainMenu); // for debugging

        StateMachine.setGameState(GameState.Playing); // for debugging
        StateMachine.setPlayState(PlayState.OverWorld);
    }

    void OnDisable()
    {
        StateMachine.onGameStateChanged -= onGameStateChange;
    }

    void Start()
    {
    }


    void Update()
    {

    }

    private void onGameStateChange(GameState prevState, GameState curState)
    {
        if (prevState == GameState.MainMenu && curState == GameState.Playing)
        {
            Debug.Log("The Game is started");
        }
    }
}
