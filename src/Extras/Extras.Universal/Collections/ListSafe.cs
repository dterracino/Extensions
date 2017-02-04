//-----------------------------------------------------------------------
// <copyright file="ListSafe.cs" company="Genesys Source">
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
using System.Collections.Generic;
using Genesys.Extensions;

namespace Genesys.Extras.Collections
{
    /// <summary>
    /// Contains an enumerable list of types
    /// </summary>
    [CLSCompliant(true)]
    public class ListSafe<ListType> : List<ListType>, IListSafe<ListType> where ListType : class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ListSafe() : base() { }
        
        /// <summary>
        /// Index overload
        /// </summary>
        /// <param name="key">Item to find</param>
        /// <returns>Item that matches key</returns>
        public ListType this[ListType key]
        {
            get { return base[base.IndexOf(base.Find(x => x.ToString() == key.ToString()))]; }
            set { base[base.IndexOf(base.Find(x => x.ToString() == key.ToString()))] = value; }
        }

        /// <summary>
        /// Gets an item that matches key
        /// </summary>
        /// <param name="key">Item to find</param>
        /// <returns>Item that matches key</returns>
        public ListType GetValue(ListType key)
        {
            return base.Find(x => x == key);
        }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="newItem">Item to add</param>
        public new void Add(ListType newItem)
        {
            if (this.GetValue(newItem).ToStringSafe() != TypeExtension.DefaultString)
            {
                base.RemoveAt(this.FindIndex(newItem));
            }
            base.Add(newItem);
        }

        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="itemToRemove">Item to be removed</param>
        public new void Remove(ListType itemToRemove)
        {
            if (this.GetValue(itemToRemove).ToStringSafe() != TypeExtension.DefaultString)
            {
                base.RemoveAt(this.FindIndex(itemToRemove));
            }
        }

        /// <summary>
        /// Finds the index
        /// </summary>
        /// <param name="key">Key of item to find</param>
        /// <returns>Index of item matches passed item</returns>
        public int FindIndex(ListType key)
        {
            var returnValue = TypeExtension.DefaultInteger;

            for (var count = 0; count < this.Count; count++)
            {
                if (this[count] == key)
                {
                    returnValue = count;
                    break;
                }
            }

            return returnValue;
        }
    }    
}
