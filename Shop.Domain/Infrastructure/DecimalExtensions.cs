namespace Shop.Domain.Infrastructure
{
    /// <summary>
    /// Class that changes the decimal value into Rand string
    /// </summary>
    public static class DecimalExtensions
    {
        public static string GetValueString(this decimal value) =>
            $"R {value.ToString("N2")}";
    }
}
