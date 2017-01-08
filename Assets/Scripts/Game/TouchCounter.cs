using UnityEngine;
using UnityEngine.UI;

public class TouchCounter : MonoBehaviour {
  public Text display;
  public int total;

  private int _touches = 0;

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
      display.text = _touches.ToString() + "/" + total;
  }

  public void Increment() {
      _touches++;
  }
}