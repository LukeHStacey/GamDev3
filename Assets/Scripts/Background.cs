using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    private static Background background;
    private PlayerController player;

    void Awake() {
        Background.background = this;
    }

    void Start()
    {
        player = PlayerController.GetPlayerController();
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    void Update() {
        if (player != null) {
            transform.position = player.transform.position * 0.2f;
        }
    }

    public static Background GetBackground() {
        return background;
    }
}
