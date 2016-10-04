using UnityEngine;
using System.Collections.Generic;

public class WindowLayerControler : MonoBehaviour
{
    [SerializeField]
    private Sprite[] letterSprites;
    private Dictionary<char, Sprite> charSprites;
    private Dictionary<char, char> specialChars;
    private SpriteRenderer[] dialogueCharRenderers, questionCharRenderers;

    [SerializeField]
    private Transform dialogue, question;
    private Transform dialogueCharField, questionCharField;

    private DialogueData dialogueData;
    private int dialoguePageIndex, questionPointerIndex;
    private IntroQuestionType introQuestionAnswerChoice;
    private string[] pagesToShow;

    private OverworldCamera overCam;

    #region Gameplay
    void Update()
    {
        if (StateMachine.CurrentPlayState == PlayState.Dialogue)// && dialogueData != null)
        {
            if (Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
            {
                // Intro Question
                if (StateMachine.CurrentDialogueState == DialogueState.StartQuestion)
                {
                    // 0 = Yes , 8 = No
                    if (questionPointerIndex == 0) { questionPointerIndex = 8; }
                    else { questionPointerIndex = 0; }
                    drawQuestionPointer(questionPointerIndex);
                }
                else if (StateMachine.CurrentDialogueState == DialogueState.BattleQuestion)
                {
                    // Todo make game
                }
            }

            if (Input.GetButtonDown("A"))
            {
                if (StateMachine.CurrentDialogueState == DialogueState.StartQuestion)
                {
                    if (questionPointerIndex == 0)
                    {
                        Debug.Log("Yes");
                        introQuestionAnswerChoice = IntroQuestionType.Yes;
                    }
                    else
                    {
                        Debug.Log("No");
                        introQuestionAnswerChoice = IntroQuestionType.No;
                    }
                }
                else if (StateMachine.CurrentDialogueState == DialogueState.BattleQuestion)
                {
                    // Todo battle pointer as response type
                }
                else
                {
                    dialoguePageIndex++;
                }
            }

            if (dialoguePageIndex < pagesToShow.Length)
            {
                drawDialogue(pagesToShow[dialoguePageIndex]);
            }
            else
            {
                nextDialogueState();
            }
        }
    }

    public void setDialogueData(DialogueData data)
    {
        dialogueData = data;
    }

    private void drawDialogue(string dialoguePage)
    {
        for (int i = 0; i < dialogueCharRenderers.Length; i++)
        {
            dialogueCharRenderers[i].sprite = charSprites[dialoguePage[i]];
        }
    }

    private void drawQuestionPointer(int index)
    {
        if (index == 0)
        {
            questionCharRenderers[0].sprite = charSprites['^'];
            questionCharRenderers[8].sprite = charSprites['~'];
        }
        else
        {
            questionCharRenderers[0].sprite = charSprites['~'];
            questionCharRenderers[8].sprite = charSprites['^'];
        }
    }

    // todo: Make this not suck as much.
    private void nextDialogueState()
    {
        if (StateMachine.CurrentDialogueState == DialogueState.StartInto && dialogueData.asksIntroQuestion)
        {
            StateMachine.setDialogueState(DialogueState.StartQuestion);
        }
        else if (StateMachine.CurrentDialogueState == DialogueState.StartQuestion)
        {
            StateMachine.setDialogueState(DialogueState.StartQuestionResponse);
        }
        else if (StateMachine.CurrentDialogueState == DialogueState.StartQuestionResponse)
        {
            StateMachine.setDialogueState(DialogueState.BattleIntro);
        }
        else if (StateMachine.CurrentDialogueState == DialogueState.BattleIntro)
        {
            StateMachine.setDialogueState(DialogueState.BattleQuestion);
        }
        // uhh....
    }

    void state_onDialogueStateChange(DialogueState prevState, DialogueState curState)
    {
        if (curState == DialogueState.StartQuestion)
        {
            question.gameObject.SetActive(true);
            questionPointerIndex = 0;
        }
        else
        {
            question.gameObject.SetActive(false);
        }

        // states with text
        switch (curState)
        {
            case DialogueState.StartInto:
                pagesToShow = sanitizeText(dialogueData.IntroText);
                break;
            case DialogueState.StartQuestionResponse:
                if (!dialogueData.asksIntroQuestion) { break; }
                pagesToShow = sanitizeText(dialogueData.IntroQuestionResponses[questionPointerIndex / 8].dialogue);
                if (dialogueData.correctIntroResponse == introQuestionAnswerChoice) { StateMachine.setDialogueState(DialogueState.StartInto); } // reset dialogue
                break;
            case DialogueState.BattleIntro:
                pagesToShow = sanitizeText(dialogueData.BattleIntroText);
                break;
            case DialogueState.BattleWin:
                break;
            case DialogueState.BattleLose:
                break;
            case DialogueState.BattleNeutral:
                break;
            case DialogueState.BattleRewardExplain:
                break;
            default:
                // Question times
                break;
        }
    }

    void state_onPlayStateChange(PlayState prevState, PlayState curState)
    {
        if (curState == PlayState.Dialogue)
        {
            transform.position = overCam.topCorner;
            dialogue.gameObject.SetActive(true);
            question.gameObject.SetActive(false);
        }
        else // disable all dialogue elements to be sure
        {
            dialogue.gameObject.SetActive(false);
            question.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Helpers
    Dictionary<char, Sprite> makeSpriteDictionary(Sprite[] spriteArray)
    {
        Dictionary<char, Sprite> dict = new Dictionary<char, Sprite>(spriteArray.Length);
        foreach (Sprite sprite in spriteArray)
        {
            dict.Add(sprite.name[0], sprite);
        }
        return dict;
    }

    SpriteRenderer[] makeRenderArray(Transform textField)
    {
        SpriteRenderer[] array = new SpriteRenderer[textField.childCount];
        for (int i = 0; i < textField.childCount; i++)
        {
            array[i] = textField.GetChild(i).GetComponent<SpriteRenderer>();
        }
        return array;
    }

    public string[] sanitizeText(string input)
    {
        string clean = "";
        input = input.ToUpper();
        //input = input.PadRight(72, '~');

        // 18 char per row x 4 rows = 72 chars - 1 for the arrow

        foreach (char character in input)
        {
            if (specialChars.ContainsKey(character))
            {
                clean += specialChars[character];
            }
            else
            {
                clean += character;
            }
        }

        int numPages = Mathf.CeilToInt(clean.Length / 71f);
        string[] pages = new string[numPages];
        clean = clean.PadRight(numPages * 71, '~');

        for (int i = 0; i < pages.Length; i++)
        {

            if (clean.Length < 71)
            {
                pages[i] = clean.PadRight(71, '~') + '$';
            }
            else
            {
                string sub = clean.Substring((i * 71), 71);
                if (sub.Length < 71)
                {
                    pages[i] = clean.PadRight(71, '~') + '$';
                }
                else
                {
                    pages[i] = sub + '$';
                }
            }

        }

        foreach (string page in pages)
        {
            Debug.Log(page + " - " + page.Length);
        }
        return pages;
    }

    public void setOverworldCam(OverworldCamera cam)
    {
        overCam = cam;
    }
    #endregion

    #region MonoBehaviours

    void Awake()
    {
        if (dialogue != null) { dialogueCharField = dialogue.GetChild(0); }
        if (question != null) { questionCharField = question.GetChild(0); }

        dialogueCharRenderers = makeRenderArray(dialogueCharField);
        questionCharRenderers = makeRenderArray(questionCharField);
        charSprites = makeSpriteDictionary(letterSprites);

        specialChars = new Dictionary<char, char>
        {
            {' ','~' },
            {'?','@' },
            {':','[' },
            {';',']' },
            {'/','&' }
        };

        dialoguePageIndex = 0;
    }

    void OnEnable()
    {
        StateMachine.onPlayStateChanged += state_onPlayStateChange;
        StateMachine.onDialogueStateChanged += state_onDialogueStateChange;
    }

    void OnDisable()
    {
        StateMachine.onPlayStateChanged -= state_onPlayStateChange;
        StateMachine.onDialogueStateChanged -= state_onDialogueStateChange;
    }
    #endregion
}
