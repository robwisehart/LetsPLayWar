using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// A single pair of cards played against each other.  1 or more of these pairs makes up a round.
    /// </summary>
    public class PlayingCardPair
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the card that Player 1 played.
        /// </summary>
        public PlayingCard Player1Card
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the Card that Player 2 played.
        /// </summary>
        public PlayingCard Player2Card
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the outcome status of this pair.
        /// </summary>
        public PlayingCardPairOutcome Status
        {
            get;
            set;
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Public Methods
        #endregion
    }
}
