using UnityEngine;
using System.Collections.Generic;

public enum PlayerResponseType
{
    Default,
    Lekker,
    Mooi,
    Gezellig,
    Lekker_s,
    Mooi_s,
    Gezellig_s
}

public enum IntroQuestionType
{
    Yes,
    No
}

[System.Serializable]
public struct IntroQuestion
{
    public IntroQuestionType type;
    public string dialogue;
}

[System.Serializable]
public struct NpcBattleResponse
{
    public PlayerResponseType type;
    public string dialogue;
}

[System.Serializable]
public class DialogueData : MonoBehaviour
{
    public string IntroText;
    public bool asksIntroQuestion;
    public IntroQuestion[] IntroQuestionResponses;
    public IntroQuestionType correctIntroResponse;
    public string BattleIntroText;
    public PlayerResponseType winResponse, loseResponse;
    public NpcBattleResponse[] battleResponses;
    public int battleReward; // for now
    public string rewardExplainText;
}
