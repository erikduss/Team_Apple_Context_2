using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStates : MonoBehaviour
{
    [SerializeField] GameObject wOnly;
    [SerializeField] GameObject upDown;
    [SerializeField] GameObject leftRight;
    public enum minigameState
    {
        nothing,
        leftToRight,
        wOnly,
        upToDown
    };

    public minigameState currentState = minigameState.nothing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case minigameState.nothing:
                leftRight.SetActive(false);
                wOnly.SetActive(false);
                upDown.SetActive(false);
                break;
            case minigameState.leftToRight:
                leftRight.SetActive(true);
                wOnly.SetActive(false);
                upDown.SetActive(false);
                break;
            case minigameState.wOnly:
                leftRight.SetActive(false);
                wOnly.SetActive(true);
                upDown.SetActive(false);
                break;
            case minigameState.upToDown:
                leftRight.SetActive(false);
                wOnly.SetActive(false);
                upDown.SetActive(true);
                break;
        }
    }

}
