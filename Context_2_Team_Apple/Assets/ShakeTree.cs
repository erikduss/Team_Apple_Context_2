using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTree : MonoBehaviour
{
    private bool onTrigger = false;
    [SerializeField] MinigameStates mgStates;
    [SerializeField] PlayerStateMachine playerStates;
    [SerializeField] GameObject showUI;
    [SerializeField] PolygonCollider2D pcol;

    [SerializeField] Animator treeAnim;

    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject birds;

    bool burst = false;

    [SerializeField] float timeToGoBack = 0.8f;
    int press = 0;
    int pressR = 0;

    bool canPlay = false;

    void Start()
    {
        birds.SetActive(false);
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
            pcol.enabled = false;
            canPlay = true;
        }

        if (canPlay && onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

                press++;
                treeAnim.SetTrigger("Shake");
                
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                pressR++;
                treeAnim.SetTrigger("Shake");
            }
        }

        //MINI GAME VOLTOOID
        if (press >= 20 && pressR >= 20)
        {
            
            showUI.SetActive(false);
            Burst();
            canPlay = false;
            birds.SetActive(true);
            StartCoroutine(Destroy());
        }
        
        
        
    }

    public void Burst()
    {
        particles.Play();
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
