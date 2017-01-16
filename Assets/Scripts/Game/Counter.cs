using UnityEngine;
using UnityEngine.UI;

public abstract class Counter : MonoBehaviour {
  public Text valueDisplay;
  public Text newValueDisplay;
  
  public float pointFactor = 10.0f;
  public int initialFontSize = 360;
  public float decrementAlphaFactor = 0.05f;
  public int decrementSizeFactor = 20;

  private SmoothCounter _counter = new SmoothCounter();

  private bool _animationRunning = false;

  protected abstract string DisplayValueText(int counter);

  protected abstract string DisplayNewValueText(int value);

  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  protected virtual void Awake() {
      newValueDisplay.enabled = false;
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
      valueDisplay.text = DisplayValueText(_counter.GetActual());
      RunNewValueAnimation();
  }

  public void Add(float points) {
      var p = Mathf.FloorToInt(pointFactor * points);
      _counter.Add(p);
      StartNewValueAnimation(p);
      
  }

  private void RunNewValueAnimation() {
      if (_animationRunning) {
        newValueDisplay.fontSize -= decrementSizeFactor;
        DecrementTextAlpha();
        if (newValueDisplay.fontSize < valueDisplay.fontSize) {
            newValueDisplay.enabled = false;
            _animationRunning = false;
        }
      }
  }

  private void StartNewValueAnimation(int points) {
      newValueDisplay.text = DisplayNewValueText(points);
      newValueDisplay.enabled = true;
      newValueDisplay.fontSize = initialFontSize;
      SetTextAlpha(1.0f);

      _animationRunning = true;
  }

  private void DecrementTextAlpha() {
      var alpha = newValueDisplay.color.a - decrementAlphaFactor;
      SetTextAlpha(alpha) ;
  }

  private void SetTextAlpha(float value) {
      var c = newValueDisplay.color;
      c.a = value;
      newValueDisplay.color = c;
  }
}