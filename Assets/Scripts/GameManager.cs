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
    public List<GameObject> placedObjs;

    [SerializeField]
    float timeout = 5f;
    float timer;
    bool isTimerEnabled;
    bool processedGameOver;

    private void OnEnable() {
        ObjController.onPlace += HandlePlaced;
        Dropper.onDropped += Dropped;
    }

    private void OnDisable() {
        ObjController.onPlace -= HandlePlaced;
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
            ObjController box = currentDropped.GetComponent<ObjController>();
            if (box.isLastPlaced) {
                isTimerEnabled = false;
            }
        } else if (timer <= 0f && isTimerEnabled) {
            isGameOver = true;
        }
    }

    void HandlePlaced(GameObject ins) {
        score++;

        placedObjs.Add(ins);

        ObjController objToPlace = ins.GetComponent<ObjController>();
        canDrop = true;

        placedObjs.ForEach(obj => {
            bool isObjLastPlaced = obj == ins;
            obj.GetComponent<ObjController>().isLastPlaced = isObjLastPlaced;
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
        placedObjs = new List<GameObject>();
    }
}
