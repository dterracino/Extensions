//-----------------------------------------------------------------------
// <copyright file="KeyValuePairSafeComparer.cs" company="Genesys Source">
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
using System.Collections.Generic;
using Genesys.Extensions;

namespace Genesys.Extras.Collections
{
    /// <summary>
    /// Compares based on key and value comparison. 
    /// HashCode is immutable based on Key + Value calculation on first call of GetHashCode().
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyValuePairSafeComparer<TKey, TValue> : EqualityComparer<KeyValuePairSafe<TKey, TValue>> where TKey : new() where TValue : new()
    {
        /// <summary>
        /// Immutable calculated hash code based on (Key.GetHashCode() * 17) + (Value.GetHashCode())
        /// </summary>
        /// <param name="obj">Object to compare, must be of type KeyValuePairSafe</param>
        /// <returns></returns>
        public override int GetHashCode(KeyValuePairSafe<TKey, TValue> obj)
        {
            KeyValuePairSafe<TKey, TValue> item = obj ?? new KeyValuePairSafe<TKey, TValue>();
            return (item.Key.GetHashCode() * 17 + item.Value.GetHashCode());
        }

        /// <summary>
        /// Equality comparison of child key and value combination
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool Equals(KeyValuePairSafe<TKey, TValue> x, KeyValuePairSafe<TKey, TValue> y)
        {
            return (x.ToStringSafe() == y.Key.ToStringSafe() && x.ToStringSafe() == y.Value.ToStringSafe());
        }
    }
}
