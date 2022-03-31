using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTiger : MonoBehaviour
{
    private bool onTrigger = false;
    [SerializeField] MinigameStates mgStates;
    [SerializeField] PlayerStateMachine playerStates;
    [SerializeField] GameObject showUI;

    [SerializeField] GameObject dustCloud;

    [SerializeField] PolygonCollider2D tigerCol;
    [SerializeField] GameObject tigerSprite;
    [SerializeField] PolygonCollider2D tigerTrigger;
    [SerializeField] GameObject tigerObj;

    QuestManager questManager;

    [SerializeField] float timeToGoBack = 0.8f;
    int press = 0;
    int pressR = 0;

    bool canPlay = false;

  

    void Start()
    {
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
        dustCloud.SetActive(false);
        tigerObj.SetActive(false);
        showUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //als player op de trigger staat en E drukt gaat de speler in de minigame state
        if (onTrigger && Input.GetKeyDown(KeyCode.E))
        {
            playerStates.currentState = PlayerStateMachine.PlayerStatesEnum.inMiniGame;
            mgStates.currentState = MinigameStates.minigameState.leftToRight;
            showUI.SetActive(false);
            tigerCol.enabled = false;
            dustCloud.SetActive(true);
            canPlay = true;
        }

        if (canPlay && onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

                press++;
                
            }

            if (Input.GetKeyDown(KeyCode.D))
            {

                pressR++;
                
            }
        }
        //MINI GAME VOLTOOID
        if (press >= 20 && pressR >= 20)
        {
            showUI.SetActive(false);
            //questManager.CompleteQuestStep(questID, QuestStepID);
            dustCloud.SetActive(false);
            tigerSprite.SetActive(false);
            tigerTrigger.enabled = false;
            tigerObj.SetActive(true);
            canPlay = false;
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
        onTrigger = true;
        
        showUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTrigger = false;
        showUI.SetActive(false);
    }
}
