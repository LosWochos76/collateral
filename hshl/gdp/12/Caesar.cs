public class Caesar
{
    public static char EncryptChar(char c)
    {
        var ascii = Convert.ToInt32(c);
        
        if (ascii >= 65 && ascii <= 90)
            ascii = ascii >= 78 ? ascii - 13 : ascii + 13;

        if (ascii >= 97 && ascii <= 122)
            ascii = ascii >= 110 ? ascii - 13 : ascii + 13;

        return Convert.ToChar(ascii);
    }

    public static void EncryptChars(char[] s)
    {
        for (int i=0; i<s.Length; i++)
            s[i] = EncryptChar(s[i]);
    }

    public static string EncryptString(string s)
    {
        string result = "";
        foreach (char c in s)
            result += EncryptChar(c);
        
        return result;
    }
}