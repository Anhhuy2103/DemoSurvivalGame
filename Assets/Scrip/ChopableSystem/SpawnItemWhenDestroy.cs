using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnItemWhenDestroy : MonoBehaviour
{
    [SerializeField] private GameObject parentItem;
    [SerializeField] private GameObject itemprefab1;
    [SerializeField] private GameObject itemprefab2;
    [SerializeField] private Transform spawnPoint;

    private void Update()
    {
        DestroyAndSpawn();
    }
    private void DestroyAndSpawn()
    {
        if (parentItem.IsDestroyed())
        {
            int randomvalue = Random.Range(0, 2);
            if (randomvalue == 0)
            {
               GameObject itemdrop1 =  Instantiate(itemprefab1, spawnPoint.position, Quaternion.Euler(0,0,0));
            }
            if (randomvalue == 1)
            {
                GameObject itemdrop2= Instantiate(itemprefab2, spawnPoint.position, Quaternion.Euler(0, 0, 0));
            }
            StartCoroutine(Destroy());
        }    
    }
    protected IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(spawnPoint);
    }
}
