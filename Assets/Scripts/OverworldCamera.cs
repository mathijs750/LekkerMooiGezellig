using UnityEngine;
using System.Collections;

public class OverworldCamera : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Transform centerPoint, cornerPoint;

    public Vector3 topCorner
    {
        get
        {
            if (cornerPoint != null) { return cornerPoint.position; }
            else { return Vector3.zero; }
        }
    }

    void Awake()
    {
        centerPoint = transform.GetChild(0); // Make sure i't the first child
        cornerPoint = transform.GetChild(1);
    }

    void Update()
    {
        if (StateMachine.CurrentPlayState == PlayState.OverWorld)
        {
            transform.position = player.position - centerPoint.localPosition;
        }
    }
}
