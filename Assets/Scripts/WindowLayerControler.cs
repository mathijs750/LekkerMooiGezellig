using UnityEngine;
using System.Collections.Generic;

public class WindowLayerControler : MonoBehaviour
{
    [SerializeField]
    private OverworldCamera cam;
    [SerializeField]
    private Sprite[] letterSprites;
    private Dictionary<char, Sprite> charSprites;
    private Dictionary<char, char> specialChars;
    private SpriteRenderer[] dialogueCharSprites;

    [SerializeField]
    private Transform dialogue, yesNo;
    private Transform dialogueTextArea, yesNoTextArea;

    private int dialogueIndex;
    private string[] dialogueToShow;

    #region Gameplay
    void Update()
    {
        if (StateMachine.CurrentPlayState == PlayState.Dialogue && dialogueToShow != null)
        {
            if (Input.GetButtonDown("A"))
            {
                dialogueIndex++;
                if (dialogueIndex < dialogueToShow.Length)
                {
                    setTextInField(dialogueToShow[dialogueIndex], dialogueCharSprites);
                }
                else
                {
                    StateMachine.setPlayState(PlayState.OverWorld);
                    dialogueIndex = 0;
                    dialogueToShow = null;
                }
            }
        }
    }

    public void setDialogue(string dialogue)
    {
        dialogueToShow = sanitizeText(dialogue);
        setTextInField(dialogueToShow[0], dialogueCharSprites); // for now only one page
        dialogueIndex = 0;
    }


    void setTextInField(string text, SpriteRenderer[] field)
    {
        for (int i = 0; i < field.Length; i++)
        {
            field[i].sprite = charSprites[text[i]];
        }
    }

    void state_onPlayStateChange(PlayState prevState, PlayState curState)
    {
        if (curState == PlayState.Dialogue)
        {
            transform.position = cam.topCorner;
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
    Dictionary<char, Sprite> makeSpriteDictionary(Sprite[] spriteArray)
    {
        Dictionary<char, Sprite> dict = new Dictionary<char, Sprite>(spriteArray.Length);
        foreach (Sprite sprite in spriteArray)
        {
            //Debug.Log(sprite.name);
            dict.Add(sprite.name[0], sprite);
        }
        return dict;
    }

    SpriteRenderer[] makeTextArray(Transform textField)
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
    #endregion

    #region MonoBehaviours

    void Awake()
    {
        if (dialogue != null) { dialogueTextArea = dialogue.GetChild(0); }
        if (yesNo != null) { yesNoTextArea = yesNo.GetChild(0); }

        dialogueCharSprites = makeTextArray(dialogueTextArea);
        charSprites = makeSpriteDictionary(letterSprites);

        specialChars = new Dictionary<char, char>
        {
            {' ','~' },
            {'?','@' },
            {':','[' },
            {';',']' },
            {'/','&' }
        };

        dialogueIndex = 0;
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
