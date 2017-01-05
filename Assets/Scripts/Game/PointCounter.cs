using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour {
  public Text display;
  public float pointFactor = 10000.0f;

  private float _points = 0.0f;

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
      display.text = _points.ToString();
  }

  public void Add(float points) {
      _points += Mathf.Floor(pointFactor * points);
  }
}