using LetsPlayWar.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// The outcome of a single pair of cards played against each other.
    /// </summary>
    /// <remarks>These values are decorated with an attribute for logging purposes.</remarks>
    public enum PlayingCardPairOutcome
    {
        /// <summary>
        /// Not sure what the outcome is.  This is the initialization value.
        /// </summary>
        [EnumDisplayName("???")]
        Unknown = 0,
        /// <summary>
        /// This pair were face down, during a war.
        /// </summary>
        [EnumDisplayName("(...shh...face down)")]
        FaceDown = 1,
        /// <summary>
        /// Player 1 wins the pairing.
        /// </summary>
        [EnumDisplayName("{0} Wins")]
        Player1Wins = 2,
        /// <summary>
        /// Player 2 wins the pairing.
        /// </summary>
        [EnumDisplayName("{1} Wins")]
        Player2Wins = 3,
        /// <summary>
        /// The pairing resulted in a draw.
        /// </summary>
        [EnumDisplayName("WAR!!!")]
        Draw = 4
    }
}
