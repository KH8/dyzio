using UnityEngine;

public class TouchCounter : Counter {
    private int _total;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake() {
        base.Awake();
        _total = GameObject.FindObjectsOfType<TouchableController>().Length;
    }

    protected override string DisplayValueText(int counter) {
        return counter.ToString() + "/" + _total;
    }

    protected override string DisplayNewValueText(int value) {
        return "+";
    }
}