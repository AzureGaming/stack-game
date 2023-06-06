using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    Vector2 triggerPos;
    Camera cam;

    private void Awake() {
        cam = FindObjectOfType<Camera>();
    }

    private void OnEnable() {
        ObjController.onUpdateCam += UpdatePos;
    }

    private void OnDisable() {
        ObjController.onUpdateCam -= UpdatePos;
    }

    private void Start() {
        triggerPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
    }

    void UpdatePos(GameObject ins) {
        if (ins.transform.position.y >= triggerPos.y) {
            Vector3 newPos = cam.transform.position;
            newPos.y = ins.transform.position.y + cam.orthographicSize;
            cam.transform.position = newPos;
            triggerPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        }
    }
}
