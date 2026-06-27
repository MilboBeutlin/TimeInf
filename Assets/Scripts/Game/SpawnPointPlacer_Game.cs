using UnityEngine;
using UnityEngine.Tilemaps;
//made by Dominik
// Handles placing and saving the player's spawn point.
public class SpawnPointPlacer_Game : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private Tilemap floorTilemap; //floorTiles are the only Tiles that are safe for the spawnPoint to be placeed on

    [SerializeField] private Model model;
    [SerializeField] private Controller controller;

    void Start()
    {
        spawnPoint.transform.position = model.GetSpawnPosition(); // restores the previous saved spawnPoint position
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            PlaceSpawnPoint(transform.position);
        }
    }
    // Places the spawn point on the nearest safe floor tile.
    private void PlaceSpawnPoint(Vector3 desiredPos)
    {
        Vector3Int tile = floorTilemap.WorldToCell(desiredPos);
        Vector3? safePosition = FindSafeTile(tile);

        if (safePosition == null) {
            Debug.Log("No safe position found");
            return;
        }

        spawnPoint.transform.position = safePosition.Value;
        // Saves the new spawn point and the player's current position:
        controller.SetSpawnPosition(safePosition.Value);
        controller.SetSavePlayerLocation(model.GetPlayerLocation());
        controller.SetSavePlayerItems(model.GetCurrentPlayerItems());
    }
    // Searches the current tile and its neighboring tiles for a safe position.
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
    // Checks whether the given tile exists on the floor tilemap.
    private bool IsSafe(Vector3Int tile)
    {
        return floorTilemap.HasTile(tile);
    }

}
