using System;
using UnityEngine;

public class Player : MonoBehaviour {
  public static Player Instance { get; private set; }

  public bool IsWalking { get; private set; }
  public event Action<IInteractable> OnInteractableChanged;

  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private LayerMask interactLayerMask;
  [SerializeField] private GameInput gameInput;

  private const float RotateSpeed = 12f;
  private const float InteractionDistance = 1f;
  private IInteractable _interactable;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
    }
  }

  private void Start() {
    gameInput.OnInteractAction += GameInput_OnInteractAction;
  }

  private void GameInput_OnInteractAction() {
    _interactable?.Interact();
  }

  private void Update() {
    // Input
    var inputDir = gameInput.GetMovementVectorNormalized();

    // Movement
    var moveDir = HandleMovement(inputDir);

    // Check for Interactable
    CheckInteractable();

    // Update Walking state
    IsWalking = moveDir.sqrMagnitude > 0;
  }

  private void CheckInteractable() {
    IInteractable changed = null;

    if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, InteractionDistance,
          interactLayerMask) &&
        hitInfo.transform.TryGetComponent<IInteractable>(out var component)) {
      
      // change to a new Interactable
      changed = component;
    }

    if (_interactable != changed) {
      OnInteractableChanged?.Invoke(_interactable = changed);
    }
  }

  private Vector3 HandleMovement(Vector2 inputDir) {
    // Move
    var moveDir = new Vector3(inputDir.x, 0, inputDir.y);
    var lookDir = moveDir.normalized;
    
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

    if (lookDir != Vector3.zero) {
      transform.forward = Vector3.Slerp(transform.forward, lookDir, RotateSpeed * Time.deltaTime);
    }

    return moveDir;
  }

  private bool CheckMove(Vector3 moveDir, float moveDistance) {
    const float playerRadius = 0.7f;
    const float playerHeight = 2f;
    var position = transform.position;

    return Physics.CapsuleCast(position, position + Vector3.up * playerHeight, playerRadius,
      moveDir, moveDistance) == false;
  }
}