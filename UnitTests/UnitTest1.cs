using YoutubeDownloader.Youtube.Models;

namespace UnitTests
{
    public class Tests
    {

        YoutubeManager manager;
        [SetUp]
        public void Setup()
        {
            manager = new YoutubeManager();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}