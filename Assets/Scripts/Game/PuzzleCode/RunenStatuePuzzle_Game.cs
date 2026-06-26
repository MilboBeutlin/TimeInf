using UnityEngine;
using System.Collections;

public class RunenStatuePuzzle_Game : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Runen_Game[] runes;
    [SerializeField] private GameObject door;
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private bool runePuzzle;
    [SerializeField] private GameObject chest;
    private bool IsSolved;    //can be removed and coded cleaner, D will do it maybe sometime


    public void CheckRunen()
    {
        if(runes[0].IsCorrect() && runes[1].IsCorrect() && runes[2].IsCorrect() && runes[3].IsCorrect() && !IsSolved)    //code correct? with delay:
        {
            if (runePuzzle)
            {
                IsSolved = true;
                StartCoroutine(SetRuneInactive(1, 2, 0.5f));                                                     //remove runes
                StartCoroutine(SetRuneInactive(3, 0, 1));
                StartCoroutine(MoveDoor(door.transform.position + new Vector3(3f, 0f, 0f)));                    //door appears slowly
            }
            else{
                IsSolved = true;
                chest.SetActive(true);
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            camera.orthographicSize = 4f; //zoom in
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            camera.orthographicSize = 6f; // zoom back to normal
        }
    }
    IEnumerator SetRuneInactive(int which, int which2, float t)        //with delay setting runes inactive
    {
        yield return new WaitForSeconds(t);
        runes[which].gameObject.SetActive(false);
        runes[which2].gameObject.SetActive(false);
    }
    IEnumerator MoveDoor(Vector3 target)                            //with delay door appears out of the ground
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
        camera.orthographicSize = 6f; // just in case of bugs
    }
}
