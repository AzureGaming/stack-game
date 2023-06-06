using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour {
    GameManager gameManager;
    TMP_Text text;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        text = GetComponent<TMP_Text>();
    }

    void Update() {
        text.text = $"Score: {gameManager.score}";
    }
}
