using UnityEngine;
using System.Collections;


public class PlayerControler : MonoBehaviour
{
    [SerializeField, Range(1, 20f)]
    private float StepTreshold = 1f;

    private SpriteControler spriteCon;
    private Vector2 newPos;
    /// Left w, Down x, Right y, Up z
    private Vector4 times = Vector4.zero;
    private bool isMoving;

    // Update is called once per frame
    void Update()
    {
        if (StateMachine.CurrentGameState == GameState.Playing && StateMachine.CurrentPlayState == PlayState.OverWorld)  //NPC = layer 8
        {
            Vector2 input = movementInput();
            if (isNotBlocked(input)) { newPos += input; }

            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime + .2f);
            if (Mathf.Floor(Vector3.Magnitude(transform.position - new Vector3(newPos.x, newPos.y, 0)) * 1000) > 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
                transform.position = newPos;
            }

        }
    }



    void OnEnable()
    {
        newPos = transform.position;
        spriteCon = transform.GetChild(0).GetComponent<SpriteControler>();
        isMoving = false;
    }

    private bool isNotBlocked(Vector2 input)
    {
        // the "most" best way
        RaycastHit2D hitA = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0.5f, 0), input, 1, 1 << 9);
        RaycastHit2D hitB = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0.5f, 0), input, 1, 1 << 9);
        RaycastHit2D hitC = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -0.5f, 0), input, 1, 1 << 9);
        RaycastHit2D hitD = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.5f, 0), input, 1, 1 << 9);

        if (hitA || hitB || hitC || hitD)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Vector2 movementInput()
    {
        Vector2 pos = Vector2.zero;

        // Button ups
        //////////////

        if (Input.GetButtonUp("Left"))
        {
            if (times.w > 0 && !isMoving)
            {
                spriteCon.Direction = spriteDirection.left;
            }
            times.w = 0;
        }

        else if (Input.GetButtonUp("Right"))
        {
            {
                spriteCon.Direction = spriteDirection.right;
            }
            times.y = 0;

        }

        if (Input.GetButtonUp("Up"))
        {
            if (times.z > 0 && !isMoving)
            {
                spriteCon.Direction = spriteDirection.up;
            }
            times.z = 0;

        }

        else if (Input.GetButtonUp("Down"))
        {
            if (times.x > 0 && !isMoving)
            {
                spriteCon.Direction = spriteDirection.down;
            }
            times.x = 0;

        }

        // Button presses
        /////////////////

        if (Input.GetButton("Left"))
        {
            times.w++;
            if (times.w > StepTreshold)
            {
                isMoving = true;
                spriteCon.Direction = spriteDirection.left;
                pos += Vector2.left;
                times.w = 0;
            }
        }
        else if (Input.GetButton("Right"))
        {
            times.y++;
            if (times.y > StepTreshold)
            {
                isMoving = true;
                spriteCon.Direction = spriteDirection.right;
                pos += Vector2.right;
                times.y = 0;
            }
        }
        else if (Input.GetButton("Up"))
        {
            times.z++;
            if (times.z > StepTreshold)
            {
                isMoving = true;
                spriteCon.Direction = spriteDirection.up;
                pos += Vector2.up;
                times.z = 0;
            }
        }
        else if (Input.GetButton("Down"))
        {
            times.x++;
            if (times.x > StepTreshold)
            {
                isMoving = true;
                spriteCon.Direction = spriteDirection.down;
                pos += Vector2.down;
                times.x = 0;
            }
        }

        return pos;
    }

}
