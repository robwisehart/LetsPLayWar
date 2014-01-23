using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Contracts
{
    /// <summary>
    /// The service that manages the game.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Start a new game of war.
        /// </summary>
        /// <param name="player1Name">The name of the first player.</param>
        /// <param name="player2Name">The name of the second player.</param>
        Game StartGame(string player1Name, string player2Name);
        /// <summary>
        /// For the given game, run a round.
        /// </summary>
        /// <param name="g">The game to run a round for.</param>
        /// <returns>The round.  Also updates the status of the game as required.</returns>
        GameRound RunRound(Game g);

    }
}
