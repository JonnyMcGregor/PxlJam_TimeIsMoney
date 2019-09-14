using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenu
{
    public GameObject MenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(menuIsShowing);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePauseMenu();
        BaseMenuUpdate();
    }

    public void TogglePauseMenu()
    {
        menuIsShowing = !menuIsShowing;
        menuSelection = 0;
        Time.timeScale = menuIsShowing? 0 : 1; // Pause game updates
        MenuPanel.SetActive(menuIsShowing);
    }

}
