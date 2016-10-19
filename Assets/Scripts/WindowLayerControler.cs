using UnityEngine;
using System.Collections.Generic;

public class WindowLayerControler : MonoBehaviour
{
    private int dialoguePageIndex;
    private int questionPointerIndex, inventoryPointerIndex, battlePointerIndex;
    private string[] pagesToShow;

    public string[] TextPages { set { pagesToShow = value; } }

    void Awake()
    {

    }

    public void drawDialogue()
    {
        for (int i = 0; i < dialogueCharRenderers.Length; i++)
        {
            dialogueCharRenderers[i].sprite = charSprites[dialoguePage[i]];
        }
    }

    public void netxDialoguePage() { }


    private void drawPointers(int direction)
    {


        questionCharRenderers[1 - index].sprite = charSprites['~'];
        questionCharRenderers[index].sprite = charSprites['^'];
    }

    // todo: Make this not suck as much.
    private void nextDialogueState()
    {

    }

    public void OnEnable()
    {
        WindowLayerManager.on_movePointer += drawPointers;
    }

    public void OnDisable()
    {
        WindowLayerManager.on_movePointer -= drawPointers;
    }
}
