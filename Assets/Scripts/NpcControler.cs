using UnityEngine;
using System.Collections;

public enum ResponseType
{
    Default,
    Lekker,
    Mooi,
    Gezellig,
    Lekker_s,
    Mooi_s,
    Gezellig_s
}

public enum QuestionType
{
    Yes,
    No
}

[System.Serializable]
public struct NpcResponse
{
    public ResponseType type;
    public string dialogue;
}

[System.Serializable]
public struct NpcOverworldQuestion
{
    public QuestionType type;
    public string dialogue;
}

public class NpcControler : MonoBehaviour
{
    [SerializeField]
    private bool asksOverworldQuestion;
    [SerializeField]
    private string overworldDialogue;
    [SerializeField]
    private NpcOverworldQuestion[] question;
    [SerializeField]
    private QuestionType correctAnswer;
    /*
    [SerializeField]
    private ResponseType preferedResponse;
    [SerializeField]
    private ResponseType negativeResponse;

    [SerializeField]
    private NpcResponse[] responses;
    */
    private SpriteControler spriteCon;
    
    public void Interact(Vector3 lookPos)
    {


        Debug.Log("!");
    }

    void Awake()
    {
        spriteCon = transform.GetChild(0).GetComponent<SpriteControler>();
    }


    void Update()
    {

    }
}
