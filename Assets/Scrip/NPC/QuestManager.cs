using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

    }

    public List<Quest> AllActiveQuests;
    public List<Quest> AllCompleteQuest;

    [Header("Quest Menu")]
    public GameObject questMenu;
    public bool isQuestMenuOpen;

    public GameObject activeQuestPrefab;
    public GameObject completeQuestPrefab;

    public GameObject QuestMenuContentArea;

    [Header("QuestTracker")]

    public GameObject questTrackerConten;
    public GameObject questtrackerPrefab;

    public List<Quest> AllTrackedQuests;


    //---------

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleQuestMenu();
        }

    }
    public void ToggleQuestMenu()
    {
        questMenu.SetActive(!questMenu.activeSelf);

        if (questMenu.activeInHierarchy)
        {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isQuestMenuOpen = true;
            InteractionManager.Instance.DisableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
        }
        else
        {
            if (!InventorySystem.Instance.isInventoryOpen || !CraftingManager.Instance.IsCraftOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                InteractionManager.Instance.EnableSelection();
                InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
            }
            isQuestMenuOpen = false;

        }
    }

    public void TrackQuests(Quest quest)
    {
        AllTrackedQuests.Add(quest);
        RefreshTrackerQuestList();
    }

    public void RefreshTrackerQuestList()
    {
        foreach (Transform child in questTrackerConten.transform)
        {
            Destroy(child.gameObject);

        }
        foreach (Quest trackedQuest in AllTrackedQuests)
        {
            GameObject trackedPrefab = Instantiate(questtrackerPrefab, Vector3.zero, Quaternion.identity);
            trackedPrefab.transform.SetParent(questTrackerConten.transform, false);

            TrackerRow TRow = trackedPrefab.GetComponent<TrackerRow>();

            TRow.questName.text = trackedQuest.questName;
            TRow.questDescription.text = trackedQuest.questDecription;

            var req1 = trackedQuest.info.firstrequirementItem;
            var req1Amount = trackedQuest.info.firstRequirementAmount;

            var req2 = trackedQuest.info.SecondrequirementItem;
            var req2Amount = trackedQuest.info.SecondRequirementAmount;

            if (trackedQuest.info.SecondrequirementItem != "")
            {
                TRow.questRequirement.text = $"{req1} :" + InventorySystem.Instance.CheckItemAmount(req1) + " / " + $" {req1Amount}" +
                 $" {req2} :" + InventorySystem.Instance.CheckItemAmount(req2) + " / " + $" {req2Amount}";
            }
            else
            {
                TRow.questRequirement.text = $" {req1} :" + InventorySystem.Instance.CheckItemAmount(req1) + " / " + $" {req1Amount}";
            }

            if(trackedQuest.info.hasCheckpoints)
            {
                var existingText = TRow.questRequirement.text;
                TRow.questRequirement.text = PrintcheckPoint(trackedQuest,existingText);
            }
        }
    }

    private string PrintcheckPoint(Quest trackedQuest, string existingText)
    {
        var finalText = existingText;

        foreach(CheckPoints cp in  trackedQuest.info.checkpoints)
        {
            if(cp.isCompleted)
            {
                finalText = finalText + "\n" + cp.checkpointName +"[Completed]";
            }
            else
            {
                finalText = finalText + "\n" + cp.checkpointName; 
            }
        }
        return finalText;
    }

    public void UnTrackQuests(Quest quest)
    {
        AllTrackedQuests.Remove(quest);
        RefreshTrackerQuestList();
    }
    public void MarkQuestCompleted(Quest quest)
    {
        // remove QUest  from list
        AllActiveQuests.Remove(quest);

        // add into the completed quest
        AllCompleteQuest.Add(quest);
        UnTrackQuests(quest);

        RefreshQuestList();
    }

    public void AddActiveQuest(Quest quest)
    {
        AllActiveQuests.Add(quest);
        TrackQuests(quest);
        RefreshQuestList();
    }
    public void RefreshQuestList()
    {
        foreach (Transform child in QuestMenuContentArea.transform)
        {
            Destroy(child.gameObject);

        }


        foreach (Quest activeQuest in AllActiveQuests)
        {
            GameObject questPrefab = Instantiate(activeQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(QuestMenuContentArea.transform, false);

            QuestRow qRow = questPrefab.GetComponent<QuestRow>();
            qRow.thisQuest = activeQuest;

            qRow.questname.text = activeQuest.questName;
            qRow.questGiver.text = activeQuest.questGiver;

            qRow.isActive = true;
            qRow.isTracking = true;

            qRow.coinAmount.text = $"{activeQuest.info.coinReward}";

            if (activeQuest.info.rewardItem_1 != "")
            {
                qRow.firstReward.sprite = GetSpriteForItem(activeQuest.info.rewardItem_1);
                qRow.firstRewardAmount.text = "" ;
            }
            else
            {
                qRow.firstReward.gameObject.SetActive(false);
                qRow.firstRewardAmount.text = "";
            }
            if (activeQuest.info.rewardItem_2 != "")
            {
                qRow.secondReward.sprite = GetSpriteForItem(activeQuest.info.rewardItem_2);
                qRow.secondRewardAmount.text ="";
            }
            else
            {
                qRow.secondReward.gameObject.SetActive(false);
                qRow.secondRewardAmount.text = "";
            }

        }

        //--------------------- 

        foreach (Quest completeQuest in AllCompleteQuest)
        {
            GameObject questPrefab = Instantiate(completeQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(QuestMenuContentArea.transform, false);

            QuestRow ComplRow = questPrefab.GetComponent<QuestRow>();

            ComplRow.questname.text = completeQuest.questName;
            ComplRow.questGiver.text = completeQuest.questGiver;
            ComplRow.isActive = false;
            ComplRow.isTracking = false;

            ComplRow.coinAmount.text = $"{completeQuest.info.coinReward}";

            if (completeQuest.info.rewardItem_1 != "")
            {
                ComplRow.firstReward.sprite = GetSpriteForItem(completeQuest.info.rewardItem_1);
                ComplRow.firstRewardAmount.text ="";
            }
            else
            {
                ComplRow.firstReward.gameObject.SetActive(false);
                ComplRow.firstRewardAmount.text = "";
            }
            if (completeQuest.info.rewardItem_2 != "")
            {
                ComplRow.secondReward.sprite = GetSpriteForItem(completeQuest.info.rewardItem_2);
                ComplRow.secondRewardAmount.text = "";
            }
            else
            {
                ComplRow.secondReward.gameObject.SetActive(false);
                ComplRow.secondRewardAmount.text = "";
            }


        }
    }

    private Sprite GetSpriteForItem(string item)
    {
        var itemToGet = Resources.Load<GameObject>(item);
        return itemToGet.GetComponent<Image>().sprite;
    }
}
