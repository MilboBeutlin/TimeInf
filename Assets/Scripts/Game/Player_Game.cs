using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Game : MonoBehaviour
{
[SerializeField] private float speed = 5f;
[SerializeField] private Rigidbody2D body;
[SerializeField] private LayerMask wallLayer;
[SerializeField] private Sprite[] sprites;
[SerializeField] private SpriteRenderer sR;

private Vector2 input;

void Update()
{
    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

    if (input.x != 0)
    {
        input.y = 0;

        if (input.x < 0)
            sR.sprite = sprites[0]; // left
        else
            sR.sprite = sprites[1]; // right
    }
    else if (input.y != 0)
    {
        if (input.y < 0)
            sR.sprite = sprites[2]; // down
        else
            sR.sprite = sprites[3]; // up
    }
}

void FixedUpdate()
{
    Vector2 move = input;

    if (move.x != 0)
    {
        RaycastHit2D hit = Physics2D.Raycast(body.position, new Vector2(move.x, 0), 0.1f, wallLayer);
        if (hit.collider != null)
        {
            move.x = 0;
        }
    }

    if (move.y != 0)
    {
        RaycastHit2D hit = Physics2D.Raycast(body.position, new Vector2(0, move.y), 0.1f, wallLayer);
        if (hit.collider != null)
        {
            move.y = 0;
        }
    }

    body.linearVelocity = move.normalized * speed;
}
}
