using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    public static class RegexpTesting
    {
        public static void SimpleRegexTest()
        {
            var testString = "JHFVKGFHJFG263456256dfnbsgfdnsfgSFDHSFDHSFDHdsgfhsfdgh234625436sgvnbsfgGSDF";
            var regex = new Regex("^([A-Z]*)");
            var matches = regex.Matches(testString);
        }

        public static void SimpleRegexTest2()
        {
            var testString = "JHFVKGFHJFG263456256dfnbsgfdnsfgSFDHSFDHSFDHdsgfhsfdgh234625436sgvnbsfgGSDF";
            var regex = new Regex("([A-Z]*)([a-z]*)");
            var matches = regex.Matches(testString);
        }

        public static void LookbehindLookaheadTest()
        {
            var testString = "This is my first sentence.";
            var regex = new Regex("(?<=This is)(.*)(?=sentence)");
            var matches = regex.Matches(testString);
        }

        public static void GreedyVSLazyTest()
        {
            var testString = "This is my first sentence. This is my second sentence.";
            var regex = new Regex("(?<=This is)(.*?)(?=sentence)");
            var matches = regex.Matches(testString);
        }

        public static void SelectTextBetveenTagsTest()
        {
            var testString = "<strong>Bold Text</strong><div><div>Some text here</div></div>";
            var regex = new Regex("(?<=<.*>)(.*?)(?=<.*>)");
            var matches = regex.Matches(testString);

            var regex2 = new Regex(@"(?<=\>)(.*?)(?=\<)");
            var matches2 = regex2.Matches(testString);
        }

        public static void TagEntranceTest()
        {
            var testString = "<a href=\"http://some.url.com/link\">Url text</a>";
            //var regex = new Regex("(?<=\\")(.*?)(?=\\)"); Vladimir's
            var regex = new Regex(@"http://[\w./]+"); // Pavel's
            //var regex = new Regex(@"[http|https]+://[\w./]+"); // Pavel's
            //var regex = new Regex(@"http(s?)+://[\w./]+"); // Pavel's
            //var regex = new Regex(@"https?://[\w./]+"); //Stanislav's
            //var regex = new Regex("(?<=href=\\?\")(.*?)(?=\\?\")");

            var matches = regex.Matches(testString);

            var regex2 = new Regex(@"(?<=\>)(.*?)(?=\<)");
            var matches2 = regex2.Matches(testString);
        }

        public static void IpSelectionTest()
        {
            var testString = "some text here 192.168.10.1 some text here too 3413461346";
            //var regex = new Regex(@"([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})");
            //var regex = new Regex(@"(([0-9]{1,3}\.){3}[0-9]{1,3})");
            //var regex = new Regex(@"(?<=\d?)((\d+\.\d)+)(?=\d?)");
            //var regex = new Regex(@"(?<=\d{0,3})((\d{1,3}\.){3}\d{1,3})");
            var regex = new Regex(@"(([0-9]{1,3}\.){3}[0-9]{1,3})");
            var matches = regex.Matches(testString);

            var regex2 = new Regex(@"(?<=\>)(.*?)(?=\<)");
            var matches2 = regex2.Matches(testString);
        }

        public static void ReplacementTest()
        {
            var testString = "dsfghsfgfg34564356adfg4ghfd546dgf3465r436sdrfg53t";
            var regex = new Regex(@"\d+");
            var replaceResult = regex.Replace(testString," ");
        }

        public static void NamedGroupTest()
        {
            var testString = "10/12/2018";
            
            var replaceResult = Regex.Replace(
                testString, 
                @"(?<month>\d{1,2})\/(?<day>\d{1,2})\/(?<year>\d{2,4})", 
                "${day}/${month}/${year}");
        }

        public static void TagEntranceWithNamedGroupsTest()
        {
            var testString = "";
            testString += "<a href='https://google.com/test'>Google</a>";
            testString += "<a href='http://yandex.ru/test'>Yandex</a>";
            testString += "<a href='http://yahoo.com'>Yahoo</a>";

            var regex = new Regex(@"href='(?<url>\S+)'>(?<text>\S+)</a>");

            foreach (Match match in regex.Matches(testString))
            {
                Console.WriteLine($"{match.Groups["text"]}:{match.Groups["url"]}");
            }
        }
    }
}
