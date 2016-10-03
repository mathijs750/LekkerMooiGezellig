using UnityEngine;
using System.Collections;


public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private float StepTreshold = 1f;
    [SerializeField]
    private float stepSpeed = 1f;
    [SerializeField]
    private float runModifier = 1f;

    private SpriteControler spriteCon;
    private Vector3 newPosition, lastLookDirection;
    /// Left w, Down x, Right y, Up z
    private Vector4 times = Vector4.zero;
    private bool isMoving;


    // Update is called once per frame
    void Update()
    {
        if (StateMachine.CurrentGameState == GameState.Playing && StateMachine.CurrentPlayState == PlayState.OverWorld)  //NPC = layer 9
        {
            // Get a unit vector based on key input and save it for later use
            Vector3 inputDirection = dpadInput();
            if (inputDirection.magnitude > 0.1f)
            {
                lastLookDirection = inputDirection;
            }
            if (isFreeToMove(inputDirection))
            {
                if (Input.GetButton("B")) { newPosition += inputDirection * runModifier; }
                else { newPosition += inputDirection; }
            }

            // Check is the lerp is close to 0 to align to the grid
            float dist = Mathf.Abs(Vector3.Distance(transform.position, newPosition));
            if (dist <= 0.1)
            {
                isMoving = false;
                transform.position = newPosition;
            }
            else
            {
                isMoving = true;
                if (Input.GetButton("B"))
                {
                    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * stepSpeed);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * stepSpeed);
                }
            }


            if (Input.GetButtonDown("A"))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, lastLookDirection, 2, 1 << 9);
                if (hit)
                {
                    if (hit.collider.tag == "CanSpeak")
                    {
                        hit.transform.GetComponent<NpcControler>().Interact(transform.position);
                    }
                }
            }
        }
    }


    void OnEnable()
    {
        spriteCon = transform.GetChild(0).GetComponent<SpriteControler>();
        newPosition = transform.position;
        isMoving = false;
    }

    private bool isFreeToMove(Vector2 input)
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

    private Vector2 dpadInput()
    {
        // Button ups
        //////////////

        if (Input.GetButtonUp("Left") && !isMoving)
        {
            if (times.w > 0.1f)
            {
                spriteCon.Direction = spriteDirection.left;
            }
            times.w = 0;
        }
        else if (Input.GetButtonUp("Right") && !isMoving)
        {
            if (times.y > 0.1f)
            {
                spriteCon.Direction = spriteDirection.right;
            }
            times.y = 0;
        }
        else if (Input.GetButtonUp("Up") && !isMoving)
        {
            if (times.z > 0.1f)
            {
                spriteCon.Direction = spriteDirection.up;
            }
            times.z = 0;
        }
        else if (Input.GetButtonUp("Down") && !isMoving)
        {
            if (times.x > 0.1f)
            {
                spriteCon.Direction = spriteDirection.down;
            }
            times.x = 0;
        }

        // Button presses
        /////////////////

        if (Input.GetButton("Left"))
        {
            if (times.w > StepTreshold)
            {
                spriteCon.Direction = spriteDirection.left;
                times = Vector4.zero;
                return Vector2.left;
            }
            times.w++;
        }
        else if (Input.GetButton("Right"))
        {
            if (times.y > StepTreshold)
            {
                spriteCon.Direction = spriteDirection.right;
                times = Vector4.zero;
                return Vector2.right;
            }
            times.y++;
        }
        else if (Input.GetButton("Up"))
        {
            if (times.z > StepTreshold)
            {
                spriteCon.Direction = spriteDirection.up;
                times = Vector4.zero;
                return Vector2.up;
            }
            times.z++;
        }
        else if (Input.GetButton("Down"))
        {
            if (times.x > StepTreshold)
            {
                spriteCon.Direction = spriteDirection.down;
                times = Vector4.zero;
                return Vector2.down;
            }
            times.x++;
        }

        return Vector2.zero;
    }

}
