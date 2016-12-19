//-----------------------------------------------------------------------
// <copyright file="HttpRequestDelete.cs" company="Genesys Source">
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
using System.Threading.Tasks;
using Genesys.Extras.Security.Cryptography;
using Genesys.Extras.Serialization;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Communicates via DELETE, strongly typed
    /// </summary>
    [CLSCompliant(true)]
    public class HttpRequestDelete<TypeToReceive> : HttpRequestDeleteString where TypeToReceive : new()
    {
        /// <summary>
        /// DataReceivedRaw value decrypted
        /// </summary>
        public TypeToReceive DataReceivedDeserialized { get; set; } = new TypeToReceive();
        /// <summary>
        /// De-serializer of response
        /// </summary>
        public ISerializer<TypeToReceive> Deserializer { get; protected set; } = new JsonSerializer<TypeToReceive>();
        /// <summary>
        /// KnownTypes assist the serializer with types that cannot be mapped by default
        /// </summary>
        public List<Type> KnownTypes { get; protected set; } = new List<Type>();
        
        /// <summary>
        /// Immutable
        /// </summary>
        public HttpRequestDelete(string url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestDelete(string url, ISerializer<TypeToReceive> deserializer) : this(url) { Deserializer = deserializer; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestDelete(string url, IEncryptor encrptor) : base(url, encrptor) { }
        
        /// <summary>
        /// Sync send and Receive
        /// </summary>
        /// <returns></returns>
        public virtual new TypeToReceive Send()
        {
            base.Send();
            DataReceivedDeserialized = this.Deserializer.Deserialize(base.DataReceivedDecrypted);
            return DataReceivedDeserialized; 
        }

        /// <summary>
        /// Async send and Receive
        /// </summary>
        /// <returns></returns>
        public virtual new async Task<TypeToReceive> SendAsync()
        {
            await base.SendAsync();
            DataReceivedDeserialized = this.Deserializer.Deserialize(base.DataReceivedDecrypted);
            return DataReceivedDeserialized;
        }
    }
}
