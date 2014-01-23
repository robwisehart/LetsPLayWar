using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LetsPlayWar.Services;
using LetsPlayWar.Entities;
using LetsPlayWar.Tests.Mocks;
using LetsPlayWar.Contracts;

namespace LetsPlayWar.Tests.Services
{
    [TestClass]
    public class GameServiceTests
    {
        /// <summary>
        /// Test starting a new game with a deck in xml format.
        /// </summary>
        [TestMethod]
        public void XmlDeckStartGameTest1()
        {
            string p1 = "Fred";
            string p2 = "Barney";

            GameService service = new GameService();
            Game g = service.StartGame(p1, p2);

            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Player1);
            Assert.AreEqual<string>(p1, g.Player1.Name);
            Assert.AreEqual<string>(p2, g.Player2.Name);
            Assert.AreEqual<int>(26, g.Player1.CardsRemaining);
            Assert.AreEqual<int>(26, g.Player2.CardsRemaining);
            Assert.IsNotNull(g.Deck);
            Assert.AreEqual<int>(0, g.Deck.UndealtCards.Count);
            Assert.AreEqual<GameStatus>(GameStatus.InProgress, g.Status);
        }
        /// <summary>
        /// Test running a game.
        /// </summary>
        [TestMethod]
        public void RunRoundTest1()
        {
            //ASSERT:  XmlDecStartGameTest Succeeeds.
            string p1 = "Fred";
            string p2 = "Barney";

            GameService service = new GameService();
            Game g = service.StartGame(p1, p2);

            Assert.IsNotNull(g);
            Assert.IsNotNull(g.Player1);
            Assert.AreEqual<string>(p1, g.Player1.Name);
            Assert.AreEqual<string>(p2, g.Player2.Name);
            Assert.AreEqual<int>(26, g.Player1.CardsRemaining);
            Assert.AreEqual<int>(26, g.Player2.CardsRemaining);
            Assert.IsNotNull(g.Deck);
            Assert.AreEqual<int>(0, g.Deck.UndealtCards.Count);
            Assert.AreEqual<GameStatus>(GameStatus.InProgress, g.Status);

            GameRound round = null;

            //Now the test.
            while (g.Status == GameStatus.InProgress)
            {
                round = service.RunRound(g);
            }

            Assert.AreNotEqual<GameStatus>(GameStatus.InProgress, g.Status);
            Assert.IsNotNull(round);
            Assert.AreNotEqual<int>(g.Player1.CardsRemaining, g.Player2.CardsRemaining);
            Assert.IsTrue(
                   (g.Status == GameStatus.Player1Wins && g.Player2.CardsRemaining == 0)
                || (g.Status == GameStatus.Player2Wins && g.Player1.CardsRemaining == 0)
                || (g.Status == GameStatus.Draw)
                );
        }
        /// <summary>
        /// Test running a game with the stacked deck.
        /// </summary>
        [TestMethod]
        public void CardUnevenGameTest1()
        {
            string p1 = "Fred";
            string p2 = "Barney";

            IDeckService deckService = new StackedDeckServiceMock();

            GameService service = new GameService(deckService);
            Game g = service.StartGame(p1, p2);

            GameRound round = null;

            //Now the test.
            while (g.Status == GameStatus.InProgress)
            {
                round = service.RunRound(g);
            }

            Assert.AreNotEqual<GameStatus>(GameStatus.InProgress, g.Status);
            Assert.IsNotNull(round);
            Assert.AreNotEqual<int>(g.Player1.CardsRemaining, g.Player2.CardsRemaining);
            Assert.IsTrue(
                   (g.Status == GameStatus.Player1Wins && g.Player2.CardsRemaining == 0)
                || (g.Status == GameStatus.Player2Wins && g.Player1.CardsRemaining == 0)
                || (g.Status == GameStatus.Draw)
                );
        }
    }
}
