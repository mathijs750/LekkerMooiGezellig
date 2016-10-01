using UnityEngine;
using System.Collections;

public class OverworldCamera : MonoBehaviour {
    
    [SerializeField]
    private Transform player;
    [SerializeField,Range(0,5)]
    private float lerpSpringiness = 3f;
    private Transform centerPoint;
    private Vector3 oldPos;

	// Use this for initialization
	void Awake ()
    {
        centerPoint = transform.GetChild(0); // Make sure i't the first child
        oldPos = player.position + centerPoint.localPosition;
        transform.position = oldPos;
    }
	
	// Update is called once per frame
	void Update ()
    {
        oldPos = transform.position;

        if (StateMachine.CurrentPlayState == PlayState.OverWorld)
        {
            transform.position = Vector3.Lerp(oldPos, player.position - centerPoint.localPosition, Time.deltaTime * lerpSpringiness);

        }
    }
}
