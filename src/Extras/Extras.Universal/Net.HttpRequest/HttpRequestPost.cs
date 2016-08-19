//-----------------------------------------------------------------------
// <copyright file="HttpRequestPost.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Extras.Security.Cryptography;
using Genesys.Extras.Serialization;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Communicates via GET
    /// </summary>
    [CLSCompliant(true)]
    public class HttpRequestPost<TDataIn, TDataOut> : HttpRequestPostString
    {
        /// <summary>
        /// Data to send, initialized with default()
        /// </summary>
        protected TDataIn dataToSendStrong =  TypeExtension.InvokeConstructorOrDefault<TDataIn>();
        /// <summary>
        /// Data to send, initialized with default()
        /// </summary>
        protected TDataOut dataReceivedStrong =  TypeExtension.InvokeConstructorOrDefault<TDataOut>();

        /// <summary>
        /// Serializes data going to the endpoint
        /// </summary>
        public ISerializer<TDataIn> Serializer { get; protected set; } = new JsonSerializer<TDataIn>();
        /// <summary>
        /// Serializes data going to the endpoint
        /// </summary>
        public ISerializer<TDataOut> Deserializer { get; protected set; } = new JsonSerializer<TDataOut>();
        /// <summary>
        /// KnownTypes assist the serializer with types that cannot be mapped by default
        /// </summary>
        public List<Type> KnownTypes { get; protected set; } = new List<Type>();

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPost(string url, TDataIn dataToSend) : base(url) { this.dataToSendStrong = dataToSend; base.DataToSend = this.Serializer.Serialize(dataToSend); }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPost(string url, TDataIn dataToSend, IEncryptor encryptor) : this(url, dataToSend) { this.Encryptor = encryptor; }
        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPost(string url, TDataIn dataToSend, List<Type> knownTypes) : this(url, dataToSend) { this.KnownTypes = knownTypes; }
        
        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public new TDataOut Send()
        {
            base.DataReceivedDecrypted = base.Send();
            dataReceivedStrong = this.Deserializer.Deserialize(base.DataReceivedDecrypted);
            return dataReceivedStrong;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public new async Task<TDataOut> SendAsync()
        {
            base.DataReceivedDecrypted = await base.SendAsync();
            dataReceivedStrong = this.Deserializer.Deserialize(base.DataReceivedDecrypted);
            return dataReceivedStrong;
        }
    }
}
