using UnityEngine;
using System.Collections.Generic;

public class Lake_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private Controller controller;
    private bool isColliding;
    private bool didFish;
    [SerializeField] private List<GameObject> fishes;
    [SerializeField] private float leftPoint;
    [SerializeField] private float rightPoint;
    private Dictionary<GameObject, bool> directions = new();
    private float speed = 2f;
    void start()
    {
        foreach (var fish in fishes)
        {
            directions[fish] = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isColliding && !didFish && Input.GetKeyDown(KeyCode.E))
        {
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(Items.Scroll, 1);
            gm.ItemsGot(Items.Scroll, 1);
            Debug.Log("You gained: " + "Scroll");
            didFish = true;
            gm.ShowText(false);
        }

        //fish moving around

        

        foreach (var fish in fishes)
        {
            if (!directions.ContainsKey(fish))
            {
                directions.Add(fish, true);
            }

            bool movingRight = directions[fish];
            float direction = movingRight ? 1 : -1;

            fish.transform.position += Vector3.right * direction * speed * Time.deltaTime;    
            SpriteRenderer sr = fish.GetComponent<SpriteRenderer>();
            if (fish.transform.position.x >= rightPoint){
                directions[fish] = false;
                sr.flipY = false;
            }
            else if (fish.transform.position.x <= leftPoint){
                directions[fish] = true;
                sr.flipY = true;
            }
            

        }
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && gm.giveCurrentPlayerItems().ContainsKey(Items.FishingRod))
        {
            isColliding = true;
            gm.ChangeText("Press E to fish");
            gm.ShowText(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isColliding = false;
            gm?.ShowText(false);
        }
    }
}
