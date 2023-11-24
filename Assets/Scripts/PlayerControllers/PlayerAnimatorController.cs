using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Player Animator
/// </summary>
public class PlayerAnimatorController : MonoBehaviour {
  
  // Reference to the Player
  [SerializeField] private Player player;
  
  // Reference to the Animator
  private Animator _animator;

  // Hash of the IsWalking parameter
  private static readonly int IsWalking = Animator.StringToHash("IsWalking");

  private void Awake() {
    _animator = GetComponent<Animator>();
  }

  private void Update() {
    // Update the IsWalking parameter
    // based on the Player's IsWalking state
    _animator.SetBool(IsWalking, player.IsWalking);
  }
}