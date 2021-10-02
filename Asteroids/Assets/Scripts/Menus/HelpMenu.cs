using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public void Back()
    {
        AudioManager.Play(AudioName.Button);

        MenuManager.GoTo(MenuName.Main);
    }
}