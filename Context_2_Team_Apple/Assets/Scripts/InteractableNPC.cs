using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour
{
    [SerializeField] private NPCStatus currentNpcStatus;

    private bool NPCHasQuest = false;
    [SerializeField] private int questID;
    private IQuest NPCQuest;

    //Icons for indication NPC status
    [SerializeField] private GameObject canTalkIcon;
    [SerializeField] private GameObject hasQuestIcon;
    [SerializeField] private GameObject hasQuestCompleteIcon;
    [SerializeField] private GameObject hasQuestRunningIcon;

    private DialogManager dialogManager;
    private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogManager>();
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();

        ChangeNPCStatus(currentNpcStatus);

        if (NPCHasQuest)
        {
            NPCQuest = questManager.GetQuest(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNPCStatus(NPCStatus newStatus)
    {
        switch (newStatus)
        {
            case NPCStatus.CAN_TALK:
                canTalkIcon.SetActive(true);
                hasQuestIcon.SetActive(false);
                hasQuestCompleteIcon.SetActive(false);
                hasQuestRunningIcon.SetActive(false);
                break;
            case NPCStatus.HAS_QUEST_COMPLETE:
                canTalkIcon.SetActive(false);
                hasQuestIcon.SetActive(false);
                hasQuestCompleteIcon.SetActive(true);
                hasQuestRunningIcon.SetActive(false);

                NPCHasQuest = true;
                break;
            case NPCStatus.HAS_QUEST_RUNNING:
                canTalkIcon.SetActive(false);
                hasQuestIcon.SetActive(false);
                hasQuestCompleteIcon.SetActive(false);
                hasQuestRunningIcon.SetActive(true);

                NPCHasQuest = true;
                break;
            case NPCStatus.HAS_QUEST_START:
                canTalkIcon.SetActive(false);
                hasQuestIcon.SetActive(true);
                hasQuestCompleteIcon.SetActive(false);
                hasQuestRunningIcon.SetActive(false);

                NPCHasQuest = true;
                break;
            case NPCStatus.NONE:
                canTalkIcon.SetActive(false);
                hasQuestIcon.SetActive(false);
                hasQuestCompleteIcon.SetActive(false);
                hasQuestRunningIcon.SetActive(false);
                break;
        }
    }
}
