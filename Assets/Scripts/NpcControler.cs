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

[RequireComponent(typeof(DialogueData))]
public class NpcControler : MonoBehaviour
{
    private SpriteControler spriteCon;
    private DialogueData data;

    /*
    public bool asksOverworldQuestion;
    public string overworldDialogue;
    public NpcOverworldQuestion[] question;
    public QuestionType correctAnswer;
    
    [SerializeField]
    private ResponseType preferedResponse;
    [SerializeField]
    private ResponseType negativeResponse;

    [SerializeField]
    private NpcResponse[] responses;
    */
   
    
    public void Interact(Vector3 lookPos)
    {
        // TODO: look at the player when spoken to

        Debug.Log("!");
        GameManager.Instance.activateDialogue(this);
    }

    void Awake()
    {
        spriteCon = transform.GetChild(0).GetComponent<SpriteControler>();
    }

}
