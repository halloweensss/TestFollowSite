using System;

namespace Tests.Helpers
{
    public class TextHelper
    {
        public static string GetRandomWord(int length)
        {
            char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            char[] result = new char[length];
            var r = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = _letters[r.Next(0, _letters.Length-1)];
            }

            return new string(result);
        }
        
        public static string GetRandomWordWithoutNumbers(int length)
        {
            char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] result = new char[length];
            var r = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = _letters[r.Next(0, _letters.Length-1)];
            }

            return new string(result);
        }
    }
}