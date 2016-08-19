//-----------------------------------------------------------------------
// <copyright file="DecimalExtension.cs" company="Genesys Source">
//      Copyright (c) 2016 Genesys Source. All rights reserved.
// 
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
    /// Extension to decimal class
    /// </summary>
    [CLSCompliant(true)]
    public static class  DecimalExtension
    {
        /// <summary>
        /// FormatCurrency With Comma
        /// </summary>
        public const string FormatCurrencyWithComma = "C";

        /// <summary>
        /// FormatPercent With Comma
        /// </summary>
        public const string FormatPercentWithComma = "P";
        
        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Decimal to convert to double</param>
        /// <returns>Double of the passed decimal, or 0.</returns>
        public static double ToDouble(this decimal item)
        {
            return StringExtension.TryParseDouble(item.ToStringSafe());
        }

        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Decimal to convert to integer.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static short ToShort(this decimal item)
        {
            return StringExtension.TryParseInt16(item.ToStringSafe());
        }

        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Decimal to convert to integer.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static int ToInt(this decimal item)
        {
            return StringExtension.TryParseInt32(item.ToStringSafe());
        }

        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Decimal to convert to integer.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static long ToLong(this decimal item)
        {
            return StringExtension.TryParseInt64(item.ToStringSafe());
        }
    }
}
