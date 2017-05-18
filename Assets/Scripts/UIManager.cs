using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;

    public static UIManager Manager { get; private set; }
    private PlayerController player;
    private float time;

    void Awake() {
        Manager = this;
        UpdateTooltip("Press WASD to Move", 5);
    }

    // Use this for initialization
	void Start () {
        player = PlayerController.GetPlayerController();
            guiStyleFore = new GUIStyle();
            guiStyleFore.normal.textColor = Color.black;
            guiStyleFore.alignment = TextAnchor.UpperCenter;
            guiStyleFore.wordWrap = true;
            guiStyleBack = new GUIStyle();
            guiStyleBack.normal.textColor = Color.red;
            guiStyleBack.alignment = TextAnchor.UpperCenter;
            guiStyleBack.fontSize = 36;
            guiStyleBack.wordWrap = true;
	}

    void OnGUI() {
        if (time < Time.time) {
            TooltipText = "";
        }else if(TooltipText != "") {
            int width = 300;
            int height = 60;
            int x = (Screen.width / 2) - width/2 ;
            int y = (Screen.height / 8)* 3 - height/2 ;

            GUI.Label(new Rect(x, y, width, height), TooltipText, guiStyleBack);
            //GUI.Label(new Rect(x+1, y, width, height), TooltipText, guiStyleFore);
        }
    }

    public void UpdateTooltip(String s, float time) {
        TooltipText = s;
        this.time = time + Time.time;
    }

    public string TooltipText { get; set; }
}
