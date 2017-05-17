using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    public static Healthbar HPBar { get; private set; }
    private int startingMaxHealth;
    private Slider slider;
    private RectTransform HPRect;
    private float sizePerUnit;
    // Use this for initialization
    void Awake() {
        HPBar = this;
    }
    void Start() {
        slider = GetComponent<Slider>();
        HPRect = GetComponent<RectTransform>();
        PlayerController player = PlayerController.GetPlayerController();
        startingMaxHealth = player.MaxHealth;
        sizePerUnit = HPRect.sizeDelta.x / startingMaxHealth;
        UpdateHealth(startingMaxHealth, startingMaxHealth);
    }

    public void UpdateHealth(int health, int maxHealth) {

        if(health == 0)
            slider.fillRect.GetComponent<Image>().color = Color.red;
        slider.value = health;

        HPRect.sizeDelta = new Vector2(maxHealth * sizePerUnit, HPRect.sizeDelta.y);
    }

}
