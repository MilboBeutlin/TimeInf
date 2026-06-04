using UnityEngine;

public class PressurePlate_Game : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform spawnPoint;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i < 4; i++){
                Instantiate(arrow, spawnPoint.position + new Vector3(0, i, 0), Quaternion.Euler(0f, 0f, 45f));
            }
        }
    }
}
