using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    public enum PlayerStatesEnum
    {
        CanMove,
        inCutscene,
        inMap
    };

    public PlayerStatesEnum currentState = PlayerStatesEnum.CanMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerStatesEnum.CanMove:
                player.Movement();
                break;
            case PlayerStatesEnum.inCutscene:
                break;
            case PlayerStatesEnum.inMap:
                break;
        }
    }

    public void InMap()
    {
        currentState = PlayerStatesEnum.inMap;
    }

    public void CanMove()
    {
        currentState = PlayerStatesEnum.CanMove;
    }
}
