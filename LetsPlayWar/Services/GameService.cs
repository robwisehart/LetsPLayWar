using LetsPlayWar.Contracts;
using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Services
{
    /// <summary>
    /// Service to run a game of War.
    /// </summary>
    /// <remarks>This class implements the <see cref="IGameService"/> interface.</remarks>
    public class GameService : IGameService
    {
        #region Private Members
        /// <summary>
        /// The IDeckService to use.
        /// </summary>
        IDeckService _deckService = null;
        /// <summary>
        /// An object to lock while instantiating the deck service, making the instantiation thread-safe.
        /// </summary>
        object _deckServiceLock = new object();
        #endregion

        #region Constructors
        /// <summary>
        /// Default runtime constructor.
        /// </summary>
        public GameService()
        {

        }
        /// <summary>
        /// Constructor to allow for mocks to be put in place for testing.
        /// </summary>
        /// <param name="deckService">The mock deck service to use during this test.</param>
        public GameService(IDeckService deckService)
        {
            _deckService = deckService;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Deck Service.
        /// </summary>
        protected internal IDeckService DeckService
        {
            get
            {
                if (null == _deckService)
                {
                    lock(_deckServiceLock)
                    {
                        if (null == _deckService)
                        {
                            _deckService = new DeckService();
                        }
                    }
                }

                return _deckService;
            }
        }
        /// <summary>
        /// Gets the number of times to shuffle the cards.
        /// </summary>
        protected internal int ShuffleAmount
        {
            get
            {
                string s = ConfigurationManager.AppSettings["NumberOfTimesToShuffle"];

                int returnValue;

                if (Int32.TryParse(s, out returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// Gets the path to the Log File.
        /// </summary>
        protected internal string LogFilePath
        {
            get
            {
                string s = ConfigurationManager.AppSettings["LogFilePath"];

                return s;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Determine the outcome of a single pair of cards.
        /// </summary>
        /// <param name="pair">The pair of cards to compare.</param>
        /// <returns>The outcome of the comparison.</returns>
        protected internal PlayingCardPairOutcome GetPairOutcome(PlayingCardPair pair)
        {
            #region Param Checking
            if (pair == null)
            {
                throw new ArgumentNullException("pair");
            }
            if (pair.Player1Card == null)
            {
                throw new ArgumentNullException("pair.Player1Card");
            }
            if (pair.Player2Card == null)
            {
                throw new ArgumentNullException("pair.Player2Card");
            }
            #endregion

            PlayingCardPairOutcome returnValue = PlayingCardPairOutcome.Unknown;

            int outcome = pair.Player1Card.CompareTo(pair.Player2Card);
            
            switch (outcome)
            {
                case -1:
                    returnValue = PlayingCardPairOutcome.Player2Wins;
                    break;
                case 0:
                    returnValue = PlayingCardPairOutcome.Draw;
                    break;
                case 1:
                    returnValue = PlayingCardPairOutcome.Player1Wins;
                    break;
                default:
                    break;
            }

            return returnValue;
        }
        /// <summary>
        /// Log the results of a single round for a game or War.
        /// </summary>
        /// <param name="g">The game instance.</param>
        /// <param name="round">The round instance.</param>
        protected internal void LogRoundResults(Game g, GameRound round)
        {
            #region Param Checking
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (round == null)
            {
                throw new ArgumentNullException("round");
            }
            if (g.Player1 == null)
            {
                throw new ArgumentNullException("g.Player1");
            }
            if (g.Player2 == null)
            {
                throw new ArgumentNullException("g.Player2");
            }
            #endregion

            try
            {
                using (StreamWriter stream = File.AppendText(g.PathToGameLog))
                {
                    stream.Write(string.Format(round.ToString(), (g.Player1.Name ?? "<UNKNOWN>"), (g.Player2.Name ?? "<UNKNOWN>")));
                    stream.WriteLine(g.ToString());

                    switch (g.Status)
                    {
                        case GameStatus.Player1Wins:
                            stream.WriteLine("*********************************************");
                            stream.WriteLine(string.Format("** {0} Wins!", g.Player1.Name));
                            stream.WriteLine("*********************************************");
                            break;
                        case GameStatus.Player2Wins:
                            stream.WriteLine("*********************************************");
                            stream.WriteLine(string.Format("** {0} Wins!", g.Player2.Name));
                            stream.WriteLine("*********************************************");
                            break;
                        case GameStatus.Draw:
                            break;
                        case GameStatus.InProgress:
                        case GameStatus.NotStarted:
                        case GameStatus.Unknown:
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There wa an exception writing to the log file.  See the inner exception for details.", ex);
            }
        }
        /// <summary>
        /// Determine the status of a game and if there is a winner.
        /// </summary>
        /// <param name="g">The game instance.</param>
        /// <param name="round">The round instance.</param>
        protected internal void DetermineGameWinner(Game g, GameRound round)
        {
            #region Param Checking
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (round == null)
            {
                throw new ArgumentNullException("round");
            }
            if (g.Player1 == null)
            {
                throw new ArgumentNullException("g.Player1");
            }
            if (g.Player2 == null)
            {
                throw new ArgumentNullException("g.Player2");
            }
            #endregion

            switch (round.Outcome)
            {
                case GameRoundOutcome.Player1Winner:
                    g.Status = g.Player2.CardsRemaining == 0 ? GameStatus.Player1Wins : GameStatus.InProgress;
                    break;
                case GameRoundOutcome.Player2Winner:
                    g.Status = g.Player1.CardsRemaining == 0 ? GameStatus.Player2Wins : GameStatus.InProgress;
                    break;
                case GameRoundOutcome.Draw:
                    g.Status = GameStatus.Draw;
                    break;
                default:
                    g.Status = GameStatus.InProgress;
                    break;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Start a new game.
        /// </summary>
        /// <param name="player1Name">The name of player 1.</param>
        /// <param name="player2Name">The name of player 2.</param>
        /// <returns>The newly started game.</returns>
        public Game StartGame(string player1Name, string player2Name)
        {
            #region Param Checking
            if (String.IsNullOrWhiteSpace(player1Name))
            {
                throw new ArgumentNullException("player1Name");
            }
            if (String.IsNullOrWhiteSpace(player2Name))
            {
                throw new ArgumentNullException("player2Name");
            }
            #endregion

            GamePlayer p1 = new GamePlayer(player1Name);
            GamePlayer p2 = new GamePlayer(player2Name);

            PlayingDeck deck = null;

            try
            {
                deck = this.DeckService.GetDeck();

                int shuffleCount = this.ShuffleAmount;

                for (int i = 0; i < shuffleCount; i++)
                {
                    this.DeckService.ShuffleDeck(deck);
                }

                for (int j = 0; j < deck.UndealtCards.Count; j += 2)
                {
                    p1.Cards.Enqueue(deck.UndealtCards[j]);

                    //Odd case where there is an uneven number of cards in the deck.
                    if (deck.UndealtCards.Count > j + 1)
                    {
                        p2.Cards.Enqueue(deck.UndealtCards[j + 1]);
                    }

                }

                deck.UndealtCards.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem dealing out the cards.  See inner exception for details.", ex);
            }

            Game g = null;

            try
            {
                g = new Game(p1, p2, deck, this.LogFilePath);

                g.Status = GameStatus.InProgress;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an exception initializing the game.  See inner exception for details.", ex);
            }

            return g;
        }
        /// <summary>
        /// For the given game, run a round.
        /// </summary>
        /// <param name="g">The game to run a round for.</param>
        /// <returns>The round.  Also update the status of the game as required.</returns>
        public GameRound RunRound(Game g)
        {
            #region Private Members
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (g.Player1 == null)
            {
                throw new ArgumentNullException("g.Player1");
            }
            if (g.Player2 == null)
            {
                throw new ArgumentNullException("g.Player2");
            }
            #endregion

            GameRound returnValue = new GameRound();


            //1. While the outcome of the card pair is in question:
            while (returnValue.Outcome == GameRoundOutcome.Unknown)
            {
                //A. Create a Card Pair
                PlayingCardPair pair = new PlayingCardPair();
                pair.Player1Card = g.Player1.GetNextCard();
                pair.Player2Card = g.Player2.GetNextCard();
                
                //B. Determine the outcome of the Pair
                pair.Status = this.GetPairOutcome(pair);

                returnValue.Pairs.Add(pair);

                //C. Loop if there was not a winner.
                if (pair.Status == PlayingCardPairOutcome.Player1Wins)
                {
                    returnValue.Outcome = GameRoundOutcome.Player1Winner;
                }
                else if (pair.Status == PlayingCardPairOutcome.Player2Wins)
                {
                    returnValue.Outcome = GameRoundOutcome.Player2Winner;
                }
                else
                {
                    if (g.Player1.CardsRemaining == 0 && g.Player2.CardsRemaining == 0)
                    {
                        returnValue.Outcome = GameRoundOutcome.Draw;
                    }
                    else
                    {
                        //D. In the case of a draw, add the burn card.
                        pair = new PlayingCardPair();
                        pair.Player1Card = g.Player1.GetNextCard();
                        pair.Player2Card = g.Player2.GetNextCard();
                        pair.Status = PlayingCardPairOutcome.FaceDown;

                        returnValue.Pairs.Add(pair);
                    }
                }
            }

            //2. Add the Loser's and Winner's cards to the winner's queue.
            GamePlayer roundWinner = null;

            switch (returnValue.Outcome)
            {
                case GameRoundOutcome.Player1Winner:
                    roundWinner = g.Player1;
                    break;
                case GameRoundOutcome.Player2Winner:
                    roundWinner = g.Player2;
                    break;
                case GameRoundOutcome.Draw:
                case GameRoundOutcome.Unknown:
                default:
                    break;
            }

            if (roundWinner != null)
            {
                foreach (PlayingCardPair p in returnValue.Pairs)
                {
                    roundWinner.StoreCard(p.Player1Card);
                    roundWinner.StoreCard(p.Player2Card);
                }
            }

            //3. Determine if there was a game winner.
            DetermineGameWinner(g, returnValue);

            //3. Log the results
            this.LogRoundResults(g, returnValue);

            //4. Return the round.
            return returnValue;
        }
        #endregion
    }
}
