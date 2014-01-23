using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Utilities
{
    /// <summary>
    /// This attribute provides user-friendly string values for Enums. 
    /// </summary>
    /// <remarks>
    /// <b>Created By: </b>robertw<br />
    /// <b>Created Date: </b>10/19/2011 11:48:32 AM<br />
    /// <b>Purpose:</b><br/>
    /// This class allows the user to provide a user-friendly string to return
    /// instead of the Enum.ToString() value.
    /// <br/><b>Usage:</b><br/>
    /// <br/><b>Notes:</b> This was written by me in another life and included here for simplification of the logging process.<br/>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDisplayNameAttribute : Attribute
    {
        #region Private Members
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the DisplayNameAttribute class
        /// </summary>
        /// <param name="displayName">Friendly name for the enum value</param>
        /// <exception cref="ArgumentNullException">Returned when <paramref name="displayName" /> is null</exception>
        public EnumDisplayNameAttribute(string displayName)
        {
            // parameter checking
            if (displayName == null)
                throw new ArgumentNullException("displayName"
                    , "The displayName property was not set.");
            // NOTE: It is OK for displayName to be an empty string

            // set the the displayName
            this.DisplayName = displayName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the friendly name.
        /// </summary>
        public string DisplayName
        {
            get;
            private set;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion
    }
}
