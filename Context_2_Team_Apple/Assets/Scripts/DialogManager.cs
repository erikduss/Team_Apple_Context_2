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

    private IQuest currentlyPendingQuest;

    // Start is called before the first frame update
    void Start()
    {
        DisableDialogPanel();
    }

    public void EnableDialogPanel(bool hasQuestAttached, IQuest attachedQuest, string newDialogTitleText, string newDialogText)
    {
        dialogPanelEnabled = true;
        mainDialogPanel.SetActive(true);
        questButtons.SetActive(false);

        if (hasQuestAttached)
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
            //accept quest
        }

        DisableDialogPanel();
    }

    public void DeclineNewQuest()
    {
        DisableDialogPanel();
    }
}
