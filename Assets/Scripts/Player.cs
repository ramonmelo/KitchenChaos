using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

  public bool IsWalking { get; private set; }

  [SerializeField]
  private float moveSpeed = 5f;
  private float rotateSpeed = 12f;

  [SerializeField] private GameInput gameInput;

  private void Update() {

    var inputDir = gameInput.GetMovementVectorNormalized();

    // Move
    var moveDir = new Vector3(inputDir.x, 0, inputDir.y);
    transform.position += moveDir * moveSpeed * Time.deltaTime;
    transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

    // State
    IsWalking = moveDir.sqrMagnitude > 0;
  }
}
