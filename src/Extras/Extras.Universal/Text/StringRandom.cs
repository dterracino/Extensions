//-----------------------------------------------------------------------
// <copyright file="RandomString.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
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
using System.Text;
using Genesys.Extensions;

namespace Genesys.Extras.Text
{
    /// <summary>
    /// Produces random strings of mixed characters and integers
    /// </summary>
    [CLSCompliant(true)]
    public class RandomString
    {
        /// <summary>
        /// Generates a random string of the given length
        /// </summary>
        /// <param name="length">Size of the string</param>
        /// <returns>Random string</returns>
        public static string Next(int length = 10)
        {
            var returnValue = TypeExtension.DefaultString;
            var builder = new StringBuilder();
            var randomClass = new Random();
            char character = '\0';

            // Build the string
            for (var Count = 0; Count <= length - 1; Count++)
            {
                character = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomClass.NextDouble() + 65)));
                builder.Append(character);
            }
            returnValue = builder.ToString();

            return returnValue;
        }
    }
}
