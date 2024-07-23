using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Guns;

[RequireComponent(typeof(Animator))]
public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private StatusItemSO itemDataSo;
    [SerializeField] private int MeleeDame;
    [SerializeField] private int MaxMeleeDame;
    public int EquipItemmaxHeal;
    public int EquipItemcurrentHeal;
    public bool isEquipableActive;
    // Spawn Position Weapon
    [Header("Spawn Position Melee")]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    [Header("MeleeHit Direction")]
    public bool isMeleeShooting, readyToShoot;
    bool allowReset;
    public float shootingDelay = 2f;

    [Header("Spawn Point Effect")]
    [SerializeField] private Transform effectSpawnPoint;
    [SerializeField] private GameObject ChopChopBulet;




    public enum melleeMode
    {
        Single,
        Auto
    }
    public melleeMode CurrentMelleetype;

    private void Start()
    {
        readyToShoot = true;
        allowReset = true;
        animator = GetComponent<Animator>();
        EquipItemmaxHeal = itemDataSo.MaxHP;
        EquipItemcurrentHeal = EquipItemmaxHeal;
    }

    private void Update()
    {
        if (isEquipableActive 
            && GlobalReferences.Instance.isActiveEquipnbro 
            && !InventorySystem.Instance.isInventoryOpen 
            && !CraftingManager.Instance.IsCraftOpen
            && !DialogManager.Instance.isDiablogUIActive 
            && !IngameMenuManager.Instance.isActiveMenuPanel 
            && !PlayerStatusManager.Instance.isDead
            && !ConstructionManager.Instance.inConstructionMode 
            && !QuestManager.Instance.isQuestMenuOpen)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("WeaponRender");
            }
            if (CurrentMelleetype == melleeMode.Auto)
            {
                //hold to hit
                isMeleeShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (CurrentMelleetype == melleeMode.Single)

            {
                // clicking to shoot

                isMeleeShooting = Input.GetKeyDown(KeyCode.Mouse0);

            }
            if (readyToShoot && isMeleeShooting)
            {

                animator.SetTrigger("Hit");
                SoundManager.Instance.SFXSource.PlayOneShot(SoundManager.Instance.mellee_clip); ;
                readyToShoot = false;

                if (allowReset)
                {
                    Invoke("ResetShot", shootingDelay);
                    allowReset = false;
                }
            }

        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        if (EquipItemcurrentHeal <= 0)
        {

            Destroy(gameObject, 0.1f);
            SoundManager.Instance.Play_BrokenItemSound();
        }
    }

    public void HitDirection()
    {
        //music here
        //----------------------- Case Stone --------------------------
        GameObject selecledForeverSton = InteractionManager.Instance.HoveredSeletedStone;
        if (selecledForeverSton != null)
        {
            if (selecledForeverSton.GetComponent<ChopableStone>().isDeadStone == false)
            {
                selecledForeverSton.GetComponent<ChopableStone>().StoneGetHit(Random.Range(MeleeDame, MaxMeleeDame));
                selecledForeverSton.GetComponent<ChopableStone>().Hitcounter += 1;
                EquipItemcurrentHeal -= itemDataSo.minusHealValue;
            }
            SpawnEffectChop();

        }

        //----------------------- Case Tree --------------------------
        GameObject selecledTree = InteractionManager.Instance.HoveredSeletedTree;
        if (selecledTree != null)
        {
            if (selecledTree.GetComponent<ChopableTree>().isDeadTree == false)
            {
                selecledTree.GetComponent<ChopableTree>().TreeGetHit(Random.Range(MeleeDame, MaxMeleeDame));
                EquipItemcurrentHeal -= itemDataSo.minusHealValue;
            }
            SpawnEffectChop();
        }
        //----------------------- EnemyCreep --------------------------
        GameObject selectedEnemy = InteractionManager.Instance.hoveredEnemyCreep;
        if (selectedEnemy != null)
        {
            if (selectedEnemy.GetComponent<EnemyCreep>().isDead == false)
            {
                selectedEnemy.GetComponent<EnemyCreep>().takedameForEnemy(MeleeDame, MaxMeleeDame);
                EquipItemcurrentHeal -= itemDataSo.minusHealValue;
            }
            SpawnEffectChop();
        }

        //----------------------- EnemyBig --------------------------
        GameObject selectedEnemyBig = InteractionManager.Instance.hoveredEnemyBig;
        if (selectedEnemyBig != null)
        {
            if (selectedEnemyBig.GetComponent<Enemy>().isDead == false)
            {
                selectedEnemyBig.GetComponent<Enemy>().takedameForEnemy(MeleeDame, MaxMeleeDame);
                EquipItemcurrentHeal -= itemDataSo.minusHealValue;
            }
            SpawnEffectChop();
        }


    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    //-------InstantiteEffec---------//

    public void SpawnEffectChop()
    {
        Instantiate(GlobalReferences.Instance.Chop_WoodStoneEffect_Prefab, effectSpawnPoint.position, Quaternion.identity);
    }
}
