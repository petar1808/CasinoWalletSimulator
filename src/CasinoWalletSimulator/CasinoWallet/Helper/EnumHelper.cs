namespace CasinoWallet.Helper
{
    public static class EnumHelper
    {
        public static bool TryParseNonNumeric<TEnum>(string input, bool ignoreCase, out TEnum result)
            where TEnum : struct, Enum
        {
            result = default;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Reject purely numeric strings
            if (input.All(char.IsDigit))
                return false;

            return Enum.TryParse(input, ignoreCase, out result);
        }
    }

}
