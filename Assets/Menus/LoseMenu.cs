using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : BaseMenu
{
    [Header("Lose Screens")]
    public RawImage background;
    public TextMeshProUGUI text;

    [System.Serializable]
    public struct MenuSettings
    {
        public string text;
        public Texture image;
    }

    public MenuSettings[] loseScreens;

    public new void ToggleMenu(bool visible)
    {
        MenuSettings screen = loseScreens[Random.Range(0, loseScreens.Length)];
        background.texture = screen.image;
        text.text = screen.text;
        base.ToggleMenu(visible);
    }
}
