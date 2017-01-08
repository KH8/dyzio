using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour {
  public Text display;
  public float pointFactor = 10.0f;

  private SmoothCounter _counter = new SmoothCounter();

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
      display.text = _counter.GetActual().ToString();
  }

  public void Add(float points) {
      var p = Mathf.FloorToInt(pointFactor * points);
      _counter.Add(p);
  }
}