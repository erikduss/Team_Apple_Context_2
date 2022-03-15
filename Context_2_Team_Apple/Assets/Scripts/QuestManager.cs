using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<IQuest> allAvailableQuests = new List<IQuest>();
    private List<IQuest> allCompletedQuests = new List<IQuest>();

    // Start is called before the first frame update
    void Awake()
    {
        allAvailableQuests.Add(new ExampleQuest{ questID = 0 });
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
}
