using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject mainDialogPanel;
    [SerializeField] private GameObject questButtons;

    [SerializeField] private TextMeshProUGUI dialogTitleText;
    [SerializeField] private TextMeshProUGUI dialogText;

    public bool dialogPanelEnabled = false;

    public IQuest currentlyPendingQuest;

    private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
        DisableDialogPanel();
    }

    public void EnableDialogPanel(bool isTheQuestStart, IQuest attachedQuest, string newDialogTitleText, string newDialogText)
    {
        dialogPanelEnabled = true;
        mainDialogPanel.SetActive(true);
        questButtons.SetActive(false);

        if (isTheQuestStart)
        {
            questButtons.SetActive(true);
            currentlyPendingQuest = attachedQuest;
        }

        if (newDialogText != null && newDialogTitleText != null)
        {
            dialogTitleText.text = newDialogTitleText;
            dialogText.text = newDialogText;
        }
        
    }

    public void DisableDialogPanel()
    {
        dialogPanelEnabled = false;
        mainDialogPanel.SetActive(false);
        currentlyPendingQuest = null;
    }

    public void AcceptNewQuest()
    {
        Debug.Log("Accepted");

        if(currentlyPendingQuest != null)
        {
            questManager.AcceptQuest(currentlyPendingQuest.questID, currentlyPendingQuest.currentQuestStep);
        }

        DisableDialogPanel();
    }

    public void DeclineNewQuest()
    {
        DisableDialogPanel();
    }
}
