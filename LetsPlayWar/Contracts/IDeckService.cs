using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Contracts
{
    /// <summary>
    /// The contract for the service that manages loading and shuffling the deck.
    /// </summary>
    public interface IDeckService
    {
        /// <summary>
        /// Load a deck from somewhere.
        /// </summary>
        /// <returns>A fully loaded deck of cards.</returns>
        PlayingDeck GetDeck();
        /// <summary>
        /// Shuffle the deck randomly.
        /// </summary>
        /// <param name="deck">The deck to shuffle.</param>
        void ShuffleDeck(PlayingDeck deck);
    }
}
