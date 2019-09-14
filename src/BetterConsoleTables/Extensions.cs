using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public static class Extensions
    {
        // Pulled from Clawfoot software
        // Didn't pull it in from nuget as it's dependency graph is a mess atm

        /// <summary>
        /// The relative Left or Right hand side
        /// </summary>
        public enum RelativeHandedSide
        {
            Undefined = 0,
            Left = 1,
            Right = 2
        }

        /// <summary>
        /// Returns a new string that center-aligns the characters in this instance by padding them on the left and the right with a specified Unicode character, for a specified total length.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
        /// <param name="paddingCharacter">A Unicode padding character.</param>
        /// <param name="paddingStartSide">The side that padding will start at, determines which side will have an additional character if the string has an odd length</param>
        /// <param name="padIfNPlusOne">If the string should be padded if it's length == maxLength+1</param>
        /// <returns>
        ///     A new string that is equivalent to this instance, but center-aligned and padded on the left and right 
        ///     with as many paddingChar characters as needed to create a length of totalWidth. 
        ///     If totalWidth is equal to or less than the length of this instance, the method returns a reference to the existing instance.
        ///     If totalWidth is equal to the length of this instance + 1, it will pad only to the paddingStartSide, if padIfNPlusOne is true. Otherwise it will return a reference to the existing instance.
        ///</returns>
        public static string PadLeftAndRight(this string str, int maxLength, char paddingCharacter, RelativeHandedSide paddingStartSide = RelativeHandedSide.Left, bool padIfNPlusOne = true)
        {
            if (str.Length >= maxLength)
            {
                return str;
            }

            if (str.Length == maxLength + 1 && !padIfNPlusOne)
            {
                return str;
            }

            int padPerSide = (int)Math.Floor(((double)maxLength - (double)str.Length) / 2d);
            int carryover = maxLength - (padPerSide * 2) - str.Length;
            int leftSideTotalLength = str.Length + padPerSide; // Total length after first side is padded (without carryover)

            // Only need the left as that's the first pad. 
            // If there is no carryover on the left, it will be automatically applied on the right thanks to maxLength
            leftSideTotalLength += paddingStartSide == RelativeHandedSide.Left ? carryover : 0;

            string paddedLeft = str.PadLeft(leftSideTotalLength, paddingCharacter);
            return paddedLeft.PadRight(maxLength, paddingCharacter);
        }
    }
}
