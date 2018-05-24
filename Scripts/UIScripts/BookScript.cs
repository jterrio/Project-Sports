using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
public class BookScript : NetworkBehaviour {

    public RectTransform bookPanel;
    public GameObject closeInventoryButton, openInventoryButton;

    private bool inInventory;


    void Update() {
        autoCloseInventory();
    }


    void autoCloseInventory() {
        //only check if the inventory is open
        if (inInventory) {
            //get information about mouse position on screen
            if (Input.GetMouseButtonDown(0)) {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);

                pointerData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                //if it hits a layer, check if it is not UI, then close inventory; close in else if not hitting layer
                if (results.Count > 0) {
                    if (results[0].gameObject.layer != LayerMask.NameToLayer("UI")) {
                        MoveInventoryDown();
                        results.Clear();
                    }
                } else {
                    MoveInventoryDown();
                    results.Clear();
                }
            }
        }
    }

    //move inventory up and inverse button activity
    public void MoveInventoryUp() {
        if (!inInventory) {
            Vector3 location = bookPanel.localPosition;
            bookPanel.localPosition = new Vector3(location.x, location.y + 614, location.z);
            openInventoryButton.SetActive(false);
            closeInventoryButton.SetActive(true);
            inInventory = true;
        }
    }

    //move inventory down and inverse button activity
    public void MoveInventoryDown() {
        if (inInventory) {
            Vector3 location = bookPanel.localPosition;
            bookPanel.localPosition = new Vector3(location.x, location.y - 614, location.z);
            openInventoryButton.SetActive(true);
            closeInventoryButton.SetActive(false);
            inInventory = false;
        }
    }

}
