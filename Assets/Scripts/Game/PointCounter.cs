public class PointCounter : Counter {
    protected override string DisplayValueText(int counter) {
        return counter.ToString();
    }

    protected override string DisplayNewValueText(int value) {
        return "+" + value;
    }
}