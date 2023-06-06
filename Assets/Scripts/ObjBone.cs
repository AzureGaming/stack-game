using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBone : MonoBehaviour {
    [HideInInspector]
    public bool isCollision;
    bool isPlaced;

    private void Update() {
        if (isCollision && !isPlaced) {
            isPlaced = true;
            Placed();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Box")) {
            if (collider.GetComponent<ObjBone>() != null && collider.GetComponent<ObjBone>().GetController().isLastPlaced && isValidCollision()) {
                isCollision = true;
            }
        }
    }

    public ObjController GetController() {
        return GetComponentInParent<ObjController>();
    }

    void Placed() {
        GetController().HandleOnPlace();
    }

    bool isValidCollision() {
        return !GetController().isPlaced;
    }
}
