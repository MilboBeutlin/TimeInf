using UnityEngine;
using System.Collections;

public class PuzzleManager_Game : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Runen_Game[] runes;
    [SerializeField] private GameObject door;
    [SerializeField] private BoxCollider2D doorCollider;
    

    public void CheckRunen()
    {
        if(runes[0].IsCorrect() && runes[1].IsCorrect() && runes[2].IsCorrect() && runes[3].IsCorrect())
        {
            StartCoroutine(SetRuneInactive(1,0.5));
            StartCoroutine(SetRuneInactive(2,0.5));
            StartCoroutine(SetRuneInactive(0,1));
            StartCoroutine(SetRuneInactive(3,1));
            StartCoroutine(MoveDoor(door.transform.position + new Vector3(3f, 0f, 0f)));            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            camera.orthographicSize = 4f;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            camera.orthographicSize = 6f;
        }
    }
    IEnumerator SetRuneInactive(int which, int t)
    {
        yield return new WaitForSeconds(t);
        runes[which].gameObject.SetActive(false);
    }
    IEnumerator MoveDoor(Vector3 target)
    {
        door.SetActive(true);
        while (Vector3.Distance(door.transform.position, target) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, target, 2 * Time.deltaTime);
            yield return null;
        }

        door.transform.position = target;
        doorCollider.enabled = true;
    }
}
