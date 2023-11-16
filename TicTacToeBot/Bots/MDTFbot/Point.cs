internal record Point(int X, int Y)
{
    public int ToPlainPoint() => Y * 19 + X;
}