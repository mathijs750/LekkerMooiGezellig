using UnityEngine;
using System.Collections;

public enum spriteDirection
{
    left,
    right,
    up,
    down
}

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteControler : MonoBehaviour
{
    // 0 = left, 1 = up, 2 = down, this patern is followed for animation, so 3 = left2, 4 = up2 ...
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private bool useAnimation;
    [SerializeField]
    private int numberOfFrames = 0;
    private int animTime;

    private SpriteRenderer sRenderer;
    private spriteDirection direction;
    public spriteDirection Direction { set { direction = value; } }

    void Awake()
    {
        direction = spriteDirection.down;
        sRenderer = GetComponent<SpriteRenderer>();
        animTime = 0;
    }

    void Update()
    {
        if (useAnimation)
        {
            if (animTime >= numberOfFrames) { animTime = 0; }
            else { animTime++; }
        }
        changeSprite(animTime);
   //     Debug.Log(animTime);
    }

    private void changeSprite(int time)
    {
        switch (direction)
        {
            case spriteDirection.up:
                sRenderer.sprite = sprites[1 + time];
                break;
            case spriteDirection.down:
                sRenderer.sprite = sprites[2 + time];
                break;
            case spriteDirection.left:
                sRenderer.sprite = sprites[0 + time];
                sRenderer.flipX = false;
                break;
            case spriteDirection.right:
                sRenderer.sprite = sprites[0 + time];
                sRenderer.flipX = true;
                break;
            default:
                break;
        }
    }
}
