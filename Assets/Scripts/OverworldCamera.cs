using UnityEngine;
using System.Collections;

public class OverworldCamera : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    [SerializeField, Range(0, 5)]
    private float lerpSpringiness = 3f;
    [SerializeField]
    private float deadZone;

    private Transform centerPoint;

    private Vector3 oldPos, newPos;

    // Use this for initialization
    void Awake()
    {
        centerPoint = transform.GetChild(0); // Make sure i't the first child
        newPos = player.position - centerPoint.localPosition - new Vector3(-.5f, .5f);
        transform.position = newPos;
        oldPos = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        oldPos = transform.position;

        if (StateMachine.CurrentPlayState == PlayState.OverWorld)
        {
            if (Mathf.Abs(player.position.x - centerPoint.position.x) > deadZone / 2 || Mathf.Abs(player.position.y - centerPoint.position.y) > deadZone / 2)
            {
                newPos = player.position - centerPoint.localPosition;
                transform.position = Vector3.Lerp(oldPos, newPos, Time.deltaTime * lerpSpringiness);
            }
            else
            {
                transform.position = Vector3.Lerp(oldPos, newPos, Time.deltaTime * (lerpSpringiness / 2));
            }
        }
    }
    /*
    public void OnDrawGizmos()
    {
        if (centerPoint.position == null) { return; }
        Gizmos.DrawCube(centerPoint.position, new Vector3(deadZone, deadZone, deadZone));
    }*/
}
