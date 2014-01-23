using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LetsPlayWar.Services.DeckLoaders;
using LetsPlayWar.Entities;

namespace LetsPlayWar.Tests.Services.DeckFactories
{
    [TestClass]
    public class XmlDeckFactoryServiceTests
    {
        /// <summary>
        /// Test loading the deck from an xml file.
        /// </summary>
        [TestMethod]
        public void LoadDeckTest1()
        {
            string fileName = "PlayingCardDeck.xml";

            XmlDeckLoaderService service = new XmlDeckLoaderService();

            PlayingDeck deck = service.LoadDeck(fileName);

            Assert.IsNotNull(deck);
            Assert.IsNotNull(deck.UndealtCards);
            Assert.AreEqual<int>(52, deck.UndealtCards.Count);
        }
    }
}
