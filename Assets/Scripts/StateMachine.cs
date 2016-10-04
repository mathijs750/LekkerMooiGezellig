using UnityEngine;
using System.Collections;

public enum GameState
{
    /// <summary>
    /// Default state when the game is started
    /// </summary>
    Start,
    /// <summary>
    /// The state when interacting with the main menu
    /// </summary>
    MainMenu,
    /// <summary>
    /// The main gameplay loop
    /// </summary>
    Playing,
    /// <summary>
    /// Interuption of the gameplay loop
    /// </summary>
    Paused,
    /// <summary>
    /// The end of the game when the player dies
    /// </summary>
    Death,
    /// <summary>
    /// The end of the game when the player defeats the final encounter
    /// </summary>
    Won
}

public enum PlayState
{
    /// <summary>
    /// Walking around and able to interact with objects
    /// </summary>
    OverWorld,
    /// <summary>
    /// No movement only dialogue and yes no options
    /// </summary>
    Dialogue,
    /// <summary>
    /// The Lekker mooi gezellig part of the game. Takes place in seperate part of world
    /// </summary>
    Battle
}

public enum DialogueState
{
    StartInto,
    StartQuestion,
    StartQuestionResponse,
    BattleIntro,
    BattleQuestion,
    BattleWin,
    BattleLose,
    BattleNeutral,
    BattleRewardExplain
}

public class StateMachine : MonoBehaviour
{
    public delegate void GameStateChanged(GameState prevState, GameState newState);
    public delegate void PlayStateChanged(PlayState prevState, PlayState newState);
    public delegate void DialogueStateChanged(DialogueState prevState, DialogueState newState);

    public static event GameStateChanged onGameStateChanged;
    public static event PlayStateChanged onPlayStateChanged;
    public static event DialogueStateChanged onDialogueStateChanged;

    private static GameState gameState;
    private static PlayState playState;
    private static DialogueState dialogueState;

    public static GameState CurrentGameState { get { return gameState; }}
    public static PlayState CurrentPlayState { get { return playState; }}
    public static DialogueState CurrentDialogueState { get { return dialogueState; } }

    public static void setGameState(GameState newState)
    {
        GameState prevState = gameState;
        gameState = newState;
    
        if (onGameStateChanged != null) { onGameStateChanged(prevState, gameState); }
    }

    public static void setPlayState(PlayState newState)
    {
        PlayState prevState = playState;
        playState = newState;

        if (onPlayStateChanged != null) { onPlayStateChanged(prevState, playState); }
    }

    public static void setDialogueState(DialogueState newState)
    {
        DialogueState prevState = dialogueState;
        dialogueState = newState;

        if (onDialogueStateChanged != null) { onDialogueStateChanged(prevState, dialogueState); }
    }
}
