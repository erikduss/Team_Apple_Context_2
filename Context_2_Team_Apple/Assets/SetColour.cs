using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColour : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    
    [SerializeField] Color currentLocation;
    [SerializeField] Color notSelected;
    [SerializeField] Color trade;


    public void CurrentlySelected()
    {
        spriteRenderer.color = currentLocation;
    }

    public void NotSelected()
    {
        spriteRenderer.color = notSelected;
    }

    public void TradeIcon()
    {
        spriteRenderer.color = trade;
    }
}
