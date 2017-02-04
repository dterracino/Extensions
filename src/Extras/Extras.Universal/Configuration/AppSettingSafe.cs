//-----------------------------------------------------------------------
// <copyright file="AppSettingSafe.cs" company="Genesys Source">
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
using Genesys.Extras.Collections;

namespace Genesys.Extras.Configuration
{
    /// <summary>
    /// Container class for connection strings
    /// </summary>
    [CLSCompliant(true)]
    public class AppSettingSafe : KeyValuePairString
    {
        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlElements
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string AppSettings = "appSettings";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Add = "add";
            /// <summary>
            /// Clear element
            /// </summary>
            public const string Clear = "clear";
            /// <summary>
            /// Remove element
            /// </summary>
            public const string Remove = "remove";
        }

        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlAttributes
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string Key = "key";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Value = "value";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingSafe() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingSafe(KeyValuePairString item) : base(item.Key, item.Value) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public AppSettingSafe(string key, string value) : base(key, value) { }
        
        /// <summary>
        /// Returns key as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Key;
        }
    }
}
