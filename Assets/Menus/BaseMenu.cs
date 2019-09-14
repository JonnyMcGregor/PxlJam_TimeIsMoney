using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    public bool menuIsShowing = false;
    public Transform cursor;
    public Button[] buttons;
    public int menuSelection = 0;
    float previousAxis = 0;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    protected void BaseMenuUpdate()
    {
        if (menuIsShowing)
        {
            // Move cursor if up/down is pressed
            float cursorDirection = Input.GetAxisRaw ("Vertical");

            if (cursorDirection < -0.5f && cursorDirection < previousAxis)
                menuSelection = (int)Mathf.Repeat(menuSelection + 1, buttons.Length);
            if (cursorDirection > 0.5f && cursorDirection > previousAxis)
                menuSelection = (int)Mathf.Repeat(menuSelection - 1, buttons.Length);
            previousAxis = cursorDirection;

            // Update Cursor position
            Transform selection = buttons[menuSelection].transform;
            cursor.SetPositionAndRotation(selection.position, selection.rotation);

            // Click Button
            if (Input.GetButtonDown("Submit"))
                buttons[menuSelection].onClick.Invoke();
        }
    }
}
