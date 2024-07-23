using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCreep : MonoBehaviour
{
    [Header("Enemy Name: ")]

    public string EnemyName;

    [Header("Enemy Info: ")]
    [SerializeField] private StatusItemSO enemyCreepSo;
    [SerializeField] private int EnemyCurrentHP;
    [SerializeField] private int EnemyMaxHP;
    [SerializeField] private int EnegysSpentMelee = 2;
    [Header("ColliderSystem")]
    public bool atackReady;
    public CapsuleCollider handCollider;

    private Animator animator;
    [SerializeField] private NavMeshAgent navAgent;
    public bool isDead;
    public bool isCanMelee;



    [Header("ItemPrefabSpawn System")]
    public GameObject itemPrefab1;
    public GameObject itemPrefab2;
    public GameObject itemPrefab3;

    public Transform itemSpawnPoint;

    [Header("EffectToEnemy System")]
    public float tickTime;




    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        handCollider.enabled = true;
        EnemyMaxHP = enemyCreepSo.MaxHP;
        EnemyCurrentHP = EnemyMaxHP;

    }

    private void Update()
    {
        init();
    }



    public string GetEnemyName()
    {
        return EnemyName;
    }
    public void TurnOnHandCollider()
    {
        handCollider.enabled = true;
    }

    public void TurnOffHandCollider()
    {
        handCollider.enabled = false;
    }
    private void init()
    {
        if (isCanMelee)
        {
            GlobalReferences.Instance.EnemyMaxHP = EnemyMaxHP;
            GlobalReferences.Instance.EnemyCurrentHP = EnemyCurrentHP;
        }
    }
    int _randomDame;
    int _maxMeleeDame;


    public void takedameForEnemy(int minDameAmount, int MaxDameAmount)
    {
        int randomDame = Random.Range(minDameAmount, MaxDameAmount);
        _randomDame = randomDame;
        _maxMeleeDame = MaxDameAmount;
        EnemyCurrentHP -= randomDame;
        StartCoroutine(DisplayRandomDame());
        PlayerStatusManager.Instance.playerdataSo.CurrentEnegy -= EnegysSpentMelee;
        if (EnemyCurrentHP <= 0)
        {


            SoundManager.Instance.Turtle_Dead();
            animator.SetTrigger("DIE1");

            SpawnItemWhenDie();
            PlayerStatusManager.Instance.takeEXPandUPLevel(enemyCreepSo.BonusHP);
            if (PlayerStatusManager.Instance.playerdataSo.isLevelUp)
            {
                enemyCreepSo.BonusHP += 2;
            }

            handCollider.enabled = false;
            isDead = true;
            isCanMelee = false;
            StartCoroutine(PlaySound_ChaseToBackGround());
            DestroyWhenDie();

            InteractionManager.Instance.hoveredEnemyCreep = null;
            InteractionManager.Instance.chopHolder.SetActive(false);
        }
        else
        {
            animator.SetTrigger("HURT");
            SoundManager.Instance.Turtle_Hurt();
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

        yield return new WaitForSeconds(1f);
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
        if (randomvalue == 1)
        {
            GameObject itemDrop = Instantiate(itemPrefab2, itemSpawnPoint.position, Quaternion.identity);
        }
        if (randomvalue == 3)
        {
            GameObject itemDrop = Instantiate(itemPrefab3, itemSpawnPoint.position, Quaternion.identity);
        }
            
    }


    //------------------------ Other Funtion -----------------------

    public void DestroyWhenDie()
    {
        Destroy(gameObject, 2);
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
                takedameForEnemy(10, 50);
                tickTime = 1.0f;
            }
        }
    }

    //----------------------- Melee Funtion ----------------------------
    public void EnemyGetHit(int minusHP)
    {
        StartCoroutine(hit(minusHP));

    }

    private IEnumerator hit(int minusHP)
    {
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("HURT");
        EnemyCurrentHP -= minusHP;


        if (EnemyCurrentHP <= 0)
        {
            StartCoroutine(TreeIsDead());
            animator.SetTrigger("isDead");
            isDead = true;

        }
        else
        {
            isDead = false;
            SoundManager.Instance.Turtle_Hurt();
        }
    }

    private IEnumerator TreeIsDead()
    {
        yield return new WaitForSeconds(3f);
        Vector3 treeSpawnPosition = transform.position;
        Destroy(gameObject);
        isDead = false;
        InteractionManager.Instance.HoveredSeletedTree = null;
        InteractionManager.Instance.chopHolder.SetActive(false);

        SpawnItemWhenDie();

    }
}
