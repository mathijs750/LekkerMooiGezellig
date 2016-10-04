using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DialogueData))]
public class NpcControler : MonoBehaviour
{
    [SerializeField]
    private DialogueData dialogueData;
    private SpriteControler spriteCon;

    public void Interact(Vector3 lookPos)
    {
        // TODO: look at the player when spoken to

        Debug.Log("!");
        GameManager.Instance.activateDialogue(dialogueData, gameObject);
    }

    void Awake()
    {
        spriteCon = transform.GetChild(0).GetComponent<SpriteControler>();
    }

}
