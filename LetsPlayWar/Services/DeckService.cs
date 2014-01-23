using LetsPlayWar.Contracts;
using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Services
{
    /// <summary>
    /// Service for managing a deck of cards.
    /// </summary>
    /// <remarks>This class implements the <see cref="IDeckService"/> interface.</remarks>
    public class DeckService : IDeckService
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Protected Methods
        /// <summary>
        /// Use the config file to get the deck loader service.
        /// </summary>
        /// <returns>A deck loader service.</returns>
        protected internal IDeckLoaderService GetLoaderService()
        {
            string serviceInfo = ConfigurationManager.AppSettings["DeckLoaderService"];

            if (String.IsNullOrWhiteSpace(serviceInfo))
            {
                throw new ConfigurationErrorsException("The DeckLoadService AppSetting was not found.");
            }

            string[] serviceSplit = serviceInfo.Split(",".ToCharArray());

            if (serviceSplit.Length != 2)
            {
                throw new ConfigurationErrorsException("The DeckLoadService AppSetting was not formatted correctly."
                    + " Expected format is [ClassName], [AssemblyName]");
            }

            IDeckLoaderService service = null;

            try
            {
                service = Activator.CreateInstance(serviceSplit[1], serviceSplit[0]).Unwrap() as IDeckLoaderService;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("The DeckLoaderService AppSetting value was not a valid class."
                    + "  See inner exception for details.", ex);
            }
            
            if (service == null)
            {
                throw new ConfigurationErrorsException("The DeckLoaderService AppSetting value was not a valid "
                    + "LetsPlayWar.Contracts.IDeckLoader class.");
            }

            return service;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load a deck from somewhere.
        /// </summary>
        /// <returns>A fully loaded deck of cards.</returns>
        public PlayingDeck GetDeck()
        {
            string data = ConfigurationManager.AppSettings["DeckLoaderServiceData"];

            if (String.IsNullOrWhiteSpace(data))
            {
                throw new ConfigurationErrorsException("The DeckLoaderServiceData App Setting was not found.");
            }

            PlayingDeck returnValue = null;

            try
            {
                returnValue = this.GetLoaderService().LoadDeck(data);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error loading the deck of cards.  See innner exception for details.", ex);
            }

            return returnValue;
        }
        /// <summary>
        /// Shuffle the deck randomly.
        /// </summary>
        /// <param name="deck">The deck to shuffle.</param>
        /// <remarks>This shuffles the deck with one of the best .NET randomization algorithms.  The algorithm is used in encryption.</remarks>
        public void ShuffleDeck(PlayingDeck deck)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            int n = deck.UndealtCards.Count;
           
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                PlayingCard value = deck.UndealtCards[k];
                deck.UndealtCards[k] = deck.UndealtCards[n];
                deck.UndealtCards[n] = value;
            }
        }
        #endregion
        
    }
}
