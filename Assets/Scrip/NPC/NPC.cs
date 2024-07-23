using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public bool playerInrange;
    public bool isTalkingWithPlayer;

    private TextMeshProUGUI npcDiablogText;

    private Button optionBTN_1;
    private TextMeshProUGUI optionBTN_2_diablogText;

    private Button optionBTN_2;
    private TextMeshProUGUI optionBTN_1_diablogText;

    [Header("QuestSysTem")]

    public List<Quest> quests;
    public Quest currentActiveQuest = null;
    public int activeQuestIndex = 0;
    public bool firstTimeInteraction = true;
    public int currentDiablog;


    private void Start()
    {
        OptionDescription_Diablog();
    }

    private void OptionDescription_Diablog()
    {
        npcDiablogText = DialogManager.Instance.diablogText;

        optionBTN_1 = DialogManager.Instance.optionBTN_1;
        optionBTN_1_diablogText = DialogManager.Instance.optionBTN_1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        optionBTN_2 = DialogManager.Instance.optionBTN_2;
        optionBTN_2_diablogText = DialogManager.Instance.optionBTN_2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInrange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInrange = false;
        }

    }

    public void StartConversation()
    {
        isTalkingWithPlayer = true;
        LookAtPlayer();

        if (firstTimeInteraction) // interacting with the npc at the firsttime
        {
            firstTimeInteraction = false;
            currentActiveQuest = quests[activeQuestIndex];
            StartQuestInitialDiablog();
            currentDiablog = 0;
        }
        else   // interacting with the npc after the firsttime
        {
            // If we return after declining the quest
            if (currentActiveQuest.Decline)
            {

                DialogManager.Instance.OpenDiablogUI();

                npcDiablogText.text = currentActiveQuest.info.comebackAfterDecline;

                SetAcceptAndDeclineOptions();
            }


            // If we return while the quest is still in progress
            if (currentActiveQuest.Accepted && currentActiveQuest.IsCompleted == false)
            {
                if (AreQuestRequirmentsCompleted())
                {

                    SubmitRequiredItems();

                    DialogManager.Instance.OpenDiablogUI();

                    npcDiablogText.text = currentActiveQuest.info.combackComplete;

                    optionBTN_1_diablogText.text = "[Take Reward]";
                    optionBTN_1.onClick.RemoveAllListeners();
                    optionBTN_1.onClick.AddListener(() =>
                    {
                        ReceiveRewardAndCompleteQuest();
                    });
                }
                else
                {
                    DialogManager.Instance.OpenDiablogUI();

                    npcDiablogText.text = currentActiveQuest.info.comebackInprogress;

                    optionBTN_1_diablogText.text = "[Close]";
                    optionBTN_1.onClick.RemoveAllListeners();
                    optionBTN_1.onClick.AddListener(() =>
                    {
                        DialogManager.Instance.CloseDiablogUI();
                        isTalkingWithPlayer = false;
                    });
                }
            }

            if (currentActiveQuest.IsCompleted == true)
            {
                DialogManager.Instance.OpenDiablogUI();

                npcDiablogText.text = currentActiveQuest.info.finalWords;

                optionBTN_1_diablogText.text = "[Close]";
                optionBTN_1.onClick.RemoveAllListeners();
                optionBTN_1.onClick.AddListener(() =>
                {
                    DialogManager.Instance.CloseDiablogUI();
                    
                    isTalkingWithPlayer = false;
                });
            }

            // If there is another quest available
            if (currentActiveQuest.InitDiablogCompleted == false)
            {
                StartQuestInitialDiablog();
            }

        }
    }

    private void SubmitRequiredItems()
    {
        string firstRequiredItem = currentActiveQuest.info.firstrequirementItem;
        int firstRequiredAmount = currentActiveQuest.info.firstRequirementAmount;

        if (firstRequiredItem != "")
        {
            InventorySystem.Instance.RemoveItem(firstRequiredItem, firstRequiredAmount);
        }


        string secondtRequiredItem = currentActiveQuest.info.SecondrequirementItem;
        int secondRequiredAmount = currentActiveQuest.info.SecondRequirementAmount;

        if (firstRequiredItem != "")
        {
            InventorySystem.Instance.RemoveItem(secondtRequiredItem, secondRequiredAmount);
        }
    }

    private bool AreQuestRequirmentsCompleted()
    {
        print("Checking Requirments");

        // First Item Requirment

        string firstRequiredItem = currentActiveQuest.info.firstrequirementItem;
        int firstRequiredAmount = currentActiveQuest.info.firstRequirementAmount;

        var firstItemCounter = 0;

        foreach (string item in InventorySystem.Instance.itemList)
        {
            if (item == firstRequiredItem)
            {
                firstItemCounter++;
            }
        }

        // Second Item Requirment -- If we dont have a second item, just set it to 0

        string secondRequiredItem = currentActiveQuest.info.SecondrequirementItem;
        int secondRequiredAmount = currentActiveQuest.info.SecondRequirementAmount;

        var secondItemCounter = 0;

        foreach (string item in InventorySystem.Instance.itemList)
        {
            if (item == secondRequiredItem)
            {
                secondItemCounter++;
            }
        }

        SetQuestHasCheckPoint(currentActiveQuest);

        bool AllCheckPointsCompleted = false;

        if (currentActiveQuest.info.hasCheckpoints)
        {
            foreach (CheckPoints cp in currentActiveQuest.info.checkpoints)
            {
                if (cp.isCompleted == false)
                {
                    AllCheckPointsCompleted = false;
                    break;
                }

                AllCheckPointsCompleted = true;
            }
        }




        if (firstItemCounter >= firstRequiredAmount && secondItemCounter >= secondRequiredAmount)
        {
            if (currentActiveQuest.info.hasCheckpoints)
            {
                if (AllCheckPointsCompleted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }
    }

    private void SetQuestHasCheckPoint(Quest ActiveQuest)
    {

        if (ActiveQuest.info.checkpoints.Count > 0)
        {
            ActiveQuest.info.hasCheckpoints = true;
        }
        else
        {
            ActiveQuest.info.hasCheckpoints = false;
        }
    }

    private void StartQuestInitialDiablog()
    {
        DialogManager.Instance.OpenDiablogUI();
        npcDiablogText.text = currentActiveQuest.info.initialDialog[currentDiablog];
        optionBTN_1_diablogText.text = "NEXT";
        optionBTN_1.onClick.RemoveAllListeners();
        optionBTN_1.onClick.AddListener(() =>
        {

            currentDiablog++;
            CheckIfDialogDone();

        }
        );
        optionBTN_2.gameObject.SetActive(false);
    }

    private void SetAcceptAndDeclineOptions()
    {
        optionBTN_1_diablogText.text = currentActiveQuest.info.accepOption;
        optionBTN_1.onClick.RemoveAllListeners();
        optionBTN_1.onClick.AddListener(() =>
        {
            AcceptedQuest();
        });

        optionBTN_2.gameObject.SetActive(true);
        optionBTN_2_diablogText.text = currentActiveQuest.info.declineOption;
        optionBTN_2.onClick.RemoveAllListeners();
        optionBTN_2.onClick.AddListener(() =>
        {
            DeclinedQuest();
        });
    }
    private void AcceptedQuest()
    {
        QuestManager.Instance.AddActiveQuest(currentActiveQuest);

        currentActiveQuest.Accepted = true;
        currentActiveQuest.Decline = false;

        if (currentActiveQuest.HasNoRequirement)
        {
            npcDiablogText.text = currentActiveQuest.info.combackComplete;
            optionBTN_1_diablogText.text = "[Take Reward]";
            optionBTN_1.onClick.RemoveAllListeners();
            optionBTN_1.onClick.AddListener(() =>
            {
                ReceiveRewardAndCompleteQuest();
            });
            optionBTN_2.gameObject.SetActive(false);
        }
        else
        {
            npcDiablogText.text = currentActiveQuest.info.acceptAnswer;
            CloseTheDiablogUI();
        }


    }

    private void CloseTheDiablogUI()
    {
        optionBTN_1_diablogText.text = "[Close]";
        optionBTN_1.onClick.RemoveAllListeners();
        optionBTN_1.onClick.AddListener(() =>
        {
            DialogManager.Instance.CloseDiablogUI();
            isTalkingWithPlayer = false;
        });
        optionBTN_2.gameObject.SetActive(false);

    }

    private void DeclinedQuest()
    {
        currentActiveQuest.Decline = true;
        npcDiablogText.text = currentActiveQuest.info.declineAnswer;
        CloseTheDiablogUI();
    }
    private void ReceiveRewardAndCompleteQuest()
    {
        QuestManager.Instance.MarkQuestCompleted(currentActiveQuest);
        currentActiveQuest.IsCompleted = true;

        var coinsRecieved = currentActiveQuest.info.coinReward;
        PlayerStatusManager.Instance.IncreaseMoney(coinsRecieved);
        print("You recieved " + coinsRecieved + " gold coins");

        if (currentActiveQuest.info.rewardItem_1 != "")
        {
            InventorySystem.Instance.AddToInventory(currentActiveQuest.info.rewardItem_1);
        }

        if (currentActiveQuest.info.rewardItem_2 != "")
        {
            InventorySystem.Instance.AddToInventory(currentActiveQuest.info.rewardItem_2);
        }

        activeQuestIndex++;

        // Start Next Quest 
        if (activeQuestIndex < quests.Count)
        {
            currentActiveQuest = quests[activeQuestIndex];
            currentDiablog = 0;
            DialogManager.Instance.CloseDiablogUI();
            isTalkingWithPlayer = false;
        }
        else
        {
            DialogManager.Instance.CloseDiablogUI();
            isTalkingWithPlayer = false;
            print("No more quests");
        }

    }



    private void CheckIfDialogDone()
    {
        if (currentDiablog == currentActiveQuest.info.initialDialog.Count - 1) // If its the last dialog 
        {
            npcDiablogText.text = currentActiveQuest.info.initialDialog[currentDiablog];

            currentActiveQuest.InitDiablogCompleted = true;

            SetAcceptAndDeclineOptions();
        }
        else  // If there are more dialogs
        {
            npcDiablogText.text = currentActiveQuest.info.initialDialog[currentDiablog];

            optionBTN_1_diablogText.text = "Next";
            optionBTN_1.onClick.RemoveAllListeners();
            optionBTN_1.onClick.AddListener(() =>
            {
                currentDiablog++;
                CheckIfDialogDone();
            });
        }
    }


    public void LookAtPlayer()
    {
        var player = PlayerStatusManager.Instance.playerBody.transform;
        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
