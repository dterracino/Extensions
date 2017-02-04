//-----------------------------------------------------------------------
// <copyright file="HttpRequestSender.cs" company="Genesys Source">
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
using System.Net.Http;
using System.Threading.Tasks;
using Genesys.Extensions;
using Genesys.Extras.Security.Cryptography;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Communicates via GET, all transmissions String
    /// </summary>
    [CLSCompliant(true)]
    public abstract class HttpRequestClient
    {
        /// <summary>
        /// Raw data received
        /// </summary>
        public string DataReceivedRaw { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// DataReceivedRaw value decrypted
        /// </summary>
        public string DataReceivedDecrypted { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// HttpClient for request
        /// </summary>
        public HttpClientBuilder Client { get; protected set; } = new HttpClientBuilder();

        /// <summary>
        /// Url of request
        /// </summary>
        public string Url { get; protected set; } = TypeExtension.DefaultString;

        /// <summary>
        /// Response after request
        /// </summary>
        public HttpResponseMessage Response { get; protected set; } = new HttpResponseMessage();

        /// <summary>
        /// Specify if want to send plain text with no alterations
        /// </summary>
        public bool SendPlainText { get; protected set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Specify if want to send plain text with no alterations
        /// </summary>
        public bool ThrowExceptionWithEmptyReponse { get; set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Encryptor if plain text is off
        /// </summary>
        public IEncryptor Encryptor { get; protected set; } = new CaesarEncryptor(); // Start with simple cross platform class, allowing more complicated encryption on construction.
        
        /// <summary>
        /// Immutable
        /// </summary>
        protected internal HttpRequestClient() : base()
        {
            SendPlainText = true;
#if (DEBUG)
            ThrowExceptionWithEmptyReponse = true;
#endif
        }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(string url) : this() { Url = url; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(string url, IEncryptor encrptor) : this(url) { Encryptor = Encryptor; }
                
        /// <summary>
        /// Synchronously sends a GET request, Receives string response
        /// </summary>
        /// <returns>Result</returns>
        public abstract string Send();

        /// <summary>
        /// Asynchronously sends a GET request, Receives strongly typed response
        /// </summary>
        /// <returns></returns>
        public abstract Task<string> SendAsync();

        /// <summary>
        /// string format of the request Url
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Url.ToString();
        }
    }
}
