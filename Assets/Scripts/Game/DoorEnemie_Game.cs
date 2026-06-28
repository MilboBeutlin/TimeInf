using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Controls the enemy that blocks the door and spawns it to fight the player on the second meeting
public class DoorEnemie_Game : MonoBehaviour
{
    [SerializeField] private Transform moveTo;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Transform player;
    private GM_Game gm;

    public void Start()
    {
        gm = FindAnyObjectByType<GM_Game>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(transform.position != moveTo.position)
            {
                gm.ChangeText("You dare to exist?");
                gm.ShowText(true);
            }
            else
            {
                gm.ChangeText("This is our last meeting bitch");
                gm.ShowText(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gm?.ShowText(false);
        
    }
    //moves the enemy or spawn it if it was the second time walking awway from it
    private void OnBecameInvisible()
    {
        if (gm?.GetText() == "You dare to exist?" && this.gameObject != null)
        {
            transform.position = moveTo.position;
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (gm?.GetText() == "This is our last meeting bitch" && this.gameObject != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, player.position, Quaternion.identity);
            enemy.GetComponent<Enemy_Game>().Creation(Gegner.MonsterPainting, enemySprite);
            this.gameObject.SetActive(false);
        }
    }

}
