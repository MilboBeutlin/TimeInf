using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DoorEnemie_Game : MonoBehaviour
{
    [SerializeField] private Transform moveTo;
    [SerializeField] private GameObject TextObject;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform player;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.position != moveTo.position)
        {
            text.text = "You dare to exist?";
            TextObject.SetActive(true);
        }
        else
        {
            text.text = "This is our last meeting bitch";
            TextObject.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TextObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        if (text.text == "You dare to exist?")
        {
            transform.position = moveTo.position;       //-31.833 empty stuff, -31.48667 door
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (text.text == "This is our last meeting bitch")
        {
            Instantiate(enemy, player.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

}
