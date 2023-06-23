using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

  public bool IsWalking { get; private set; }

  [SerializeField]
  private float moveSpeed = 5f;
  private float rotateSpeed = 12f;

  private void Update() {

    // Input
    var inputDir = Vector2.zero;

    if (Input.GetKey(KeyCode.W)) { inputDir.y = 1; }
    if (Input.GetKey(KeyCode.S)) { inputDir.y = -1; }
    if (Input.GetKey(KeyCode.A)) { inputDir.x = -1; }
    if (Input.GetKey(KeyCode.D)) { inputDir.x = 1; }

    inputDir = inputDir.normalized;

    // Move
    var moveDir = new Vector3(inputDir.x, 0, inputDir.y);
    transform.position += moveDir * moveSpeed * Time.deltaTime;
    transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

    // State
    IsWalking = moveDir.sqrMagnitude > 0;
  }
}
