using UnityEngine;

public class Runen_Game : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject[] runes;
    [SerializeField] private GameObject door;
    private int[] runeDir = {0, 0, 0, 0};
    private bool puzzle;
    private int currentRune;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzle)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentRune++;
                if(currentRune > 3)
                {
                    currentRune = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentRune--;
                if(currentRune < 0)
                {
                    currentRune = 3;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                runes[currentRune].transform.Rotate(0,0,-90);
                runeDir[currentRune] -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                runes[currentRune].transform.Rotate(0,0,90);
                runeDir[currentRune] += 1;
            }
            if(runeDir[0] == 1 && runeDir[1] == 1 && runeDir[2] == 1 && runeDir[3] == 1)
            {
                Debug.Log("Wann kommt FNAF4?");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            puzzle = true;
            camera.orthographicSize = 4f;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            puzzle = false;
            camera.orthographicSize = 6f;
        }
    }
}
