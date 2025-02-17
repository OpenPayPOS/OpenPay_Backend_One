namespace OpenPay.Tests.Api.Helpers;
public static class RandomString
{
    private const string text = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

    public static string Random(int length)
    {
        Random r = new Random();
        string s = "";
        for (int i = 0; i < length; i++)
        {
            int index = r.Next(text.Length);
            s += text[index];
        }

        return s;
    }

    public static string Random(int minLength, int maxLength)
    {
        Random r = new Random();
        return RandomString.Random(r.Next(minLength, maxLength));
    }
}