using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// A single deck of cards.
    /// </summary>
    /// <remarks>This class serializes to xml as and element named "deck".</remarks>
    [XmlRoot("deck")]
    public class PlayingDeck
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the name of the card for display purposes.
        /// </summary>
        /// <remarks>This property serializes to xml as an attribute named "name".</remarks>
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the path to the image used for the UI.
        /// </summary>
        /// <remarks>This property serializes to xml as an attribute named "backImage".</remarks>
        [XmlAttribute("backImage")]
        public string BackImage
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the cards in the deck that are not dealt to any player.
        /// </summary>
        /// <remarks>This attribute seriualizes to xml as a child element named "cards" with each item in the List
        /// serializing to an element named "card".</remarks>
        [XmlArray("cards")]
        [XmlArrayItem(Type=typeof(PlayingCard), ElementName="card")]
        public List<PlayingCard> UndealtCards
        {
            get;
            set;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion
    }
}
