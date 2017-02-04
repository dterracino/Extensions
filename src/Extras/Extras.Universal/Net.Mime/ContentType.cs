//-----------------------------------------------------------------------
// <copyright file="ContentType.cs" company="Genesys Source">
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
using Genesys.Extensions;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Holds a float content type and file extension
    /// </summary>
    public class ContentType
    {        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; protected set; } = TypeExtension.DefaultString;
        /// <summary>
        /// Extension
        /// </summary>
        public string Extension { get; protected set; } = TypeExtension.DefaultString;
        
        /// <summary>
        /// Force immutability
        /// </summary>
        private ContentType() : base() { }

        /// <summary>
        /// Constructor with data
        /// </summary>
        /// <param name="name"></param>
        public ContentType(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Constructor with data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        public ContentType(string name, string extension)
            : this()
        {
            Name = name;
            Extension = extension;
        }
    }
}
