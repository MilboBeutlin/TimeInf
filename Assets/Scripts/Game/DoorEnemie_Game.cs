using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void OnBecameInvisible()
    {
        if (gm?.GetText() == "You dare to exist?" && this.gameObject != null)
        {
            transform.position = moveTo.position;       //-31.833 empty stuff, -31.48667 door
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (gm?.GetText() == "This is our last meeting bitch" && this.gameObject != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, player.transform.position, Quaternion.identity);
            enemy.GetComponent<Enemy_Game>().Creation(Gegner.MonsterPainting, enemySprite);
            this.gameObject.SetActive(false);
        }
    }

}
