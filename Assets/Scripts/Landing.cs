using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Landing : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Box") && FindObjectOfType<GameManager>().placedObjs.Count == 0) {
            collider.GetComponent<ObjBone>().isCollision = true;
        }
    }
}
