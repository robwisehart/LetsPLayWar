using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LetsPlayWar.Entities
{
    /// <summary>
    /// A single playing card in the game of war.
    /// </summary>
    /// <remarks>This class implements IComparable for using to compare to other cards.</remarks>
    public class PlayingCard : IComparable<PlayingCard>
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the name of the card for display purposes.
        /// </summary>
        /// <remarks>When serializing this to XML, the attribute's name is "name".</remarks>
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the value of the card.
        /// </summary>
        /// <remarks>When serializing this to XML, the attribute's name is "value".</remarks>
        [XmlAttribute("value")]
        public int Value
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the path to the image used for the UI.
        /// </summary>
        /// <remarks>When serializing this to xml, the attribute's name is "image".</remarks>
        [XmlAttribute("image")]
        public string ImagePath
        {
            get;
            set;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Used for comaring the winning card.
        /// </summary>
        /// <param name="other">The other card to compare to.</param>
        /// <returns>A signed number indicating the relative values of this instance and value:
        ///    Less than zero == this instance is less than value.<br />
        ///    Zero == this instance is equal to value.<br />
        ///    Greater than zero == this instance is greater than value.</returns>
        public int CompareTo(PlayingCard other)
        {
            return this.Value.CompareTo(other.Value);
        }
        #endregion

        
    }
}
