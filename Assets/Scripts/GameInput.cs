using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

  public Vector2 GetMovementVectorNormalized() {

    // Input
    var inputDir = Vector2.zero;

    if (Input.GetKey(KeyCode.W)) { inputDir.y = 1; }
    if (Input.GetKey(KeyCode.S)) { inputDir.y = -1; }
    if (Input.GetKey(KeyCode.A)) { inputDir.x = -1; }
    if (Input.GetKey(KeyCode.D)) { inputDir.x = 1; }

    return inputDir.normalized;
  }
}
