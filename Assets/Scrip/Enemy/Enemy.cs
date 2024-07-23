using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Name: ")]

    public string EnemyName;
    [Header("Enemy Info: ")]
    [SerializeField] private StatusItemSO EnemybigSo;
    [SerializeField] private int EnemyCurrentHP;
    [SerializeField] private int EnemyMaxHP;
    [SerializeField] private int EnegysSpentMelee = 2;
    public bool isCanMelee;
    public bool isDead;

    [Header("ColliderSystem")]
    public bool atackReady;
    public CapsuleCollider handColliderLeft;
    public CapsuleCollider handColliderRight;
    

  
    private Animator animator;
    private NavMeshAgent navAgent;

    [Header("ItemPrefabSpawn System")]
    public GameObject itemPrefab1;
    public GameObject itemPrefab2;
    public GameObject itemPrefab3;
    public Transform itemSpawnPoint;

    [Header("EffectToEnemy System")]
    public float tickTime;



    public string GetEnemyName()
    {
        return EnemyName;
    }
    private void init()
    {
        if (isCanMelee)
        {
            GlobalReferences.Instance.EnemyMaxHP = EnemyMaxHP;
            GlobalReferences.Instance.EnemyCurrentHP = EnemyCurrentHP;
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
   
        handColliderLeft.enabled = false;
        handColliderRight.enabled = false;
        EnemyMaxHP = EnemybigSo.maxHP;
        EnemyCurrentHP = EnemyMaxHP;
    }

    public void TurnOnHandCollider()
    {
        handColliderLeft.enabled = true;
        handColliderRight.enabled = true;

    }

    public void TurnOffHandCollider()
    {
        handColliderLeft.enabled = false;
        handColliderRight.enabled = false;
    }
    int _randomDame;
    int _maxMeleeDame;
    public void takedameForEnemy(int minDameAmount, int maxDameAmount)
    {
        int randomDame = Random.Range(minDameAmount, maxDameAmount);
        _randomDame = randomDame;
        _maxMeleeDame = maxDameAmount;
        EnemyCurrentHP -= randomDame;
        StartCoroutine(DisplayRandomDame());
        PlayerStatusManager.Instance.playerdataSo.CurrentEnegy -= EnegysSpentMelee;

        if (EnemyCurrentHP <= 0)
        {
          
            SoundManager.Instance.GolemSound_Death();
            StartCoroutine(PlaySound_ChaseToBackGround());
            animator.SetTrigger("DIE1");

            SpawnItemWhenDie();
            takeEXPandUPLevel(EnemybigSo.BonusHP);
            if(PlayerStatusManager.Instance.playerdataSo.isLevelUp)
            {
                EnemybigSo.BonusHP += 15;

            }
            handColliderLeft.enabled = false;
            handColliderRight.enabled = false;
            isDead = true;
            isCanMelee = false;
            InteractionManager.Instance.hoveredEnemyBig = null;
            InteractionManager.Instance.chopHolder.SetActive(false);
        }
        else if (50 >= EnemyCurrentHP && EnemyCurrentHP <= 200)
        {
            SoundManager.Instance.GolemSound_Hurt();        
            isDead = false;
            isCanMelee = true;
            animator.SetTrigger("HURT");
        }
        else if( 300 >= EnemyCurrentHP && EnemyCurrentHP <= 400)
        {        
            SoundManager.Instance.EnemySource2.PlayOneShot(SoundManager.Instance.GolemHurt2);
            isDead = false;
            isCanMelee = true;
        }
    }
    private IEnumerator DisplayRandomDame()
    {
        InteractionManager.Instance.chopHolder.SetActive(true);
        InteractionManager.Instance.nameOfTreeText.text = "";
        if (_randomDame == _maxMeleeDame)
        {
            InteractionManager.Instance.DameText.color = Color.magenta;
            InteractionManager.Instance.DameText.fontSize = 60;
        }
        InteractionManager.Instance.DameText.text = _randomDame.ToString();
        yield return new WaitForSeconds(0.2f);
        InteractionManager.Instance.DameText.color = Color.black;
        InteractionManager.Instance.DameText.fontSize = 40;
        InteractionManager.Instance.chopHolder.SetActive(false);
        InteractionManager.Instance.DameText.text = "";

    }

    private IEnumerator PlaySound_ChaseToBackGround()
    {
       
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.musicSource2.Stop();
                    
    }

    // ---------- Spawn Item --------
    private void SpawnItemWhenDie()
    {
        int randomvalue = Random.Range(0, 3);
        if (randomvalue == 0)
        {
            GameObject itemDrop = Instantiate(itemPrefab1, itemSpawnPoint.position, Quaternion.identity);    
        }
        if(randomvalue == 1)
        {
            GameObject itemDrop = Instantiate(itemPrefab2, itemSpawnPoint.position, Quaternion.identity);        
        }
        if(randomvalue == 2)
        {
            GameObject itemDrop = Instantiate(itemPrefab3, itemSpawnPoint.position, Quaternion.identity);            
        }

    }

    // ----------- Plus exp To player -------
    public void takeEXPandUPLevel(int amount)
    {
       PlayerStatusManager.Instance.playerdataSo.CurrentEXP += amount;

        if (PlayerStatusManager.Instance.playerdataSo.CurrentEXP >= PlayerStatusManager.Instance.playerdataSo.maxEXP)
        {
            PlayerStatusManager.Instance.playerdataSo.CurrentEXP = 0;
            PlayerStatusManager.Instance.playerdataSo.playerLevel++;
            PlayerStatusManager.Instance.playerdataSo.maxEXP++;
            PlayerStatusManager.Instance.playerdataSo.maxHP += 10;
            PlayerStatusManager.Instance.playerdataSo.maxEnegy += 10;
            if (PlayerStatusManager.Instance.playerdataSo.MaxLevel >= 100)
            {
                PlayerStatusManager.Instance.playerdataSo.MaxLevel = 100;
            }
        }
       
    }
  //------------------------ Other Funtion -----------------------

    public void DestroyWhenDie()
    {
        Destroy(gameObject,10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f); // Attacking // stop Attacking

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 15f); // Detection

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 16f); // Stop Chassing

    }

    //------------------------------ Va Cham Effect -------------------
    private void OnTriggerStay(Collider collision)
    {
        // 1. -- SMoke Boom --
        if (collision.gameObject.CompareTag("DameByTime"))
        {
            tickTime -= Time.deltaTime;

            if (tickTime < 0 && isDead == false)
            {
                takedameForEnemy(10,50);
                tickTime = 1.0f;
            }
        }
    }
}
