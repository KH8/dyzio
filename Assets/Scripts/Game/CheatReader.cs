public class CheatReader {
    private string _cheat;
    private int _index = 0;

    public CheatReader(string cheat) {
        this._cheat = cheat;
    }

    public void NextChar(char c) {
        if (!IsValid() && _cheat[_index] == c) {
            _index++;
        } else {
            _index = 0;
        }
    }

    public bool IsValid() {
        return _index >= _cheat.Length;
    }
}