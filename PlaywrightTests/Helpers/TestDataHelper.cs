using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlaywrightTests.Helpers
{
    public static class TestDataHelper
    {
        public static List<TestData> LoadTestData(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<TestData>>(json);
        }
    }
    public class TestData
    {
        public string Authority { get; set; }
        public string SearchTerm { get; set; }
    }
}