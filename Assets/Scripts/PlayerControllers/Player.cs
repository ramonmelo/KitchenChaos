using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Controls the Player
/// </summary>
public class Player : MonoBehaviour, IKitchenObjectHolder {
  /// <summary>
  /// The Player instance
  /// </summary>
  public static Player Instance { get; private set; }

  /// <summary>
  /// Returns true if the Player is walking
  /// </summary>
  public bool IsWalking { get; private set; }

  /// <summary>
  /// Event that is invoked when the Interactable changes
  /// </summary>
  public event Action<IInteractable> OnInteractableChanged;

  [SerializeField] private Transform holdingPoint;
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private LayerMask interactLayerMask;
  [SerializeField] private GameInput gameInput;

  private const float RotateSpeed = 12f;
  private const float InteractionDistance = 1f;
  private IInteractable _interactable;
  private KitchenObject _kitchenObject;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
    }
  }

  private void Start() {
    // Subscribe to the Interact Action
    gameInput.OnInteractAction += GameInput_OnInteractAction;
  }

  /// <summary>
  /// Interacts with the current Interactable
  /// </summary>
  private void GameInput_OnInteractAction() {
    _interactable?.Interact(this);
  }

  private void Update() {
    // Get the Movement Vector normalized
    var inputDir = gameInput.GetMovementVectorNormalized();

    // Handle Movement
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

  public Transform GetHoldingPoint() {
    return holdingPoint != null ? holdingPoint : transform;
  }

  public bool IsHoldingKitchenObject() {
    return _kitchenObject != null;
  }

  public void HoldKitchenObject(KitchenObject kitchenObject) {
    // Ignore if there is already a kitchen object
    if (IsHoldingKitchenObject()) { return; }

    // Store the kitchen object
    _kitchenObject = kitchenObject;
  }

  public void DropKitchenObject() {
    _kitchenObject = null;
  }

  public KitchenObject GetKitchenObject() {
    return _kitchenObject;
  }
}