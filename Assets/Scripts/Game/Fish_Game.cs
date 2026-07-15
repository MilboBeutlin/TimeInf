using UnityEngine;

//fish moving around the lake, back and forth between two horizontal points
public class Fish_Game : MonoBehaviour
{
    [SerializeField] private float leftPoint;
    [SerializeField] private float rightPoint;

    private float speed;
    private bool movingRight = true;
    private SpriteRenderer sR;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        speed = Random.Range(1.8f, 4f); //random speed for each fish
    }

    private void Update()
    {
        float direction = movingRight ? 1 : -1;
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if (transform.position.x >= rightPoint)
        {
            movingRight = false;
            sR.flipY = false;
        }
        else if (transform.position.x <= leftPoint)
        {
            movingRight = true;
            sR.flipY = true;
        }
    }
}
