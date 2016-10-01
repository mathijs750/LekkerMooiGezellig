using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
    [SerializeField,Range(0,1)]
    private float stepSize = 0.25f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StateMachine.CurrentGameState == GameState.Playing && StateMachine.CurrentPlayState == PlayState.OverWorld)
        {
            transform.Translate(Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0), stepSize));
        }
    }
}
