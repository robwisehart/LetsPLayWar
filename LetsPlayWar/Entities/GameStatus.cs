using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// The current status of a game of War.
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// Not really sure where we are.  This is the initialization value.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The game has been created, but not yet started.
        /// </summary>
        NotStarted = 1,
        /// <summary>
        /// The game is currently in progress.
        /// </summary>
        InProgress = 2,
        /// <summary>
        /// The game has concluded, player 1 winning.
        /// </summary>
        Player1Wins = 3,
        /// <summary>
        /// The game has concluded, player 2 winning.
        /// </summary>
        Player2Wins = 4,
        /// <summary>
        /// The game has concluded in a draw.
        /// </summary>
        Draw = 5
    }
}
