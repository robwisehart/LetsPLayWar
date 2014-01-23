using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// The outcome of a single round of the game war.
    /// </summary>
    public enum GameRoundOutcome
    {
        /// <summary>
        /// The outcoe is unknown.  This is the initialization value.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Player 1 was the winner.
        /// </summary>
        Player1Winner = 1,
        /// <summary>
        /// Player 2 was the winner.
        /// </summary>
        Player2Winner = 2,
        /// <summary>
        /// The round ended in a draw.
        /// </summary>
        Draw = 3
    }
}
