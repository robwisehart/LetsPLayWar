using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlayWar.Utilities
{
    /// <summary>
    /// Static class to house the utilities necessary to get the DisplayName attribute value.
    /// </summary>
    /// <remarks> This was written by me in another life and included here for simplification of the logging process.</remarks>
    public static class EnumUtilities
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the display name of an enum, or the enum name value if the display name is emtpy or null
        /// </summary>
        /// <param name="field">The field to show.</param>
        /// <returns>The firendly name if there is one, or the enum value if not.</returns>
        private static string FetchDisplayName(FieldInfo field)
        {
            string displayName = String.Empty;

            // load the EnumStringAttributes for the object (there should be 1 and only 1)
            object[] attributes;
            attributes = field.GetCustomAttributes(typeof(EnumDisplayNameAttribute), false);
            EnumDisplayNameAttribute attribute = null;

            if (attributes.Length > 0)
            {
                attribute = attributes[0] as EnumDisplayNameAttribute;
            }

            if (attribute != null)
            {
                // get the DisplayName property from the attribute
                displayName = attribute.DisplayName;
            }
            else
            {
                // if we couldn't get the EnumStringAttribute (or it wasn't set),
                // then default back to the name of the field
                displayName = field.Name;
            }

            return displayName;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method retrieves the DisplayNameAttribute from the metadata
        /// of the enumField passed in.
        /// </summary>
        /// <param name="enumField">A specific field of an enumeration
        /// (MyEnum.Field)</param>
        /// <exception cref="ArgumentNullException">Returned 
        /// when <paramref name="enumField" /> is null</exception>
        /// <returns cref="string">
        /// String containing DisplayNameAttribute if set. If the DisplayNameAttribute was 
        /// not set in the enum, the enumField's variable name is returned instead.
        /// </returns>
        public static string FetchDisplayName(object enumField)
        {
            // parameter checking
            if (enumField == null)
                throw new ArgumentNullException("enumField", "There was an exception fetching the attribute.");

            // determine what type of object we are dealing with
            Type myType = enumField.GetType().UnderlyingSystemType;

            // get the specific field we are interested in
            FieldInfo field = myType.GetField(enumField.ToString());

            return EnumUtilities.FetchDisplayName(field);
        }
        #endregion
    }
}
