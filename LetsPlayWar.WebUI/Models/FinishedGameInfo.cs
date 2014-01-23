using LetsPlayWar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LetsPlayWar.WebUI.Models
{
    /// <summary>
    /// Information to display to the user when the game is completed.
    /// </summary>
    public class FinishedGameInfo
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        /// <summary>
        /// Gets/sets the path to the game's log file.
        /// </summary>
        public string PathToGameLog
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the final status of the game.
        /// </summary>
        public GameStatus FinalGameStatus
        {
            get;
            set;
        }
        /// <summary>
        /// Gets/sets the final entry in the log file indicating who won the game.
        /// </summary>
        public string WinnerStatement
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