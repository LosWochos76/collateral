public class Caesar
{
    public static char Encrypt(char c)
    {
        var ascii = Convert.ToInt32(c);
        
        if (ascii >= 65 && ascii <= 90)
            ascii = ascii >= 78 ? ascii - 13 : ascii + 13;

        if (ascii >= 97 && ascii <= 122)
            ascii = ascii >= 110 ? ascii - 13 : ascii + 13;

        return Convert.ToChar(ascii);
    }

    public static string Encrypt(string s)
    {
        string result = "";
        for (int i=0; i<s.Length; i++)
            result += Encrypt(s[i]);
        
        return result;
    }
}