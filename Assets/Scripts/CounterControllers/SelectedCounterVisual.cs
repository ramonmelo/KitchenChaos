using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
  [SerializeField] private GameObject visuals;

  public void Activate() {
    visuals.SetActive(true);
  }

  public void Deactivate() {
    visuals.SetActive(false);
  }
}