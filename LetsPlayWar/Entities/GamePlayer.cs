using LetsPlayWar.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// A single player in a game of war.
    /// </summary>
    public class GamePlayer
    {
        #region Private Members
        /// <summary>
        /// If the player has played his last card, this would be it.
        /// </summary>
        private PlayingCard _lastCard = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Construct the player with their name.
        /// </summary>
        /// <param name="name">The player's name.</param>
        public GamePlayer(string name)
        {
            #region Param Checking
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            #endregion

            this.Name = name;
            this.Cards = new Queue<PlayingCard>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the card queue.
        /// </summary>
        protected internal Queue<PlayingCard> Cards
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the player's name.
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the number of cards the player has remaining.
        /// </summary>
        public int CardsRemaining
        {
            get
            {
                return this.Cards.Count;
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieve the next card from the queue.
        /// </summary>
        /// <returns>The next card, or the "last card" if the player has none left.</returns>
        public PlayingCard GetNextCard()
        {
            switch (this.Cards.Count)
            {
                case 0:
                    return _lastCard;
                case 1:
                    _lastCard = this.Cards.Dequeue();
                    return _lastCard;
                default:
                    return this.Cards.Dequeue();
            }
        }
        /// <summary>
        /// Add a card to the bottom of the queue.
        /// </summary>
        /// <param name="card">The card to add.</param>
        /// <remarks>It will not be added if the card is already in the queue.</remarks>
        public void StoreCard(PlayingCard card)
        {
            #region Param Checking
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }

            #endregion

            if (!this.Cards.Contains(card))
            {
                this.Cards.Enqueue(card);
            }
        }
        #endregion
        
    }
}
