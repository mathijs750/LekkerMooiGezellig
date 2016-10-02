using UnityEngine;
using System.Collections;

public class WindowLayerControler : MonoBehaviour
{
    [SerializeField]
    private Transform dialogue, yesNo;
    private Transform dialogueTextArea, yesNoTextArea;

    private GameObject[] dialogueTextCharacters;


    #region Gameplay
    public void setDialogue(string dialogue)
    {
        setTextInField(dialogue, dialogueTextCharacters);
    }


    void setTextInField(string text, GameObject[] field)
    {
        if (text.Length >= field.Length) { text = text.Substring(0, field.Length); }
        else { text = text.PadRight(field.Length); }

        for (int i =0; i < field.Length; i++)
        {
            field[i].name = "" + text[i];
        }

        field[field.Length - 1].name = "<X>";
    }

    void state_onPlayStateChange(PlayState prevState, PlayState curState)
    {
        if (curState == PlayState.Dialogue)
        {
            dialogue.gameObject.SetActive(true);
        }
        else // disable all dialogue elements to be sure
        {
            dialogue.gameObject.SetActive(false);
            yesNo.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Helpers
    GameObject[] makeTextArray(Transform textField)
    {
        GameObject[] array = new GameObject[textField.childCount];
        for (int i = 0; i < textField.childCount; i++)
        {
            array[i] = textField.GetChild(i).gameObject;
        }
        return array;
    }
    #endregion

    #region MonoBehaviours

    void Awake()
    {
        if (dialogue != null) { dialogueTextArea = dialogue.GetChild(0); }
        if (yesNo != null) { yesNoTextArea = yesNo.GetChild(0); }

        dialogueTextCharacters = makeTextArray(dialogueTextArea);
        Debug.Log("texArealength: " + dialogueTextCharacters.Length);
    }

    void OnEnable()
    {
        StateMachine.onPlayStateChanged += state_onPlayStateChange;
    }

    void OnDisable()
    {
        StateMachine.onPlayStateChanged -= state_onPlayStateChange;
    }
    #endregion
}
