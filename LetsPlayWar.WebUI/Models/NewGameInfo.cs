using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsPlayWar.WebUI.Models
{
    /// <summary>
    /// Information collected to start a new game.
    /// </summary>
    public class NewGameInfo
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the name of Player 1.
        /// </summary>
        [Required]
        public string Player1Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the name of Player 2.
        /// </summary>
        [Required]
        public string Player2Name
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