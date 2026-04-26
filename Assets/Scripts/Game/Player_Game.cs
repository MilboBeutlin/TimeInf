using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Game : MonoBehaviour
{
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer sR;
    private float x;
    private float y;
    private Vector2 move;
    void Start()
    {
        
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x != 0)
        {
            move = new Vector2(x, 0);
            if (x < 0)
            {
                sR.sprite = sprites[0];
            }
            else
            {
                sR.sprite = sprites[1];
            }
        }            
        else if (y != 0)
        {
            move = new Vector2(0, y);
            if (y < 0)
            {
                sR.sprite = sprites[2];
            }
            else
            {
                sR.sprite = sprites[3];
            }
        }
        else
        {
            move = Vector2.zero;
        }
            

        this.transform.Translate(move * speed * Time.deltaTime);
    }
}
