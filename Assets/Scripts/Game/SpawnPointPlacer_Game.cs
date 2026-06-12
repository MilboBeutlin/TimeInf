using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnPointPlacer_Game : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private Tilemap floorTilemap;
    private float holdTimer = 0f;
    private float holdDuration = 0.5f;    // how long to press q to place spawnPoint
    [SerializeField] private Model model;
    [SerializeField] private Controller controller;

    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdDuration)
            {
                PlacespawnPoint(transform.position);
            }
        }
        /*if (Input.GetKeyUp(KeyCode.Q))
        {
                Vector3Int direction = GetArrowDirection();
                if (direction != Vector3Int.zero)
                {
                    holdTimer = 0f;
                    Vector3Int targetTile = floorTilemap.WorldToCell(transform.position) + direction * 2;
                    PlacespawnPoint(floorTilemap.GetCellCenterWorld(targetTile));
                }
        }*/
    }

    private Vector3Int GetArrowDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            return Vector3Int.up;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            return Vector3Int.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            return Vector3Int.left;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            return Vector3Int.right;
        }   
        return Vector3Int.zero;
    }
    private void PlacespawnPoint(Vector3 desiredPos)
    {
        Vector3Int tile = floorTilemap.WorldToCell(desiredPos);
        Vector3? safePosition = FindSafeTile(tile);

        if (safePosition == null) {
            Debug.Log("No safe position found");
            return;
        }

        //spawnPoint.transform.position = safePosition.Value;
        //spawnPoint.moveSpawnPoint(safePosition.Value, model.GetLocation());
        spawnPoint.transform.position = safePosition.Value;

        //SpawnPoint_Game.Instance.spawnPosition = safePosition.Value;
        controller.SetSpawnPosition(safePosition.Value);
        controller.SetSavePlayerItems(model.GetCurrentPlayerItems());
        Debug.Log("Spawn point is placed");
    }
    private Vector3? FindSafeTile(Vector3Int tile)
    {
        Vector3Int[] TileArea = {tile, tile + Vector3Int.up, tile + Vector3Int.down, tile + Vector3Int.left, tile + Vector3Int.right};
        foreach (var Tile in TileArea)
        {
            if (IsSafe(Tile))
            {
                return floorTilemap.GetCellCenterWorld(Tile);
            }
        }
        return null;
    }
    private bool IsSafe(Vector3Int tile)
    {
        if (!floorTilemap.HasTile(tile)){ 
            return false;
        }else
        {
            return true;
        }
    }

}
