using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour {

  public bool IsWalking { get; private set; }

  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private LayerMask InteractLayerMask;
  [SerializeField] private GameInput gameInput;

  private readonly float rotateSpeed = 12f;
  private readonly float interactionDistance = 2f;

  private void Start() {
    gameInput.OnInteractAction += GameInput_OnInteractAction;
  }

  private void GameInput_OnInteractAction(object sender, EventArgs e) {

    if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out var hitInfo, interactionDistance, InteractLayerMask)) {

      if (hitInfo.transform.TryGetComponent<ClearCounter>(out var component)) {

        component.Interact();
      }
    }
  }

  private void Update() {

    // Input
    var inputDir = gameInput.GetMovementVectorNormalized();

    // Movement
    var moveDir = HandleMovement(inputDir);

    // Update Walking state
    IsWalking = moveDir.sqrMagnitude > 0;
  }

  private Vector3 HandleMovement(Vector2 inputDir) {
    // Move
    var moveDir = new Vector3(inputDir.x, 0, inputDir.y);

    // Collision Check
    var moveDistance = moveSpeed * Time.deltaTime;
    var canMove = CheckMove(moveDir, moveDistance);

    if (canMove == false) {
      var moveDirX = new Vector3(inputDir.x, 0, 0);
      canMove = CheckMove(moveDirX, moveDistance);

      if (canMove) {
        moveDir = moveDirX;
      }
    }

    if (canMove == false) {
      var moveDirZ = new Vector3(0, 0, inputDir.y);
      canMove = CheckMove(moveDirZ, moveDistance);

      if (canMove) {
        moveDir = moveDirZ;
      }
    }

    moveDir = moveDir.normalized;

    if (canMove) {
      transform.position += moveDir * moveDistance;
    } else {
      moveDir = Vector3.zero;
    }

    transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

    return moveDir;
  }

  private bool CheckMove(Vector3 moveDir, float moveDistance) {
    float playerRadius = 0.7f;
    float playerHeight = 2f;
    return Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance) == false;
  }
}
