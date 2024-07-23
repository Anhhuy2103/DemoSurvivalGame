using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }
    [Header("Hovered System")]
    public AmmoBox hoveredAmmoBox = null;
    public ThrowAble hoveredThrowable = null;
    public GameObject HoveredSeletedTree = null;
    public GameObject HoveredSeletedStone = null;
    public GameObject hoveredSelectedObject_JustShow = null;
    public GameObject hoveredSelectedObject = null;
    public GameObject hoveredEnemyCreep = null;
    public GameObject hoveredEnemyBig = null;
    public GameObject hoveredStorageBox = null;


  [Header(("Other"))]
    public GameObject Player;
    public GameObject middleCross;
    public float distanceToPickUp = 5f;
    public float distanceToHit = 4f;

    [Header("Interaction Point UI")]
    // --- NPC ---
    public TextMeshProUGUI interactionText;
    public GameObject interactionPoint_UI;

    // --- Item ---
    [Header("ItemPickUp")]
    [SerializeField] private GameObject interaction_Info_UI;
    TextMeshProUGUI interactionItem_text;
    public bool playerInRange;
    public bool onTarget;
    public bool isActiveWeaponInterac;
    public Image centerDotImage;
    public Image handIcon;
    public Text timeDecaytext;

    //-- Item No F to Pickup --
    [Header("Item Just no Pickup by F key")]
    public GameObject interactionNoDes_Info_UI;
    TextMeshProUGUI interactionNoDes_text;
  

    [Header("Chopable System")]  
    public GameObject chopHolder;
    public GameObject ResourcesHPbar;
    public Text nameOfTreeText;
    public Text DameText;

    
  
    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        playerInRange = true;
        onTarget = false;
        interactionItem_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
        interactionNoDes_text = interactionNoDes_Info_UI.GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitbyRayCast = hit.transform.gameObject;
            float distanceFromPlayer = Vector3.Distance(Player.transform.position, objectHitbyRayCast.transform.position);


            // ------------------------ EnemyBig ------------------------------------------------------
            Enemy enemyBIg = objectHitbyRayCast.GetComponent<Enemy>();
            if (enemyBIg && distanceFromPlayer < distanceToHit)
            {
                chopHolder.gameObject.SetActive(true);             
                nameOfTreeText.text = enemyBIg.GetEnemyName();
                hoveredEnemyBig = enemyBIg.gameObject;
                enemyBIg.isCanMelee = true;
            }
            else
            {
                if (hoveredEnemyBig != null)
                {
                    
                    hoveredEnemyBig.gameObject.GetComponent<Enemy>().isCanMelee = false;
                    hoveredEnemyBig = null;
                    chopHolder.gameObject.SetActive(false);
                    nameOfTreeText.text = "";
                }
            }
            // ------------------------ EnemyCreep ------------------------------------------------------
            EnemyCreep enemyCreep = objectHitbyRayCast.GetComponent<EnemyCreep>();
            if(enemyCreep && distanceFromPlayer < distanceToHit)
            {
                chopHolder.gameObject.SetActive(true);              
                nameOfTreeText.text = enemyCreep.GetEnemyName();
                hoveredEnemyCreep = enemyCreep.gameObject;
                enemyCreep.isCanMelee = true;
            }
            else
            {
                if(hoveredEnemyCreep != null)
                {
                   
                    hoveredEnemyCreep.gameObject.GetComponent<EnemyCreep>().isCanMelee = false;
                    hoveredEnemyCreep = null;
                    chopHolder.gameObject.SetActive(false);
                    nameOfTreeText.text = "";
                }
                hoveredEnemyCreep = null;
                chopHolder.gameObject.SetActive(false);
                nameOfTreeText.text = "";
            }



            //------------------------- CHOPABLE:  * TREE * ----------------------------------------------
            ChopableTree chopableTree = objectHitbyRayCast.GetComponent<ChopableTree>();
            if (chopableTree && distanceFromPlayer < distanceToPickUp)
            {
              
                chopableTree.isPlayerInRange = true;
                chopableTree.isCanChop = true;
                HoveredSeletedTree = chopableTree.gameObject;
                chopHolder.gameObject.SetActive(true);
                nameOfTreeText.text = chopableTree.GetTreeName();
                
            }
            else
            {
                if (HoveredSeletedTree != null)
                {
                    HoveredSeletedTree.gameObject.GetComponent<ChopableTree>().isCanChop = false;
                    HoveredSeletedTree = null;
                    chopHolder.gameObject.SetActive(false);
                    nameOfTreeText.text = "";
                }
            }
            //------------------------- CHOPABLE:  * Stone * ----------------------------------------------
            ChopableStone chopableStone = objectHitbyRayCast.GetComponent<ChopableStone>();
            if (chopableStone && distanceFromPlayer < distanceToPickUp)
            {
               
                chopableStone.isPlayerInRange = true;
                chopableStone.isCanChop = true;
                HoveredSeletedStone = chopableStone.gameObject;
                chopHolder.gameObject.SetActive(true);
                nameOfTreeText.text = chopableStone.GetTreeName();

            }
            else
            {
                if (HoveredSeletedStone != null)
                {
                    HoveredSeletedStone.gameObject.GetComponent<ChopableStone>().isCanChop = false;
                    HoveredSeletedStone = null;
                    chopHolder.gameObject.SetActive(false);
                    nameOfTreeText.text = "";
                }
            }








            // --------------------------------- Weapon-Infomation  ----------------------------------
            JustShow_InteractableObject justShow = objectHitbyRayCast.GetComponent<JustShow_InteractableObject>();
            if (justShow && distanceFromPlayer < distanceToPickUp)
            {
                hoveredSelectedObject_JustShow = justShow.gameObject;
                interactionNoDes_text.text = justShow.GetItemName();
                interactionNoDes_Info_UI.SetActive(true);
            }
            else
            {
                hoveredSelectedObject_JustShow = null;
                interactionNoDes_Info_UI.SetActive(false);
            }

            // --------------------------------- Item-Infomation  ----------------------------------

            InteractableObject interactableObject = objectHitbyRayCast.GetComponent<InteractableObject>();
            if (interactableObject && distanceFromPlayer < distanceToPickUp)
            {
                if(interactableObject.itemDecayMode == InteractableObject.typeOfDecayItem.canDecay)
                {
                    timeDecaytext.gameObject.SetActive(true);
                    interactableObject.interactable = true;
                }
                else if (interactableObject.itemDecayMode == InteractableObject.typeOfDecayItem.noDecay)
                {
                    timeDecaytext.gameObject.SetActive(false);
                    interactableObject.interactable = false;
                }
                
                    isActiveWeaponInterac = true;
                    hoveredSelectedObject = interactableObject.gameObject;
                    interactionItem_text.text = interactableObject.GetItemName();
                    interaction_Info_UI.SetActive(true);

                    playerInRange = true;
                    onTarget = true;

                
              

                if (interactableObject.CompareTag("Pickable"))
                {
                    centerDotImage.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                }
                else
                {
                    centerDotImage.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false); // fix here
                }
                if (interactableObject.CompareTag("Weapon")&& GlobalReferences.Instance.isActiveEquipnbro)
                {
                    timeDecaytext.gameObject.SetActive(false);
                }
                if (interactableObject.CompareTag("Weapon") && GlobalReferences.Instance.isActiveWeaponbro)
                {
                    timeDecaytext.gameObject.SetActive(false);
                }

            }
            else
            {
               if(hoveredSelectedObject!= null)
                {
                    hoveredSelectedObject.gameObject.GetComponent<InteractableObject>().interactable = false;
                    hoveredSelectedObject = null;
                    timeDecaytext.gameObject.SetActive(false);                 
                }
                else
                {
                    timeDecaytext.gameObject.SetActive(false);
                    isActiveWeaponInterac = false;
                    onTarget = false;
                    interaction_Info_UI.SetActive(false);
                    playerInRange = false;
                    centerDotImage.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false); // fix here
                }
                
            }
            //- ------------------------------ PLACEMENT ---------------------------------

            StorageBox storageBox = objectHitbyRayCast.GetComponent<StorageBox>();
            if(storageBox && storageBox.playerInRange && !PlacementSystem.Instance.inPlacementMode)
            {
                interactionItem_text.text = "E To Open";
                interaction_Info_UI.SetActive(true);

                hoveredStorageBox = storageBox.gameObject;
                if(Input.GetKeyDown(KeyCode.E))
                {
                    StorageManager.Instance.OpenBox(storageBox);
                }
            }
            else
            {
                if(hoveredStorageBox != null)
                {
                    hoveredStorageBox = null;
                }
            }


            // --------------------------------- NCP ---------------------------------

            NPC npc = objectHitbyRayCast.GetComponent<NPC>();
            if (npc && npc.playerInrange)

            {
                interactionText.text = "'F' To Talk";
                interactionPoint_UI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F) && objectHitbyRayCast.GetComponent<NPC>().isTalkingWithPlayer == false)
                {
                    objectHitbyRayCast.GetComponent<NPC>().StartConversation();
                }
                if (DialogManager.Instance.isDiablogUIActive)
                {
                    middleCross.SetActive(false);
                    interactionPoint_UI.SetActive(false);
                    //interactionWeaponPoint_UI.SetActive(false);
                }
            }
            else
            {
                interactionText.text = "";
                interactionPoint_UI.SetActive(false);

            }
            // --------------------------------- Item-AMMO BOX PICKUP ----------------------------------

            if (objectHitbyRayCast.GetComponent<AmmoBox>() && distanceFromPlayer < distanceToPickUp)
            {
                //// Tat cai Outline khi quay cam vao ca 2 weapon
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outlinee>().enabled = false;

                }

                hoveredAmmoBox = objectHitbyRayCast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outlinee>().enabled = true;


                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickUpAmmoBox(hoveredAmmoBox);
                    Destroy(objectHitbyRayCast.gameObject);

                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outlinee>().enabled = false;

                }
            }


            // --------------------------------- Item-GRENADE COCKTAIL BOX PICKUP ----------------------------------

            if (objectHitbyRayCast.GetComponent<ThrowAble>() && distanceFromPlayer < distanceToPickUp)
            {
                //// Tat cai Outline khi quay cam vao ca 2 weapon
                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outlinee>().enabled = false;

                }

                hoveredThrowable = objectHitbyRayCast.gameObject.GetComponent<ThrowAble>();
                hoveredThrowable.GetComponent<Outlinee>().enabled = true;


                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickUpThrowable(hoveredThrowable);

                }

            }
            else
            {

                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outlinee>().enabled = false;

                }
            }
            if(!npc && !interactableObject && !chopableTree && !chopableStone && !storageBox)
            {
                interactionItem_text.text = "";
                interaction_Info_UI.gameObject.SetActive(false);
            }

        }
        else
        {

            onTarget = false;
            interaction_Info_UI.SetActive(false);
            timeDecaytext.gameObject.SetActive(false);
            centerDotImage.gameObject.SetActive(true);
            handIcon.gameObject.SetActive(false); // fix here
            
        }

    }

    public void DisableSelection()
    {
        handIcon.enabled = false;
        centerDotImage.enabled = false;
        interaction_Info_UI.SetActive(false);
        hoveredSelectedObject = null;


    }

    public void EnableSelection()
    {
        handIcon.enabled = true;
        centerDotImage.enabled = true;
        interaction_Info_UI.SetActive(true);

    }

   
}
