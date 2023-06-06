using System.Linq;
using UnityEngine;

public class ObjController : MonoBehaviour {
    public delegate void Place(GameObject ins);
    public static Place onPlace;
    public delegate void UpdateCam(GameObject ins);
    public static UpdateCam onUpdateCam;

    public bool isLastPlaced;
    public bool isPlaced;

    [SerializeField]
    GameObject rootObj;
    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update() {
        SetColor();
    }

    void SetColor() {
        if (isLastPlaced) {
            spriteRenderer.color = Color.green;
        } else if (isPlaced) {
            spriteRenderer.color = Color.grey;
        }
    }

    public void HandleOnPlace() {
        isLastPlaced = true;
        isPlaced = true;

        onPlace?.Invoke(gameObject);
        onUpdateCam?.Invoke(rootObj);

        FreezeBones();
    }

    void FreezeBones() {
        Rigidbody2D[] rbs = GetComponentsInChildren<Rigidbody2D>();
        rbs.ToList().ForEach(rb => {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        });
    }
}
