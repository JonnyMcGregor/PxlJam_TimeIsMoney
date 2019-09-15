using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public string[] scenes;
    public Button Template;
    public Transform ButtonGroup;

    void Start()
    {
        Template.gameObject.SetActive(false);
        buttons = new Button[scenes.Length];
        for (int i = 0; i < scenes.Length; ++i)
        {
            Button btn = GameObject.Instantiate(Template, ButtonGroup);
            int index = i;
            btn.onClick.AddListener(delegate{LoadLevel(index);});
            btn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i + 1);
            btn.gameObject.SetActive(true);
            buttons[i] = btn; 
        } 
    }

    public void LoadLevel(int levelID)
    {
        SceneManager.LoadScene(scenes[levelID], LoadSceneMode.Single);
    }
}
