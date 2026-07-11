using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//player class for movement and collison with enemy and objects that kills the player
public class Player_Game : MonoBehaviour
{
private float currentSpeed;
private float walkSpeed = 8f;
private float sprintSpeed = 16f;
[SerializeField] private Rigidbody2D body;
[SerializeField] private LayerMask wallLayer;
[SerializeField] private Sprite[] playerSprites;
[SerializeField] private SpriteRenderer sR;
[SerializeField] private Controller controller;
[SerializeField] private GM_Game gameMaster;
private Vector2 input;
public Vector2 CurrentMovement => input;


void Update()
{
    //input wasd/arrow keys
    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

    //player.sprite change
    if (input.x != 0)
    {
        input.y = 0;

        if (input.x < 0)
        {
            sR.sprite = playerSprites[0]; // facing left
        }
        else
        {
            sR.sprite = playerSprites[1]; // facing right
        }
            
    }
    else if (input.y != 0)
    {
        if (input.y < 0)
        {
            sR.sprite = playerSprites[2]; // facing down
        }
        else
        {
            sR.sprite = playerSprites[3]; // facing up
        }          
    }

    //sprinting with left shift
    if (Input.GetKey(KeyCode.LeftShift))
    {
        currentSpeed = sprintSpeed;
    }
    else
    {
        currentSpeed = walkSpeed;
    }
}

void FixedUpdate()
{
    Vector2 move = input;

    body.linearVelocity = move.normalized * currentSpeed;
}

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy")) //loading enemie on collision
    {
        controller.SetCurrentOponent(other.gameObject.GetComponent<Enemy_Game>().type);
        gameMaster.ShowText(false);
        SceneManager.LoadScene("Fight", LoadSceneMode.Additive);
        FindAnyObjectByType<Model>().LightsSwitchToFight(true);
        Destroy(other.gameObject);
    }
    else if (other.CompareTag("Death"))
    {
        gameMaster.PlayerDeath();
    }
}
}