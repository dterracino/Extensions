//-----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Genesys Source">
//      Copyright (c) 2016 Genesys Source. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Genesys.Extensions
{
    /// <summary>
    /// StringExtension
    /// </summary>
    [CLSCompliant(true)]
    public static class  StringExtension
    {
        /// <summary>
        /// Converts a string to Boolean
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static bool TryParseBoolean(this string item, bool notFoundValue = false)
        {
            bool returnValue = TypeExtension.DefaultBoolean;
            bool convertValue = TypeExtension.DefaultBoolean;

            if (String.IsNullOrEmpty(item) == false)
            {                
                if (item.TryParseInt16() != TypeExtension.DefaultInt16) // Catch integers, as TryParse only evaluates "true" and "false", not "0".
                {
                    returnValue = item.TryParseInt16() == 0 ? false : true;
                }
                else if (Boolean.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts a string to Int16
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static short TryParseInt16(this string item, short notFoundValue = -1)
        {
            short returnValue = TypeExtension.DefaultInt16;
            short convertValue = TypeExtension.DefaultInt16;

            // Try to parse it out
            if (String.IsNullOrEmpty(item) == false)
            {
                if (Int16.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to int
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static int TryParseInt32(this string item, int notFoundValue = -1)
        {
            var returnValue = TypeExtension.DefaultInteger;
            var convertValue = TypeExtension.DefaultInteger;

            if (String.IsNullOrEmpty(item) == false)
            {
                if (int.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to Int64
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static long TryParseInt64(this string item, long notFoundValue = -1)
        {
            long returnValue = TypeExtension.DefaultInteger;
            long convertValue = TypeExtension.DefaultInteger;

            if (String.IsNullOrEmpty(item) == false)
            {
                if (Int64.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts string to Guid
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static Guid TryParseGuid(this string item, Guid notFoundValue =  default(Guid))
        {
            Guid returnValue = TypeExtension.DefaultGuid;
            Guid convertValue = TypeExtension.DefaultGuid;

            if (String.IsNullOrEmpty(item) == false)
            {
                try
                {
                    returnValue = new Guid(item);
                }
                catch
                {
                    returnValue = notFoundValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts string to decimal
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static decimal TryParseDecimal(this string item, decimal notFoundValue = 0m)
        {
            decimal returnValue = TypeExtension.DefaultDecimal;
            decimal convertValue = TypeExtension.DefaultDecimal;

            if (String.IsNullOrEmpty(item) == false)
            {
                if (Decimal.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts string to double
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static double TryParseDouble(this string item, double notFoundValue = 0.0)
        {           
            double returnValue = TypeExtension.DefaultDouble;
            double convertValue = TypeExtension.DefaultDouble;

            if (String.IsNullOrEmpty(item) == false)
            {
                if (Double.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts string to DateTime
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted or not found value (01/01/1900) of the source item</returns>
        public static DateTime TryParseDateTime(this string item)
        {
            DateTime notFoundValue = TypeExtension.DefaultDate;
            DateTime returnValue = TypeExtension.DefaultDate;
            DateTime convertDate = TypeExtension.DefaultDate;

            item = item.Trim();
            if (item.IsInteger() == true & item.Length == 8)
            {
                item = item.Substring(0, 2) + "-" + item.Substring(2, 2) + "-" + item.Substring(4, 4);
            }
            if ((!(String.IsNullOrEmpty(item))) & (item.Trim().Length >= 8))
            {
                if (DateTime.TryParse(item, out convertDate))
                {
                    if (convertDate.IsSavable())
                    {
                        returnValue = convertDate;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to time (of date 01/01/1900)
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static DateTime TryParseTime(this string item)
        {
            DateTime notFoundValue = TypeExtension.DefaultDate;
            DateTime returnValue = TypeExtension.DefaultDate;
            DateTime convertDate = TypeExtension.DefaultDate;

            item = item.Trim();
            if ((String.IsNullOrEmpty(item) == false))
            {
                if (DateTime.TryParse("01/01/1900 " + item, out convertDate))
                {
                    returnValue = convertDate;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Pulls right characters from a String
        /// </summary>
        /// <param name="item">Item to extract right characters</param>
        /// <param name="rightCharacters">Number of characters, starting from the right</param>
        /// <returns>Characters or original string, starting from the right</returns>
        public static string SubstringRight(this string item, int rightCharacters)
        {
            return item.SubstringSafe(item.Length - rightCharacters);
        }

        /// <summary>
        /// Pulls left characters from a String
        /// </summary>
        /// <param name="item">Item to extract left characters</param>
        /// <param name="leftCharacters">Number of characters, starting from the left</param>
        /// <returns>Characters or original string, starting from the left</returns>
        public static string SubstringLeft(this string item, int leftCharacters)
        {
            return item.SubstringSafe(0, leftCharacters);
        }

       /// <summary>
       /// Extracts substring exception-safe
       /// </summary>
       /// <param name="item">Item to extract the substring</param>
       /// <param name="starting">Starting position</param>
       /// <param name="length">Number of characters to try to extract</param>
       /// <returns>Extracted characters, or original string if cant substring.</returns>
        public static string SubstringSafe(this string item, int starting, int length = -1)
        {
            var returnValue = TypeExtension.DefaultString;

            if (length == -1)
            {
                length = item.Length - starting;
            }
            if (item.Length >= length - (starting + 1))
            {
                if (length > -1)
                {
                    returnValue = item.Substring(starting, length);
                }
                else
                {
                    returnValue = item.Substring(starting);
                }
            }
            else
            {
                returnValue = item;
            }


            return returnValue;
        }
        
        /// <summary>
        /// Applies pascal casing to a string
        /// </summary>
        /// <param name="uncasedString">string to case</param>
        /// <returns>Cased string</returns>
        public static string ToPascalCase(this string uncasedString)
        {
            var returnValue = TypeExtension.DefaultString;
            var partiallyCased = TypeExtension.DefaultString;

            // Do nothing if nothing to work with
            if (string.IsNullOrEmpty(uncasedString) == false & ((uncasedString.ToLower() == uncasedString) | (uncasedString.ToUpper() == uncasedString) & uncasedString.Contains(" ")))
            {
                uncasedString = uncasedString.Trim();
                partiallyCased = StringExtension.FormatCasePascal(uncasedString, " ", false);
                partiallyCased = StringExtension.FormatCasePascal(partiallyCased, "-");
                partiallyCased = StringExtension.FormatCasePascal(partiallyCased, "'");
                partiallyCased = StringExtension.FormatCaseException(partiallyCased, " ");
                returnValue = partiallyCased;
                returnValue = returnValue.Trim();
                if (returnValue.Length > uncasedString.Length)
                {
                    returnValue = returnValue.Substring(0, uncasedString.Length);
                }
            }
            else
            {
                returnValue = uncasedString;
            }

            return returnValue.Trim();
        }

        /// <summary>
        /// Applies pascal casing to a string
        /// </summary>
        /// <param name="uncasedString">string to case</param>
        /// <param name="parseCharacter">Character that decides when to start a new capital letter</param>
        /// <param name="useExistingCase">Protects all upper characters, or previously cased characters from getting re-cased.</param>
        /// <returns>Cased string</returns>
        private static string FormatCasePascal(string uncasedString, string parseCharacter, bool useExistingCase = true)
        {
            var returnValue = TypeExtension.DefaultString;
            string[] words = null;
            var word = TypeExtension.DefaultString;
            char firstLetter = TypeExtension.DefaultChar;
            var count = TypeExtension.DefaultInteger;

            words = uncasedString.Split(parseCharacter.ToCharArray());
            for (count = 0; count <= words.Length - 1; count++)
            {
                // Upper-case abbreviations (P.O., B.S.A.)
                if ((words[count].Replace(".", string.Empty).Length == (words[count].Length / 2)))
                {
                    word = words[count].ToUpper();
                }
                else
                {
                    if (useExistingCase == false)
                    {
                        word = words[count].ToLower();
                    }
                    else
                    {
                        word = words[count];
                    }
                }
                if (word.Length > 0)
                {
                    firstLetter = char.ToUpper(word[0]);
                    returnValue = returnValue + firstLetter + word.Substring(1) + parseCharacter;
                }
            }


            return returnValue;
        }

        /// <summary>
        /// Formats exceptions for Pascal Case 
        ///  (i.e. Mr. Smith II should not be pascal cased to Ii)
        /// </summary>
        /// <param name="uncasedString">string to search for exceptions to special-case</param>
        /// <param name="parseCharacter">Character that decides when to start a new capital letter</param>
        /// <returns>Cased item based on the exception casing rules</returns>
        private static string FormatCaseException(string uncasedString, string parseCharacter)
        {
            var returnValue = TypeExtension.DefaultString;
            string[] words = null;
            var word = TypeExtension.DefaultString;
            char firstLetter = TypeExtension.DefaultChar;
            var count = TypeExtension.DefaultInteger;

            words = uncasedString.Split(parseCharacter.ToCharArray());
            for (count = 0; count <= words.Length - 1; count++)
            {
                word = words[count];
                if (word.Length > 0)
                {
                    switch (word.ToLower())
                    {
                        case "jr.":
                            word = "Jr.";
                            break;
                        case "ii":
                            word = "II";
                            break;
                        case "iii":
                            word = "III";
                            break;
                        case "iv":
                            word = "IV";
                            break;
                    }
                    returnValue = returnValue + word + parseCharacter;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Adds string/char if string begins with that string/char
        /// </summary>
        /// <param name="item">Item to Add part</param>
        /// <param name="toAdd">string to Add if match</param>
        /// <returns>Original item without the Addd substring</returns>
        public static string AddFirst(this string item, string toAdd)
        {
            var returnValue = item.Trim();

            if (item.IsFirst(toAdd) == false)
            {
                returnValue = (toAdd + item);
            }

            return returnValue;
        }

        /// <summary>
        /// Adds string/char if string ends with that string/char
        /// </summary>
        /// <param name="item">Item to Add part</param>
        /// <param name="toAdd">string to Add if match</param>
        /// <returns>Original item without the Addd substring</returns>
        public static string AddLast(this string item, string toAdd)
        {
            var returnValue = item.Trim();

            if (item.IsLast(toAdd) == false)
            {
                returnValue = (item + toAdd);
            }

            return returnValue;
        }

        /// <summary>
        /// Removes string/char if string begins with that string/char
        /// </summary>
        /// <param name="item">Item to remove part</param>
        /// <param name="toRemove">string to remove if match</param>
        /// <returns>Original item without the removed substring</returns>
        public static string RemoveFirst(this string item, string toRemove)
        {
            var returnValue = item.Trim();

            if (item.IsFirst(toRemove) == true)
            {
                returnValue = item.SubstringRight(item.Length - toRemove.Length );
            }

            return returnValue;
        }

        /// <summary>
        /// Removes string/char if string ends with that string/char
        /// </summary>
        /// <param name="item">Item to remove part</param>
        /// <param name="toRemove">string to remove if match</param>
        /// <returns>Original item without the removed substring</returns>
        public static string RemoveLast(this string item, string toRemove)
        {
            var returnValue = item.Trim();

            if (item.IsLast(toRemove) == true)
            {
                returnValue = item.SubstringLeft(item.Length - toRemove.Length);
            }

            return returnValue;
        }

        /// <summary>
        /// Is the first character(s) equal to the passed characters?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <param name="firstCharacters">Character to look for</param>
        /// <returns>True/False if found characters in position</returns>
        public static bool IsFirst(this string item, string firstCharacters)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            if (item.Length >= firstCharacters.Length)
            {
                if (item.SubstringSafe(0, firstCharacters.Length) == firstCharacters)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Is the last character(s) equal to the passed characters?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <param name="lastCharacters">Character to look for</param>
        /// <returns>True/False if found characters in position</returns>
        public static bool IsLast(this string item, string lastCharacters)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            if (item.Length >= lastCharacters.Length)
            {
                if (item.SubstringRight(lastCharacters.Length) == lastCharacters)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Is this item an email address?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <param name="emptyStringOK">Flags an empty string as valid, even though it is not an email address</param>
        /// <returns>True if this is an email address (or if empty.)</returns>
        public static bool IsEmail(this string item, bool emptyStringOK = true)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            item = item.Trim();
            if ((emptyStringOK == true & item.Length == 0))
            {
                returnValue = true;
            }
            else
            {
                if ((item.Contains("@") & item.Contains("."))
                    && (item.IndexOf(".", item.IndexOf("@")) > item.IndexOf("@"))
                    && (item.Contains(" ") == false)
                    && (item.SubstringSafe(item.IndexOf("@") + 1).Contains("@") == false))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item an integer?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this is an integer.</returns>
        public static bool IsInteger(this string item)
        {
            long result = TypeExtension.DefaultInteger;
            bool returnValue = TypeExtension.DefaultBoolean;

            if (item.TryParseInt64() != TypeExtension.DefaultInteger)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item all upper case?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this is all upper case.</returns>
        public static bool IsCaseUpper(this string item)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            if (item == item.ToUpper())
            {
                returnValue = true;
            }
 
            return returnValue;
        }

        /// <summary>
        /// Is this item all lower case?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this is all lower case.</returns>
        public static bool IsCaseLower(this string item)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            if (item == item.ToLower())
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item mixed case?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this has mixed case.</returns>
        public static bool IsCaseMixed(this string item)
        {
            return !item.IsCaseLower() & !item.IsCaseUpper();
        }
    }
}
