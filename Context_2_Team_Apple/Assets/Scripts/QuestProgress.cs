using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgress : MonoBehaviour, IClickable
{
    private QuestManager questManager;
    private DialogManager dialogManager;

    //TODO: Discuss if this needs to be a list instead to allow an object or npc to have multiple quests (steps) attached
    [Header("Script Info:")]
    [Header("1. Make sure this script is on the layer: 'Clickable'")]
    [Space(10)]

    [Header("ID of the quest that will gain progress.")]
    [SerializeField] private List<int> questID;

    [Space(5)]

    [Header("ID of the quest step that will be completed.")]
    [SerializeField] private List<int> questStepID;

    // Start is called before the first frame update
    void Start()
    {
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
        dialogManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogManager>();
    }

    public void ObjectClicked()
    {
        if(questID == null || questStepID == null)
        {
            Debug.Log("Quest object not linked to any quest");
            return;
        }

        List<IQuest> displayableQuests = new List<IQuest>();

        for(int i=0; i < questID.Count; i++)
        {
            if (questManager.CanProgressQuestStep(questID[i], questStepID[i]))
            {
                displayableQuests.Add(questManager.GetQuest(questID[i]));
            }
        }

        if(displayableQuests.Count > 0)
        {
            //If the dialog panel needs to enable the accept/decline buttons the functions needs a quest parameter
            if(displayableQuests[0].currentQuestStep == 0) //This is a quest start
            {
                string temporaryTitle = displayableQuests[0].GetCurrentTitle();
                string temporaryDialog = displayableQuests[0].GetCurrentDialog();

                dialogManager.EnableDialogPanel(true, displayableQuests[0], temporaryTitle, temporaryDialog);
            }
            else //This is not a quest start, dialog needs to be shown only.
            {
                string temporaryTitle = displayableQuests[0].GetCurrentTitle();
                string temporaryDialog = displayableQuests[0].GetCurrentDialog();

                dialogManager.EnableDialogPanel(false, null, temporaryTitle, temporaryDialog);

                questManager.CompleteQuestStep(displayableQuests[0].questID, displayableQuests[0].currentQuestStep);
            }
        }
    }
}
