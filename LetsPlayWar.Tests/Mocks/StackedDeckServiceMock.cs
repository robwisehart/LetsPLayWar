using LetsPlayWar.Contracts;
using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Tests.Mocks
{
    /// <summary>
    /// A mock deck that will result in an ending tie.
    /// </summary>
    /// <remarks>This class implements the <see cref="IDeckService"/> interface.</remarks>
    public class StackedDeckServiceMock : IDeckService
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Stack the deck.
        /// </summary>
        /// <returns>The deck stacked.</returns>
        public PlayingDeck GetDeck()
        {
            PlayingDeck returnValue = new PlayingDeck();

            returnValue.BackImage = String.Empty;
            returnValue.Name = String.Empty;
            returnValue.UndealtCards = new List<PlayingCard>();



            PlayingCard card = new PlayingCard();
            card.ImagePath = String.Empty;
            card.Name = "Ace of Spades";
            card.Value = 14;

            returnValue.UndealtCards.Add(card);

            card = new PlayingCard();
            card.ImagePath = String.Empty;
            card.Name = "Ace of Diamonds";
            card.Value = 14;

            returnValue.UndealtCards.Add(card);

            card = new PlayingCard();
            card.ImagePath = String.Empty;
            card.Name = "King of Hearts";
            card.Value = 13;

            returnValue.UndealtCards.Add(card);


            return returnValue;

        }
        /// <summary>
        /// No shuffling allowed.
        /// </summary>
        /// <param name="deck">The deck to shuffle.</param>
        public void ShuffleDeck(PlayingDeck deck)
        {
            //Do nothing.
        }
        #endregion

        
    }
}
