using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private WindowLayerControler windowLayer;

    public static GameManager Instance = null;

    public void activateDialogue(NpcControler npc)
    {
        StateMachine.setPlayState(PlayState.Dialogue);
        windowLayer.setDialogue("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc elit risus, facilisis a hendrerit eleifend, cursus vel mauris. Mauris pellentesque.");

    }

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);

        // Application.targetFrameRate = 59; // 59.73fps
        StateMachine.onGameStateChanged += onGameStateChange;
        StateMachine.onPlayStateChanged += onPlayStateChange;
        StateMachine.setGameState(GameState.MainMenu); // for debugging

        StateMachine.setGameState(GameState.Playing); // for debugging
        StateMachine.setPlayState(PlayState.OverWorld);
    }

    void OnDisable()
    {
        StateMachine.onGameStateChanged -= onGameStateChange;
        StateMachine.onPlayStateChanged -= onPlayStateChange;
    }


    private void onGameStateChange(GameState prevState, GameState curState)
    {
        if (prevState == GameState.MainMenu && curState == GameState.Playing)
        {
           // Debug.Log("The Game is started");
        }
    }

    private void onPlayStateChange(PlayState prevState, PlayState curState)
    {
        if (prevState == PlayState.OverWorld && curState == PlayState.Dialogue)
        {
           // Debug.Log("BABBEL TIJD!!!!!");
        }
    }
}
