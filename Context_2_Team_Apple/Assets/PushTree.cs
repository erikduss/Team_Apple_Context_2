using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTree : MonoBehaviour
{
    private bool onTrigger = false;
    [SerializeField] MinigameStates mgStates;
    [SerializeField] PlayerStateMachine playerStates;
    [SerializeField] GameObject showUI;

    [SerializeField] Animator treeAnim;
    [SerializeField] Animator deerAnim;
    [SerializeField] Collider2D treeCol;

    QuestManager questManager;

    bool canPlay = false;

    public bool questStarted = false;
    private bool completedStep = false;

    [SerializeField]float timeToGoBack = 0.8f;
    int press = 0;
    
    
    void Start()
    {
        showUI.SetActive(false);
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //als player op de trigger staat en E drukt gaat de speler in de minigame state
        if (onTrigger && Input.GetKeyDown(KeyCode.E))
        {
            playerStates.currentState = PlayerStateMachine.PlayerStatesEnum.inMiniGame;
            mgStates.currentState = MinigameStates.minigameState.wOnly;
            showUI.SetActive(false);
            treeCol.enabled = false;
            canPlay = true;
        }

        if(canPlay && onTrigger && Input.GetKeyDown(KeyCode.W))
        {
            
                
                press++;
                treeAnim.SetTrigger("Shake");
            
        }

        //MINI GAME VOLTOOID
        if(press >= 20 && !completedStep)
        {
            treeAnim.SetTrigger("Move");
            showUI.SetActive(false);
            questManager.CompleteQuestStep(6, 1);
            deerAnim.SetTrigger("Move");
            canPlay = false;

            completedStep = true;

            StartCoroutine(Destroy());
        }

        Debug.Log(press);
    }

   IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToGoBack);
        mgStates.currentState = MinigameStates.minigameState.nothing;
        playerStates.currentState = PlayerStateMachine.PlayerStatesEnum.CanMove;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (questStarted)
        {
            onTrigger = true;
            showUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTrigger = false;
        showUI.SetActive(false);
    }
}
