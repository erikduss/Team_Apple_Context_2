using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<IQuest> allAvailableQuests = new List<IQuest>();
    private List<IQuest> allCompletedQuests = new List<IQuest>();

    [SerializeField] private List<InteractableNPC> npcs = new List<InteractableNPC>();

    [SerializeField] private GameObject diverQuestObject;
    [SerializeField] private GameObject fisherQuestObject;
    [SerializeField] private GameObject explorerQuestObject;

    [SerializeField] private GameObject pirateQuestObject1;
    [SerializeField] private GameObject pirateQuestObject2;

    [SerializeField] private PushTree deerQuestObject;
    [SerializeField] private ShakeTree crowsQuestObject;
    [SerializeField] private CaptureTiger hermitQuestObject;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        //allAvailableQuests.Add(new ExampleQuest{ questID = 0 });
        allAvailableQuests.Add(new DiverQuest{ questID = 1 });
        allAvailableQuests.Add(new PirateQuest { questID = 2 });
        allAvailableQuests.Add(new ExplorerQuest { questID = 3 });
        allAvailableQuests.Add(new FisherQuest { questID = 4 });
        //allAvailableQuests.Add(new NieuweQuest { questID = 5 });
        allAvailableQuests.Add(new DeerResqueQuest { questID = 6 });
        allAvailableQuests.Add(new CrowsHelpQuest { questID = 7 });
        allAvailableQuests.Add(new HermitQuest { questID = 8 });
        allAvailableQuests.Add(new NomadQuest { questID = 9 });
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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

                    if (activeQuest.questStatus == QuestStatus.COMPLETED)
                    {
                        gameManager.UpdateYear();
                    }
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

                for (int i = 0; i < npcs.Count; i++)
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

        //Fisher quest
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

        //Nieuwe quest
        if (quest.questID == 5)
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

        //Deer quest
        if (quest.questID == 6)
        {
            if (quest.currentQuestStep == 0)
            {
                npcs[3].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
                deerQuestObject.questStarted = true;
            }
            else if (quest.currentQuestStep == 1)
            {
                npcs[3].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if (quest.currentQuestStep == 2)
            {
                npcs[3].ChangeNPCStatus(NPCStatus.NONE);
                npcs[3].gameObject.layer = 0;
            }
        }

        //Crows quest
        if (quest.questID == 7)
        {
            if (quest.currentQuestStep == 0)
            {
                npcs[4].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
                crowsQuestObject.questStarted = true;
            }
            else if (quest.currentQuestStep == 1)
            {
                npcs[4].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if (quest.currentQuestStep == 2)
            {
                npcs[4].ChangeNPCStatus(NPCStatus.NONE);
                npcs[4].gameObject.layer = 0;
            }
        }

        //Hermit quest
        if (quest.questID == 8)
        {
            if (quest.currentQuestStep == 0)
            {
                npcs[5].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
                hermitQuestObject.questStarted = true;
            }
            else if (quest.currentQuestStep == 1)
            {
                npcs[5].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if (quest.currentQuestStep == 2)
            {
                npcs[5].ChangeNPCStatus(NPCStatus.NONE);
                npcs[5].gameObject.layer = 0;
            }
        }

        //Nomad quest
        if (quest.questID == 9)
        {
            if (quest.currentQuestStep == 0)
            {
                npcs[2].ChangeNPCStatus(NPCStatus.HAS_QUEST_RUNNING);
            }
            else if (quest.currentQuestStep == 1)
            {
                npcs[2].ChangeNPCStatus(NPCStatus.HAS_QUEST_COMPLETE);
            }
            else if (quest.currentQuestStep == 2)
            {
                npcs[2].ChangeNPCStatus(NPCStatus.NONE);
                npcs[2].gameObject.layer = 0;
            }
        }
    }
}
