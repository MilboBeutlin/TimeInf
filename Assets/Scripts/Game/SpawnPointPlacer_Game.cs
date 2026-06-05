using UnityEngine;

public class SpawnPointPlacer_Game : MonoBehaviour
{
    [SerializeField] private GameObject checkpointPrefab;
    [SerializeField] private Tilemap floorTilemap;

    private GameObject activeCheckpoint;
    private float holdTimer = 0f;
    private float holdDuration = 0.5f;    // how long to press q to place checkpoint
    private bool placed = false;

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdDuration && !placed)
            {
                PlaceCheckpoint(transform.position);
                placed = true;
            }
        }
    }

    void PlaceCheckPoint(Vector3 desiredPosition)
    {
        
    }

}
