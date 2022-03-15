using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    private DialogManager dialogManager;
    private int interactable_layer_mask;

    [SerializeField] private Texture2D cursorCanInteractTexture;
    private RaycastHit raycastHit;
    private Ray ray;

    private bool cursorOnDefault = true;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogManager>();
        interactable_layer_mask = LayerMask.GetMask("Clickable");
    }

    // Update is called once per frame
    void Update()
    {
        //Check for mouse click 
        if (!dialogManager.dialogPanelEnabled || dialogManager.currentlyPendingQuest == null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f, interactable_layer_mask))
            {
                if (cursorOnDefault)
                {
                    Cursor.SetCursor(cursorCanInteractTexture, Vector2.zero, CursorMode.Auto);
                    cursorOnDefault = false;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (raycastHit.transform != null)
                    {
                        if (raycastHit.transform.gameObject.tag == "DialogClick")
                        {
                            dialogManager.DisableDialogPanel();
                        }
                        else
                        {
                            CurrentClickedGameObject(raycastHit.transform.gameObject);
                        }
                    }
                }
            }
            else
            {
                if (!cursorOnDefault)
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    cursorOnDefault = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (dialogManager.dialogPanelEnabled)
                    {
                        dialogManager.DisableDialogPanel();
                        return;
                    }
                }
            }
        }
        else
        {
            if (!cursorOnDefault)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                cursorOnDefault = true;
            }
        }
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.GetComponent<IClickable>() != null)
        {
            gameObject.GetComponent<IClickable>().ObjectClicked();
        }
        else
        {
            Debug.Log(gameObject.name);
        }
    }
}
