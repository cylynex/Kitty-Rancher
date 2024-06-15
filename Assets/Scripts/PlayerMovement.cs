using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float moveSpeed;
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float turnSpeed;

    private void Start() {
        moveSpeed = baseMoveSpeed;
    }

    private void Update() {
        float turnAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -turnAmount);

        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, moveAmount, 0));
    }

}
