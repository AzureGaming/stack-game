using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    public delegate void Place(GameObject ins);
    public static Place onPlace;

    public bool isLastPlaced;
    [HideInInspector]
    public bool isCollision;
    bool isPlaced;

    private void Update() {
        if (isCollision && !isPlaced) {
            isPlaced = true;
            Placed();
        }
        SetColor();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Box")) {
            if (collider.GetComponent<Box>() != null && collider.GetComponent<Box>().isLastPlaced) {
                isCollision = true;
            }
        }
    }

    void Placed() {
        isLastPlaced = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        onPlace?.Invoke(gameObject);
    }

    void SetColor() {
        if (isLastPlaced) {
            GetComponent<SpriteRenderer>().color = Color.green;
        } else if (isPlaced) {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }
}
