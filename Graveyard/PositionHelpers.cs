namespace HDT.Plugins.Graveyard
{
    public static class PositionHelpers
    {
        public static double PixelsToPercentage(this double value, double size) => value * 100 / size;
        public static double PercentageToPixels(this double value, double size) => size * value / 100;
    }
}
