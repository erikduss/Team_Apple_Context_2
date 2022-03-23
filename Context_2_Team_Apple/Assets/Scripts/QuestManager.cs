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

    // Start is called before the first frame update
    void Awake()
    {
        allAvailableQuests.Add(new ExampleQuest{ questID = 0 });
        allAvailableQuests.Add(new DiverQuest{ questID = 1 });
        allAvailableQuests.Add(new PirateQuest { questID = 2 });
        allAvailableQuests.Add(new MechanicQuest { questID = 3 });
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
    }
}
