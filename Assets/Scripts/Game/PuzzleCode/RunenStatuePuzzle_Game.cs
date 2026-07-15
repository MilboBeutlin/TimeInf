using UnityEngine;
using System.Collections;

//Manages the rune/statue puzzle and triggers the reward when it is solved.
public class RunenStatuePuzzle_Game : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Runen_Game[] runes;
    [SerializeField] private GameObject door;
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private bool runePuzzle; //is it a runePuzzle or statuePuzzle
    [SerializeField] private GameObject chest;
    private bool isSolved; //prevention from solving the puzzle multiple times

    public void CheckRunen()
    {
        // Checks if every rune is in the correct rotation:
        if (runes[0].IsCorrect() && runes[1].IsCorrect() && runes[2].IsCorrect() && runes[3].IsCorrect() && !isSolved)
        {
            if (runePuzzle)
            {
                isSolved = true;

                //remove runes with a delay
                StartCoroutine(SetRuneInactive(1, 2, 0.5f));
                StartCoroutine(SetRuneInactive(3, 0, 1));

                //door appears slowly out of the ground
                StartCoroutine(MoveDoor(door.transform.position + new Vector3(3f, 0f, 0f)));
            }
            else
            {
                isSolved = true;
                chest.SetActive(true);
            }

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camera.orthographicSize = 4f; //zoom in
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camera.orthographicSize = 6f; // zoom back to normal
        }
    }

    //with delay setting runes inactive
    IEnumerator SetRuneInactive(int which, int which2, float t)
    {
        yield return new WaitForSeconds(t);
        runes[which].gameObject.SetActive(false);
        runes[which2].gameObject.SetActive(false);
    }

    //with delay door appears out of the ground
    IEnumerator MoveDoor(Vector3 target)
    {
        BoxCollider2D ownCollider = this.GetComponent<BoxCollider2D>();
        ownCollider.isTrigger = false;
        door.SetActive(true);

        while (Vector3.Distance(door.transform.position, target) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, target, 2 * Time.deltaTime);
            yield return null;
        }

        door.transform.position = target;
        doorCollider.enabled = true;
        ownCollider.enabled = false;
        camera.orthographicSize = 6f;
    }
}
