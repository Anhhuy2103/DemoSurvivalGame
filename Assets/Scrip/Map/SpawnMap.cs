using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Transform prefabParrent;
    [SerializeField] private GameObject prefabObject;

    public int size;
    public float offset;
    public Vector3 startPos = Vector3.zero;

    [ContextMenu("spawnFloor")]

    public void spawnFloor()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var obSpawn = Instantiate(prefabObject, prefabParrent);
                obSpawn.transform.rotation = Quaternion.Euler(Vector3.zero);
                obSpawn.transform.position = new Vector3(startPos.x + (j * offset), startPos.y, startPos.z + (i * offset));
                obSpawn.name = "Floor";
            }
        }
    }

    [ContextMenu("SpawnWall")]
    public void GenerateWall()
    {
        for (int j = 0; j < size; j++)
        {
            var obSpawn = Instantiate(prefabObject, prefabParrent);
            obSpawn.transform.position = new Vector3(startPos.x, startPos.y, startPos.z + (j * offset));
            obSpawn.name = ("Wall");
        }
    }
}
