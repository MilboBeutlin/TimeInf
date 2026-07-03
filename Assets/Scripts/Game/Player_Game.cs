using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//player class for movement and collison with enemy and objects that kills the player
public class Player_Game : MonoBehaviour
{
[SerializeField] private float speed = 5f;
[SerializeField] private Rigidbody2D body;
[SerializeField] private LayerMask wallLayer;
[SerializeField] private Sprite[] sprites;
[SerializeField] private SpriteRenderer sR;
[SerializeField] private Controller controller;
[SerializeField] private GM_Game gameMaster;
private Vector2 input;
public Vector2 CurrentMovement => input;
[SerializeField] private GameObject globalLight;

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
            sR.sprite = sprites[0]; // facing left
        }
        else
        {
            sR.sprite = sprites[1]; // facing right
        }
            
    }
    else if (input.y != 0)
    {
        if (input.y < 0)
        {
            sR.sprite = sprites[2]; // facing down
        }
        else
        {
            sR.sprite = sprites[3]; // facing up
        }          
    }

    Scene battleScene = SceneManager.GetSceneByName("Fight");

    if (!battleScene.isLoaded)
    {
        globalLight.SetActive(true);
    }else{
        globalLight.SetActive(false);
    }
}

void FixedUpdate() //movement with raycasts to block walking at walls + only movement along each axis
{
    Vector2 move = input;

    body.linearVelocity = move.normalized * speed;
}
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy")) //loading enemie on collision
    {
        controller.SetCurrentOponent(other.gameObject.GetComponent<Enemy_Game>().type);
        
        SceneManager.LoadScene("Fight", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
        Destroy(other.gameObject);
    }
    else if (other.CompareTag("Death"))
    {
        gameMaster.PlayerDeath();
    }
}
private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "Fight")
    {
        if (globalLight != null && globalLight)
        {
            globalLight.SetActive(false);
        }

        var battleLight = FindFirstObjectByType<UnityEngine.Rendering.Universal.Light2D>();

        battleLight.gameObject.SetActive(true);
    }
}
}
