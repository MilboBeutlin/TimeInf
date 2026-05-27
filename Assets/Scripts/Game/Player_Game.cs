using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Game : MonoBehaviour
{
[SerializeField] private float speed = 5f;
[SerializeField] private Rigidbody2D body;
[SerializeField] private LayerMask wallLayer;
[SerializeField] private Sprite[] sprites;
[SerializeField] private SpriteRenderer sR;
private Controller controller;
private Vector2 input;
private string location = "K1";
void Start()
{
        controller = FindAnyObjectByType<Controller>();
}
void Update()
{
    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");
    //player.sprite change
    if (input.x != 0)
    {
        input.y = 0;

        if (input.x < 0)
        {
            sR.sprite = sprites[0]; // left
        }
        else
        {
            sR.sprite = sprites[1]; // right
        }
            
    }
    else if (input.y != 0)
    {
        if (input.y < 0)
        {
            sR.sprite = sprites[2]; // down
        }
        else
        {
            sR.sprite = sprites[3]; // up
        }          
    }

    
}

void FixedUpdate() //movement
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
        Debug.Log(input.x = Input.GetAxisRaw("Horizontal"));
}
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy")) //loading enemie on collision
    {
        Enemy_Game enemyScript = other.gameObject.GetComponent<Enemy_Game>();
         switch (enemyScript.type)
        {
            case Gegner.StorageGuard:
            EnemyLoading(Gegner.StorageGuard, 15, 30, 100, 3, 5);
            break;
            case Gegner.MonsterPainting:
            EnemyLoading(Gegner.MonsterPainting, 30, 35, 40, 9, 30);
            break;
            case Gegner.ShadowEnemy:
            EnemyLoading(Gegner.ShadowEnemy, 10, 50, 60, 7, 90); //Verflucht
            break;
            case Gegner.Insects:
            EnemyLoading(Gegner.Insects, 25, 25, 0, 4, 55); // Vergiftet
            break;
            case Gegner.PrisonGuard:
            EnemyLoading(Gegner.PrisonGuard, 100, 55, 30, 5, 35); // blutend
            break;
            case Gegner.MiniBoss:
            EnemyLoading(Gegner.MiniBoss, 160, 75, 10, 8, 100); // verbrennen
            break;
            case Gegner.Endboss:
            EnemyLoading(Gegner.Endboss, 300, 90, 30, (int)speed, 120); //speed = player speed
            break;
            default:
                Debug.Log("I am useless");
                break;
        }
        SceneManager.LoadScene("Fight");
    }
}

    private void EnemyLoading(Gegner type, int lp, int atk, int armor, int speed, int dk) 
{
    if (type == Gegner.Endboss)
    {
        controller.SetCurrentOponent(type);
        controller.SetCurrentOponnentAttacks(new Attacks[]
        {
            Attacks.DunklerSchnitt,
            Attacks.Giftwurf,
            Attacks.KronederVerdammnis,
            Attacks.Daemonensphaere,
            Attacks.ChaosLanze,
            Attacks.Block,
            Attacks.DunkleResonanz,
            Attacks.KingsBreaker,
            Attacks.EndloseDunkelheit
        });
    }
    else
    {
        controller.SetCurrentOponnentAttacks(new Attacks[]
        {
            //gegner attacken
        });
    }
    controller.SetCurrentOponnentStats(new int[]{lp, atk, armor, speed, dk});
}

    public string GetLocation() //location for camera
    {
        return location;
    }
    public void SetLocation(string location) //location for camera
    {
        this.location = location;
    }
}
