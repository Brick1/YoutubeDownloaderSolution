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
        public void ValidateTitleTest()
        {
            string[] titles = {
                "\r\n          Arcane | TEETH [Edit/Tribute]!\r\n        ",
                "\r\n          Unlike Pluto - Revenge, And A Little More\r\n        ",
                "\r\n          twenty one pilots - My Blood (Official Video)\r\n        ",
                "\r\n          VINAI x Le Pedre - I Was Made (Official Lyric Video)\r\n        ",
                "start*sign"

            };

            string[] excepted =
            {
                 "Arcane  TEETH EditTribute!",
                "Unlike Pluto - Revenge, And A Little More",
                "twenty one pilots - My Blood",
                "VINAI x Le Pedre - I Was Made",
                "startsign"
            };

            for(int i = 0; i < titles.Length; i++)
            {
                string title = titles[i];
                string actual = Helper.ValidateTitle(title);
                Assert.That(actual, Is.EqualTo(excepted[i]));
            }
        }
    }
}