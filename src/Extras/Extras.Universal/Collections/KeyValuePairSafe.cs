//-----------------------------------------------------------------------
// <copyright file="KeyValuePairSafe.cs" company="Genesys Source">
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
using System.Collections.Generic;
using Genesys.Extensions;

namespace Genesys.Extras.Collections
{
    /// <summary>
    /// Simple serializable KeyValuePair strongly typed
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class KeyValuePairSafe<TKey, TValue> : IKeyValuePair<TKey, TValue>, IEquatable<KeyValuePairSafe<TKey, TValue>> where TKey : new() where TValue : new()
    {
        private int hashCode = TypeExtension.DefaultInteger;

        /// <summary>
        /// Key
        /// </summary>
        protected TKey keyField = new TKey();

        /// <summary>
        /// Value
        /// </summary>
        protected TValue valueField = new TValue();
        
        /// <summary>
        /// Key, self-initializes to be null safe
        /// </summary>
        public TKey Key { get { return keyField; } set { keyField = value == null ? new TKey() : value; } }
        
        /// <summary>
        /// Value, self-initializes to be null safe
        /// </summary>
        public TValue Value { get { return valueField; } set { valueField = value == null ? new TValue() : value; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public KeyValuePairSafe() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public KeyValuePairSafe(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Unique hash of (Key.GetHashCode() * 17 + Value.GetHashCode());
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (hashCode == TypeExtension.DefaultInteger)
            {
                hashCode = (Key == null ? new TKey() : Key).GetHashCode() * 17 + (Value == null ? new TValue() : Value).GetHashCode();
            }
            return hashCode;
        }

        /// <summary>
        /// Default comparer
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(KeyValuePairSafe<TKey, TValue> other)
        {
            return (Key.ToStringSafe() == other.Key.ToStringSafe() && Value.ToStringSafe() == other.Value.ToStringSafe());
        }
    }
}
