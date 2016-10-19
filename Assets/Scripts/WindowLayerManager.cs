using UnityEngine;
using System.Collections.Generic;

public class WindowLayerManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] letterSprites;
    private Dictionary<char, Sprite> charSprites;
    private Dictionary<char, char> specialChars;

    private SpriteRenderer[] dialogueCharRenderers, inventoryCharRenderers;
    private SpriteRenderer[] questionPointRenderers, inventoryPointRenderers, battlePointRenderers;

    private DialogueData dialogueData;
    private OverworldCamera overCam;

    public DialogueData Data { set { dialogueData = value; } }

    public delegate void movePointer(int direction);
    public static event movePointer on_movePointer;

    void Awake()
    {
        dialogueCharRenderers = makeRenderArray(transform.GetChild(0).GetChild(0));
        questionPointRenderers = makeRenderArray(transform.GetChild(1).GetChild(1));
        battlePointRenderers = makeRenderArray(transform.GetChild(2).GetChild(1));
        inventoryCharRenderers = makeRenderArray(transform.GetChild(3).GetChild(0));
        inventoryPointRenderers = makeRenderArray(transform.GetChild(3).GetChild(1));

        charSprites = makeSpriteDictionary(letterSprites);

        specialChars = new Dictionary<char, char>
        {
            {' ','~' },
            {'?','@' },
            {':','[' },
            {';',']' },
            {'/','&' },
            {'"','=' },
        //  {'"','%' }
        };
    }

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

        int numPages = Mathf.CeilToInt(clean.Length / 72f);
        string[] pages = new string[numPages];
        clean = clean.PadRight(numPages * 72, '~');

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i] = clean.Substring((i * 72), 72);
        }

        foreach (string page in pages)
        {
            Debug.Log("WinMan(sanitize Text)" +page + " - " + page.Length);
        }
        return pages;
    }



    void state_onDialogueStateChange(DialogueState prevState, DialogueState curState)
    {

    }

    void state_onBattleStateChange(BattleState prevState, BattleState curState)
    {

    }

    void state_onPlayStateChange(PlayState prevState, PlayState curState)
    {
        if (curState == PlayState.Dialogue)
        {
            transform.position = overCam.topCorner;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (curState == PlayState.Battle) {
            transform.position = overCam.topCorner;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (curState == PlayState.Inventory)
        {
            transform.position = overCam.topCorner;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        StateMachine.onPlayStateChanged += state_onPlayStateChange;
        StateMachine.onDialogueStateChanged += state_onDialogueStateChange;
        StateMachine.onBattleStateChanged += state_onBattleStateChange;
    }

    void OnDisable()
    {
        StateMachine.onPlayStateChanged -= state_onPlayStateChange;
        StateMachine.onDialogueStateChanged -= state_onDialogueStateChange;
        StateMachine.onBattleStateChanged -= state_onBattleStateChange;
    }
}
