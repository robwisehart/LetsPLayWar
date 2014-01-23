using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Contracts
{
    /// <summary>
    /// Contract defining a service that loads a deck.  This allows for a factory for different loading types.
    /// </summary>
    public interface IDeckLoaderService
    {
        /// <summary>
        /// Load a deck from whatever source this class is designed for.
        /// </summary>
        /// <param name="data">Any data required to load the deck. i.e. for loading from a file, the file name, from a database the connstring.</param>
        /// <returns>The deck.</returns>
        PlayingDeck LoadDeck(string data);
    }
}
