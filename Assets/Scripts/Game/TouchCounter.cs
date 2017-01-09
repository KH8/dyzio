using UnityEngine;
using UnityEngine.UI;

public class TouchCounter : MonoBehaviour {
  public Text display;

  
  private int _total;
  private int _touches = 0;

  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake() {
      _total = GameObject.FindObjectsOfType<TouchableController>().Length;
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
      display.text = _touches.ToString() + "/" + _total;
  }

  public void Increment() {
      _touches++;
  }
}