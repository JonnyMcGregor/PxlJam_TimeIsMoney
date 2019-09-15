using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : BaseMenu
{
    public Texture StarEmpty;
    public Texture StarFull;
    public RawImage[] stars;

    public void SetStarRating(int score)
    {
        for (int i = 0; i < stars.Length; ++i)
        {
            stars[i].texture = i < score? StarFull : StarEmpty;
        }
    }

    public void NextLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        int id = int.Parse(name.Substring(name.Length-1, 1)) + 1;
        if (id > 5) id = 1;
        name = name.Substring(0, name.Length - 1);
        SceneManager.LoadScene(name + id, LoadSceneMode.Single);
    }
}
