using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour {

    [SerializeField] bool isMoving = false;
    [SerializeField] float moveSpeed;
    [SerializeField] float currentDirection;

    [SerializeField] float moveTimer = 0;
    [SerializeField] float moveTimerMax = 5f;

    void Start () {
        moveTimer = moveTimerMax;
    }

    void Update () {
        // If already moving, keep moving
        if (isMoving) {
            float moveAmount = moveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, moveAmount, 0));
            moveTimer -= Time.deltaTime;

            if (moveTimer <= 0) {
                moveTimer = moveTimerMax;
                PickDirection();
            }

        }

        // Else pick new direction and start moving
        else {
            PickDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        print("kitty hit wall");
        isMoving = false;
        PickDirection();
    }


    void OnTriggerEnter2D(Collider2D other) {
        PickDirection();
    }

    void PickDirection() {
        float direction = Random.Range(0, 360);
        currentDirection = direction;
        transform.Rotate(0, 0, currentDirection);
        isMoving = true;
    }

}
