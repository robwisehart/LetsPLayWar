using LetsPlayWar.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// A single round of the card game War.
    /// </summary>
    public class GameRound
    {
        #region Private Members
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameRound()
        {
            this.Pairs = new List<PlayingCardPair>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the pairs of cards that were played against each other during this round.
        /// </summary>
        /// <remarks>Since a single round can contain multiple pairs played against each other this is a collection.</remarks>
        public List<PlayingCardPair> Pairs
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets/sets the outcome of this round.
        /// </summary>
        public GameRoundOutcome Outcome
        {
            get;
            set;
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Override the ToString() method for outputing to Log.
        /// </summary>
        /// <returns>The round as a string for logging.</returns>
        public override string ToString()
        {
            return this.ToString(false);
        }
        /// <summary>
        /// Override the ToString() method for outputing to Log.
        /// </summary>
        /// <param name="forHtml">Should this be formatted for html?</param>
        /// <returns>The round results as a string.</returns>
        public string ToString(bool forHtml)
        {
            StringBuilder sb = new StringBuilder();

            foreach (PlayingCardPair pair in this.Pairs)
            {
                sb.AppendFormat("{{0}} played the {0} vs {{1}} played the {1} == {2}{3}", pair.Player1Card.Name
                    , pair.Player2Card.Name
                    , EnumUtilities.FetchDisplayName(pair.Status), (forHtml ? "<br />" : Environment.NewLine));
            }

            return sb.ToString();
        }
        #endregion
    }
}
