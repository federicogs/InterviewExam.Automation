using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Test.Federico.Utils
{
    static class UtilHelper
    {
        public static string CleanUpString(string inputstring) {

            List<String> lStringsToRemove = new List<string>() { };

            foreach (var item in lStringsToRemove)
            {
                inputstring.Replace(item, String.Empty);
            }

            List<Char> lCharsToRemove = new List<Char>() { '\t', '\r', '\n', };

            foreach (var item in lCharsToRemove)
            {
                inputstring = RemoveCharFromString(inputstring, item);
            }

            return inputstring;

        }

        public static string RemoveCharFromString(string input, char charItem)
        {
            int indexOfChar = input.IndexOf(charItem);
            if (indexOfChar < 0)
            {
                return input;
            }
            return RemoveCharFromString(input.Remove(indexOfChar, 1), charItem);
        }

        public static void SetupDriverWindow(ref OpenQA.Selenium.IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

    }
}
