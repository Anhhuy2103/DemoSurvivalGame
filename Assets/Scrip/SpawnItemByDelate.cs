using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemByDelate : MonoBehaviour
{
    private List<GameObject> itemList; // khởi tạo biến
    [SerializeField] private GameObject itemPrefab;


    protected float tickTime = 0f;
    [SerializeField] protected float delayTime = 10f; // 10s se spawn 1 lan
    [SerializeField] protected float minOffset = -20;
    [SerializeField] protected float MaxOffset = 20;
    [SerializeField] protected int maxItemCount = 5;



    private void Start()
    {
        itemList = new List<GameObject>(); // khai báo biến = gán biến sau dấu bằng           
    }

  
    private void Update()
    {
        this.Spawn();


        this.CheckMinionDead();
    }


    private void Spawn()
    {
        Vector3 spawnOffset = new Vector3(Random.Range(minOffset, MaxOffset), 0f, Random.Range(minOffset, MaxOffset));
        Vector3 spawnPosition = transform.position + spawnOffset;

        this.tickTime += Time.deltaTime;
        if (this.tickTime < this.delayTime) return;
        this.tickTime = 0;

        if (this.itemList.Count >= maxItemCount) return;


        GameObject item = Instantiate(this.itemPrefab, spawnPosition, Quaternion.identity);


        item.gameObject.SetActive(true);
        this.itemList.Add(item);

    }

    private void CheckMinionDead()
    {
        GameObject item;
        for (int i = 0; i < this.itemList.Count; i++)
        {
            item = this.itemList[i];
            if (item == null)
            {
                this.itemList.RemoveAt(i);
            }
        }
    }
}
