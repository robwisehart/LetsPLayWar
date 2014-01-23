using LetsPlayWar.Contracts;
using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LetsPlayWar.Services.DeckLoaders
{
    /// <summary>
    /// This class is a Deck Loader that loads the deck from an XML file.
    /// </summary>
    /// <remarks>This class implements the <see cref="IDeckLoader"/> interface.</remarks>
    public class XmlDeckLoaderService : IDeckLoaderService
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        /// <summary>
        /// Load the deck from an xml file.
        /// </summary>
        /// <param name="data">The filename for the xml file to load from.</param>
        /// <returns>The deck after it is loaded.</returns>
        public PlayingDeck LoadDeck(string data)
        {
            #region Param Checking
            if (String.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException("data");
            }
            #endregion

            XmlSerializer ser = new XmlSerializer(typeof(PlayingDeck));
            PlayingDeck returnValue;

            try
            { 
                using (FileStream stream = File.OpenRead(data))
                {
                    returnValue = ser.Deserialize(stream) as PlayingDeck;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an exception opening the specified XML file.  See inner exception for details.", ex);
            }

            return returnValue;
        }
        #endregion
    }
}
