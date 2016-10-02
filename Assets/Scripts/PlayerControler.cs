using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
    [SerializeField, Range(1, 20f)]
    private float walkSpeed = 1f;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private bool isColliding;

    // Use this for initialization
    void OnEnable()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateMachine.CurrentGameState == GameState.Playing && StateMachine.CurrentPlayState == PlayState.OverWorld)  //NPC = layer 8
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * walkSpeed);
            //transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * (Time.deltaTime * walkSpeed);
            //Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            /*
            if (!isTouchingInDirection(moveDirection))
            {
                transform.position += new Vector3(moveDirection.x, moveDirection.y, 0);
            }*/
        }


    }
    /*
    bool isTouchingInDirection(Vector2 direction)
    {
        int layerMask = 1 << 9;
        float angle = Vector2.Angle(Vector2.right, direction);
        angle = Mathf.RoundToInt(angle);



       // if (Physics2D.Raycast())

        return false;
    }


    Vector3 allignToGrid(Vector3 inPos)
    {
        inPos.x = Mathf.Round(inPos.x);
        inPos.y = Mathf.Round(inPos.y);
        inPos.z = Mathf.Round(inPos.z);
        return inPos;
    }
    */

    public void OnCollisionEnter2D(Collision2D coll)
    {
        isColliding = true;
        Debug.Log(coll.gameObject.name);
        GameManager.Instance.activateDialogue(coll.gameObject);
    }

    public void OnCollisonExit2D(Collision2D coll)
    {
        isColliding = false;
        Debug.Log("HO!");
    }


    /*
    Vector3 ManhatanizeVector3(Vector3 movement, float intervalSize)
    {
        Vector3 manVec = new Vector3();
        if (movement.x <= 0)
        {
            manVec.x = Mathf.Floor(movement.x / intervalSize) * intervalSize;
        }
        else
        {
            manVec.x = Mathf.Ceil(movement.x / intervalSize) * intervalSize;
        }

        if (movement.y <= 0)
        {
            manVec.y = Mathf.Floor(movement.y / intervalSize) * intervalSize;
        }
        else
        {
            manVec.y = Mathf.Ceil(movement.y / intervalSize) * intervalSize;
        }

        manVec.z = movement.z;
        return manVec;
    }
    */

}
