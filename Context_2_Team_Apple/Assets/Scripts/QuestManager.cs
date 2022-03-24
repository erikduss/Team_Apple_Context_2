using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<IQuest> allAvailableQuests = new List<IQuest>();
    private List<IQuest> allCompletedQuests = new List<IQuest>();

    [SerializeField] private List<InteractableNPC> npcs = new List<InteractableNPC>();

    [SerializeField] private GameObject diverQuestObject;
    [SerializeField] private GameObject fisherQuestObject;
    [SerializeField] private GameObject explorerQuestObject;

    [SerializeField] private GameObject pirateQuestObject1;
    [SerializeField] private GameObject pirateQuestObject2;

    // Start is called before the first frame update
    void Awake()
    {
        allAvailableQuests.Add(new ExampleQuest{ questID = 0 });
        allAvailableQuests.Add(new DiverQuest{ questID = 1 });
        allAvailableQuests.Add(new PirateQuest { questID = 2 });
        allAvailableQuests.Add(new ExplorerQuest { questID = 3 });
        allAvailableQuests.Add(new FisherQuest { questID = 4 });
    }

    public bool CanProgressQuestStep(int questID, int questStepID)
    {
        try
        {
            IQuest questToCheck = allAvailableQuests.Where(a => a.questID == questID).First();

            if(questToCheck.currentQuestStep == questStepID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    //use this function to attach quests to certain npcs
    public IQuest GetQuest(int id)
    {
        if (allAvailableQuests.Count < 1) return null;

        IQuest returnQuest;

        try
        {
            returnQuest = allAvailableQuests.Where(a => a.questID == id).First();
            return returnQuest;
        }
        catch
        {
            return null;
        }
    }

    public void AcceptQuest(int _questID, int _questStepID)
    {
        IQuest activeQuest = allAvailableQuests.Where(a => a.questID == _questID).First();

        QuestStep step = activeQuest.questSteps.Where(b => b.questStepID == _questStepID).First();

        if (activeQuest.questStatus == QuestStatus.NOT_ACCEPTED)
        {
            activeQuest.questStatus = QuestStatus.ONGOING;

            if (activeQuest.currentQuestStep == _questStepID)
            {
                step.CompleteQuestStep();

                CheckForNessecaryActions(activeQuest);

                activeQuest.CheckForNextSteps();
            }
            else
            {
                Debug.Log("Failed to complete quest step: The quest is active, but the current quest step and the quest step to complete do not match.");
            }
        }
    }

    public void CompleteQuestStep(int _questID, int _questStepID)
    {
        try
        {
            IQuest activeQuest = allAvailableQuests.Where(a => a.questID == _questID).First();

            QuestStep step = activeQuest.questSteps.Where(b => b.questStepID == _questStepID).First();

            if(activeQuest.questStatus == QuestStatus.ONGOING)
            {
                if(activeQuest.currentQuestStep == _questStepID)
                {
                    step.CompleteQuestStep();

                    CheckForNessecaryActions(activeQuest);

                    activeQuest.CheckForNextSteps();
                }
                else
                {
                    Debug.Log("Failed to complete quest step: The quest is active, but the current quest step and the quest step to complete do not match.");
                }
            }
            else
            {
                Debug.Log("Quest not active");
            }
        }
        catch
        {
            Debug.Log("Failed to complete Quest: " + _questID + " Step: " + _questStepID);
        }
    }

    private void CheckForNessecaryActions(IQuest quest)
    {
        //Diver quest actions
        if(quest.questID == 1)
        {
            if(quest.currentQuestStep == 0)
            {
                npcs[0].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
                diverQuestObject.SetActive(true);
            }
            else if(quest.currentQuestStep == 1)
            {
                diverQuestObject.SetActive(false);
                npcs[0].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if(quest.currentQuestStep == 2)
            {
                npcs[0].ChangeNPCStatus(NPCStatus.NONE);
                npcs[0].gameObject.layer = 0;
            }
        }

        if (quest.questID == 4)
        {
            if (quest.currentQuestStep == 0)
            {
                npcs[2].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
                fisherQuestObject.SetActive(true);
            }
            else if (quest.currentQuestStep == 1)
            {
                fisherQuestObject.SetActive(false);
                npcs[2].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if (quest.currentQuestStep == 2)
            {
                npcs[2].ChangeNPCStatus(NPCStatus.NONE);
                npcs[2].gameObject.layer = 0;

                //this quest completion enabled the explorer's quest.
                npcs[1].ChangeNPCStatus(NPCStatus.HAS_QUEST_START);
                npcs[1].gameObject.layer = 6;
            }
        }

        if (quest.questID == 3)
        {
            if (quest.currentQuestStep == 0)
            {
                npcs[1].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
                explorerQuestObject.SetActive(true);
            }
            else if (quest.currentQuestStep == 1)
            {
                explorerQuestObject.SetActive(false);
                npcs[1].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if (quest.currentQuestStep == 2)
            {
                npcs[1].ChangeNPCStatus(NPCStatus.NONE);
                npcs[1].gameObject.layer = 0;
            }
        }

        if (quest.questID == 2)
        {
            if (quest.currentQuestStep == 0)
            {
                pirateQuestObject1.SetActive(true);
            }
            else if (quest.currentQuestStep == 1)
            {
                pirateQuestObject1.SetActive(false);
                pirateQuestObject2.SetActive(true);

                for(int i=0; i<npcs.Count; i++)
                {
                    npcs[1].gameObject.layer = 0;
                    npcs[i].gameObject.SetActive(false);
                }
            }
            else if (quest.currentQuestStep == 2)
            {
                pirateQuestObject2.SetActive(false);
            }
        }
    }
}
