public class SmoothCounter {
    private int _actPoint = 0;
    private int _setPoint = 0;
    private int _incrementValue = 10;

    public void Add(int value) {
        _setPoint += value;
    }

    public int GetActual() {
        if (_setPoint - _actPoint > _incrementValue) {
            _actPoint += _incrementValue;
        } else {
            _actPoint = _setPoint;
        }
        return _actPoint;
    }
}