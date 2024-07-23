using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopableStone : MonoBehaviour
{
    public string stoneName;
    [Header("Stone Health")]

    [SerializeField] private int stone_MaxHP;
    [SerializeField] private int stone_CurrentHP;
    public int Hitcounter = 0;

    [Header("TreeSystem")]
    public bool isPlayerInRange;
    public bool isCanChop;
    public bool isDeadStone;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject StonePrefab;
    [SerializeField] private Transform StoneSpawnPoint;
    [SerializeField] private StoneSound stoneSound;

    [SerializeField] private int EnegysSpentChopping = 5;
    private void Start()
    {

        stone_CurrentHP = stone_MaxHP;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inIt();
        if (Hitcounter >= 4)
        {
            GameObject BrokenStone = Instantiate(StonePrefab,
            StoneSpawnPoint.position, Quaternion.Euler(0, 0, 0));
            Hitcounter = 0;
        }
    }

    private void inIt()
    {
        if (isCanChop)
        {
            GlobalReferences.Instance.StoneMaxHP = stone_MaxHP;
            GlobalReferences.Instance.StoneHP = stone_CurrentHP;
        }

    }
    public string GetTreeName()
    {
        return stoneName;
    }
    public void StoneGetHit(int minusHP)
    {
        StartCoroutine(hit(minusHP));

    }

    private IEnumerator hit(int minusHP)
    {



        yield return new WaitForSeconds(0f);
        animator.SetTrigger("StoneShake");
        stone_CurrentHP -= minusHP;
        PlayerStatusManager.Instance.playerdataSo.CurrentEnegy -= EnegysSpentChopping;
        if (stone_CurrentHP <= 0)
        {
            StartCoroutine(TreeIsDead());
            animator.SetTrigger("isDead");
            isDeadStone = true;
            stoneSound.Play_StoneFall();
        }
        else
        {

            isDeadStone = false;
            SoundManager.Instance.PlayWood_chopSound();

        }

    }

    private IEnumerator TreeIsDead()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        isCanChop = false;
        InteractionManager.Instance.HoveredSeletedStone = null;
        InteractionManager.Instance.chopHolder.SetActive(false);
        GameObject BrokenStone = Instantiate(StonePrefab,
            StoneSpawnPoint.position, Quaternion.Euler(0, 0, 0));

    }
}
