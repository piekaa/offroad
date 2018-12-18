using System;

public class StringUtils
{
    public static string ToFirstUpper(string s)
    {
        return Char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string ToFirstLower(string s)
    {
        return Char.ToLower(s[0]) + s.Substring(1);
    }

}