using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("shooting game/TitleScreen")]
public class TitleScreen : MonoBehaviour {

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 48;
        GUI.skin.label.alignment = TextAnchor.LowerCenter; //对齐
        GUI.Label(new Rect(0, 20, Screen.width, 100), "SPACE WAR");

        GUI.skin.label.fontSize = 24;
        GUI.Label(new Rect(0, 40, Screen.width, 100), "by Yuxuan Chen");

        if (GUI.Button(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.7f, 200, 30), "START"))
        {
            Application.LoadLevel("level1");
        }
    }
}