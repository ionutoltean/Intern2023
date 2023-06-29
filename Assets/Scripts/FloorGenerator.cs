using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public GameObject whiteTile, blackTile;

    public float tileSize = 0.25f;
    public float floorSize = 5f;

    [ContextMenu("generate")]
    public void Generateobjects()
    {
        int rowElements = Mathf.CeilToInt(floorSize / tileSize);
        for (int i = 0; i < rowElements; i++)
        {
            for (int j = 0; j < rowElements; j++)
            {
                GameObject spawnPrefab;
                if ((i + j) % 2 == 0)
                    spawnPrefab = whiteTile;
                else
                {
                      spawnPrefab = blackTile;
                }

                Vector3 nextPositon = new Vector3(transform.position.x + i * tileSize, transform.position.y,
                    transform.position.z + j * tileSize);
                var spawned = Instantiate(spawnPrefab);
                spawned.transform.position = nextPositon;
            }
        }
    }

}