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
    public GameObject MenuPanel;
    private const float AXIS_THRESHOLD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(menuIsShowing);
    }

    // Update is called once per frame
    void Update()
    {
        BaseMenuUpdate();
    }


    protected void BaseMenuUpdate()
    {
        if (menuIsShowing)
        {
            // Move cursor if up/down is pressed
            float cursorDirection = Input.GetAxisRaw ("Vertical");

            if (cursorDirection < -AXIS_THRESHOLD && previousAxis >= -AXIS_THRESHOLD)
                menuSelection = (int)Mathf.Repeat(menuSelection + 1, buttons.Length);
            if (cursorDirection > AXIS_THRESHOLD && previousAxis <= AXIS_THRESHOLD)
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

    public void ToggleMenu()
    {
        menuIsShowing = !menuIsShowing;
        menuSelection = 0;
        Time.timeScale = menuIsShowing? 0 : 1; // Pause game updates
        MenuPanel.SetActive(menuIsShowing);
    }

    public void ToggleMenu(bool visible)
    {
        menuIsShowing = visible;
        menuSelection = 0;
        Time.timeScale = menuIsShowing? 0 : 1; // Pause game updates
        MenuPanel.SetActive(menuIsShowing);
    }
}
