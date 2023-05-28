using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour {
    public delegate void Dropped(GameObject ins);
    public static Dropped onDropped;

    [SerializeField]
    GameObject objToDrop;

    GameManager manager;
    Camera cam;
    float rightBoundX;
    float leftBoundX;
    bool isMovingLeft;

    private void Awake() {
        cam = FindObjectOfType<Camera>();
        manager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        rightBoundX = cam.ViewportToWorldPoint(new Vector3(1, 0)).x;
        leftBoundX = cam.ViewportToWorldPoint(new Vector3(0, 0)).x;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Drop();
        }

        Move();
    }

    void Move() {
        Vector2 newPos = transform.position;
        if (isMovingLeft) {
            if (transform.position.x > leftBoundX) {
                newPos.x -= 0.01f;
            } else {
                isMovingLeft = false;
            }
        }

        if (!isMovingLeft) {
            if (transform.position.x < rightBoundX) {
                newPos.x += 0.01f;
            } else {
                isMovingLeft = true;
            }
        }

        transform.position = newPos;
    }

    void Drop() {
        if (manager.canDrop) {
            GameObject ins = Instantiate(objToDrop, transform.position, Quaternion.identity);
            onDropped?.Invoke(ins);
        }
    }
}
