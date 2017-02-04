//-----------------------------------------------------------------------
// <copyright file="KeyValueListSafe.cs" company="Genesys Source">
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
using Genesys.Extras.Serialization;

namespace Genesys.Extras.Collections
{
    /// <summary>
    /// Serializable Key Value List strongly typed
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class KeyValueListSafe<TKey, TValue> : List<KeyValuePairSafe<TKey, TValue>> where TKey : new() where TValue : new()
    {
        /// <summary>
        /// Item last selected from list
        /// </summary>
        public string SelectedItem { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// Serialize and de-serialize
        /// </summary>
        public JsonSerializer<KeyValueListSafe<TKey, TValue>> Serializer = new JsonSerializer<KeyValueListSafe<TKey, TValue>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public KeyValueListSafe() : base() { }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="key">Key of item to add</param>
        /// <param name="value">Value of item to add</param>
        /// <remarks></remarks>
        public virtual void Add(TKey key, TValue value)
        {
            Remove(key);
            Add(new KeyValuePairSafe<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="key"></param>
        /// <remarks></remarks>
        public virtual void Remove(TKey key)
        {
            var index = base.IndexOf(base.Find(x => x.Key.ToStringSafe() == key.ToStringSafe()));
            if (index > -1)
            { 
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Null safe and self-normalizing indexer
        /// </summary>
        /// <param name="key">Item to get/set based on key index match</param>
        /// <returns>Get returns item from list, or not found will return new instantiation. Set will add/update match by key.</returns>
        public KeyValuePairSafe<TKey, TValue> this[TKey key]
        {
            get
            {
                KeyValuePairSafe<TKey, TValue> returnValue
                    = base.Find(x => x.Key.ToStringSafe() == key.ToStringSafe()).DirectCastSafe<KeyValuePairSafe<TKey, TValue>>();
                return returnValue;
            }
            set
            {
                Add(value);
            }
        }

        /// <summary>
        /// Adds another member to the list
        /// </summary>
        /// <param name="key">Key to search</param>
        /// <remarks>Returns value if found, else the default value for the type</remarks>
        public TValue GetValue(TKey key)
        {
            return this[key].Value;
        }
    }
}
