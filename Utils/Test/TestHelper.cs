using System.Linq;
using System;

namespace store.Utils.Test
{
    public enum RamdomStringType
    {
        LETTER_NUMBER = 1,
        LETTER_NUMBER_LOWER_CASE = 2,
        LETTER_LOWER_CASE = 3,
        LETTER = 4,
        NUMBER = 5,
    }
    public class TestHelper
    {

        public static string randomString(int length, RamdomStringType type)
        {
            string chars = "";
            if (type == RamdomStringType.LETTER)
            {
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            }
            else if (type == RamdomStringType.LETTER_LOWER_CASE)
            {
                chars = "abcdefghijklmnopqrstuvwxyz";
            }
            else if (type == RamdomStringType.LETTER_NUMBER)
            {
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            }
            else if (type == RamdomStringType.LETTER_NUMBER_LOWER_CASE)
            {
                chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            }
            else if (type == RamdomStringType.NUMBER)
            {
                chars = "0123456789";
            }


            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}