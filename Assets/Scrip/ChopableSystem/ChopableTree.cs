using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopableTree : MonoBehaviour
{
    [Header("Tree Health")]
    public string treeName;
    [SerializeField] private int tree_MaxHP;
    [SerializeField] private int tree_CurrentHP;

    [Header("TreeSystem")]
    public bool isPlayerInRange;
    public bool isCanChop;
    public bool isDeadTree;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject WoodLogPrefab;
    [SerializeField] private TreeSound treeSound;

    [SerializeField] private int EnegysSpentChopping = 5;
    private void Start()
    {

        tree_CurrentHP = tree_MaxHP;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inIt();
    }

    private void inIt()
    {
        if (isCanChop)
        {
            GlobalReferences.Instance.TreeMaxHP = tree_MaxHP;
            GlobalReferences.Instance.TreeCurrentHP = tree_CurrentHP;
        }

    }
    public string GetTreeName()
    {
        return treeName;
    }
    public void TreeGetHit(int minusHP)
    {
        StartCoroutine(hit(minusHP));

    }

    private IEnumerator hit(int minusHP)
    {
        yield return new WaitForSeconds(0f);
        animator.SetTrigger("TreeShake");
        tree_CurrentHP -= minusHP;
        PlayerStatusManager.Instance.playerdataSo.CurrentEnegy -= EnegysSpentChopping;

        if (tree_CurrentHP <= 0)
        {
            StartCoroutine(TreeIsDead());
            animator.SetTrigger("isDead");
            isDeadTree = true;
            treeSound.Play_TreeFall();
        }
        else
        {
            isDeadTree = false;
            SoundManager.Instance.PlayWood_chopSound();

        }
    }

    private IEnumerator TreeIsDead()
    {
        yield return new WaitForSeconds(3f);
        Vector3 treeSpawnPosition = transform.position;
        Destroy(gameObject);
        isCanChop = false;
        InteractionManager.Instance.HoveredSeletedTree = null;
        InteractionManager.Instance.chopHolder.SetActive(false);
        GameObject brokenTree = Instantiate(WoodLogPrefab,
            new Vector3(treeSpawnPosition.x +1 , treeSpawnPosition.y + 0.5f, treeSpawnPosition.z + 1), Quaternion.Euler(0, 0, 0));

    }
}
