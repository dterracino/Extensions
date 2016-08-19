//-----------------------------------------------------------------------
// <copyright file="NameValueCollectionExtension.cs" company="Genesys Source">
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
using System.Collections.Specialized;

namespace Genesys.Extensions
{
    /// <summary>
    /// Extends System.Type
    /// </summary>
    [CLSCompliant(true)]
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// Converts to flattened array of string, string
        /// </summary>
        /// <param name="item">NameValueCollection to convert to string[count, 2] array</param>
        /// <returns>True if this connection can be opened</returns>
        public static string[,] ToArraySafe(this NameValueCollection item)
        {
            NameValueCollection itemToConvert = item ?? new NameValueCollection();
            string[,] returnValue = new string[itemToConvert.Count, 2];

            for(int count = 0; count < itemToConvert.Count; count++)
            {
                foreach(string itemKey in itemToConvert)
                {
                    returnValue[count, 0] = itemKey;
                    returnValue[count, 1] = itemToConvert[count];
                }
            }

            return returnValue;
        }
    }
}
