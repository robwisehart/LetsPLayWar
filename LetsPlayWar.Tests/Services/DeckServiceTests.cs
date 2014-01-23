using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LetsPlayWar.Services;
using LetsPlayWar.Entities;

namespace LetsPlayWar.Tests.Services
{
    [TestClass]
    public class DeckServiceTests
    {
        /// <summary>
        /// Test loading the deck from an xml file, using the factory.
        /// </summary>
        [TestMethod]
        public void LoadDeckFromXmlFileTest1()
        {
            DeckService service = new DeckService();

            PlayingDeck deck = service.GetDeck();

            Assert.IsNotNull(deck);
            Assert.IsNotNull(deck.UndealtCards);
            Assert.AreEqual<int>(52, deck.UndealtCards.Count);
        }
        /// <summary>
        /// Test shuffling the deck.
        /// </summary>
        [TestMethod]
        public void ShuffleDeckTest1()
        {
            DeckService service = new DeckService();

            PlayingDeck deck = service.GetDeck();

            Assert.IsNotNull(deck);
            Assert.IsNotNull(deck.UndealtCards);
            Assert.AreEqual<int>(52, deck.UndealtCards.Count);

            service.ShuffleDeck(deck);

            Assert.IsNotNull(deck);
            Assert.IsNotNull(deck.UndealtCards);
            Assert.AreEqual<int>(52, deck.UndealtCards.Count);
        }
    }
}
