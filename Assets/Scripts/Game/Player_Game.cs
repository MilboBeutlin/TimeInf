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
private string location = "R0";
void Start()
{
        controller = FindAnyObjectByType<Controller>();
}
void Update()
{
    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

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
        Debug.Log(input.x = Input.GetAxisRaw("Horizontal"));
}
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
         switch (other.gameObject.name)
        {
            case "R1":
            EnemyLoading(15, 30, 100, 3, 5);
            break;
            case "R3":
            EnemyLoading(30, 35, 40, 9, 30);
            break;
            case "R6":
            EnemyLoading(10, 50, 60, 7, 90); //Verflucht
            break;
            case "G1":
            EnemyLoading(25, 25, 0, 4, 55); // Vergiftet
            break;
            case "G3":
            EnemyLoading(100, 55, 30, 5, 35); // blutend
            break;
            case "Foyer":
            EnemyLoading(160, 75, 10, 8, 100); // verbrennen
            break;
            case "Endboss":
            EnemyLoading(300, 90, 30, (int)speed, 120); //speed = player speed
            break;
            default:
                Debug.Log("I am useless");
                break;
        }
        SceneManager.LoadScene("Fight");
    }else if (other.CompareTag("door"))
        {

            if (input.x != 0)
            {
                if (input.x > 0)
                {
                    body.position = new Vector2(body.position.x + 2, body.position.y);

                }
                else
                {
                    body.position = new Vector2(body.position.x - 2, body.position.y);

                }
            }

            if (input.y != 0)
            {
                if (input.y > 0)
                {
                    body.position = new Vector2(body.position.x, body.position.y + 2);
                    location = "R1";
                }

                else
                {
                    body.position = new Vector2(body.position.x, body.position.y - 2);
                    location = "R0";
                }
                
            }
        }
}

    private void EnemyLoading(int lp, int atk, int armor, int speed, int dk)
{
    if (dk == 120)
    {
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

    public string Location()
    {
        return location;
    }
}
