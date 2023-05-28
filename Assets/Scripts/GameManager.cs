using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public static GameObject currentDropped;
    public static bool isGameOver;

    public bool canDrop;
    public int score = 0;

    [SerializeField]
    float timeout = 5f;
    float timer;
    bool isTimerEnabled;
    bool processedGameOver;

    private void OnEnable() {
        Box.onPlace += UpdateLatestPlacement;
        Dropper.onDropped += Dropped;
    }

    private void OnDisable() {
        Box.onPlace -= UpdateLatestPlacement;
        Dropper.onDropped -= Dropped;
    }

    private void Awake() {
        Initialize();
    }

    private void Update() {
        if (isGameOver && !processedGameOver) {
            isGameOver = false;
            processedGameOver = true;
            GameOver();
        }

        if (timer > 0f && isTimerEnabled) {
            timer -= Time.deltaTime;
            Box box = currentDropped.GetComponent<Box>();
            if (box.isLastPlaced) {
                isTimerEnabled = false;
            }
        } else if (timer <= 0f && isTimerEnabled) {
            isGameOver = true;
        }
    }

    void UpdateLatestPlacement(GameObject ins) {
        score++;

        Box[] boxes = FindObjectsOfType<Box>();
        Box boxToPlace = ins.GetComponent<Box>();
        canDrop = true;

        boxes.ToList().ForEach(box => {
            box.isLastPlaced = box == boxToPlace;
        });
    }

    void Dropped(GameObject ins) {
        canDrop = false;
        currentDropped = ins;
        timer = timeout;
        isTimerEnabled = true;
    }

    void GameOver() {
        Initialize();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Initialize() {
        canDrop = true;
        timer = default;
        isTimerEnabled = false;
        processedGameOver = false;
        score = 0;
    }
}
