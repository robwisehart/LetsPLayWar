using LetsPlayWar.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// A single game of War.
    /// </summary>
    public class Game
    {
        #region Private Members
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a new game object.
        /// </summary>
        /// <param name="player1">The first player.</param>
        /// <param name="player2">The second player.</param>
        /// <param name="deck">The deck to play with.</param>
        /// <param name="pathToLogFile">The path to the log file for this game.</param>
        public Game(GamePlayer player1, GamePlayer player2, PlayingDeck deck, string pathToLogFile)
        {
            #region Param Checking
            if (player1 == null)
            {
                throw new ArgumentNullException("player1");
            }
            if (player2 == null)
            {
                throw new ArgumentNullException("player2");
            }
            if (deck == null)
            {
                throw new ArgumentNullException("deck");
            }

            if (String.IsNullOrWhiteSpace(pathToLogFile))
            {
                throw new ArgumentNullException(pathToLogFile);
            }
            try
            {
                Path.GetFullPath(pathToLogFile);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The path to the log file is not valid.  See inner exception for details.", ex);
            }
            #endregion

            this.Player1 = player1;
            this.Player2 = player2;
            this.Deck = deck;
            this.Status = GameStatus.NotStarted;
            this.PathToGameLog = pathToLogFile;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets Player 1.
        /// </summary>
        public GamePlayer Player1
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets Player 2.
        /// </summary>
        public GamePlayer Player2
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the deck in use.
        /// </summary>
        public PlayingDeck Deck
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets/sets the status of the game.
        /// </summary>
        public GameStatus Status
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the path to the game's log file.
        /// </summary>
        public string PathToGameLog
        {
            get;
            protected set;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the status of the game as a formatted string.
        /// </summary>
        /// <returns>The status of the game.</returns>
        public override string ToString()
        {
            string returnValue;

            switch (this.Status)
            {
                case GameStatus.Draw:
                    returnValue = "The Game was a Draw.";
                    break;
                case GameStatus.Player1Wins:
                    returnValue = Player1.Name + " WINS!";
                    break;
                case GameStatus.Player2Wins:
                    returnValue = Player2.Name + " WINS!";
                    break;
                case GameStatus.NotStarted:
                    returnValue = "No Game Started.";
                    break;
                case GameStatus.Unknown:
                    returnValue = "Unknown";
                    break;
                case GameStatus.InProgress:
                default:
                    returnValue =  string.Format("**** {0} has {1} cards.  {2} has {3} cards."
                        , Player1.Name, Player1.CardsRemaining.ToString()
                        , Player2.Name, Player2.CardsRemaining.ToString());
                    break;
            }

            return returnValue;
            
        }
        #endregion
    }
}
