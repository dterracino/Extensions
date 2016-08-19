//-----------------------------------------------------------------------
// <copyright file="HttpRequestPutString.cs" company="Genesys Source">
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
using System.Net.Http;
using System.Threading.Tasks;
using Genesys.Extensions;
using Genesys.Extras.Security.Cryptography;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Communicates via Put, string in and string out
    /// </summary>
    [CLSCompliant(true)]
    public class HttpRequestPutString : HttpRequestClient
    {
        /// <summary>
        /// DataToSend
        /// </summary>
        public string DataToSend { get; set; } = TypeExtension.DefaultString;
        /// <summary>
        /// Mime content type of data to send
        /// </summary>
        public string ContentType { get; set; } = ContentTypes.Types.Json;

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(string url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(string url, string dataToSend) : this(url) { this.DataToSend = dataToSend; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(string url, string dataToSend, IEncryptor encrptor) : this(url, dataToSend) { this.Encryptor = Encryptor; }
        
        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.Send()
        /// </summary>
        /// <returns></returns>
        public override string Send()
        {
            string returnValue = TypeExtension.DefaultString;
            HttpClientBuilder client = new HttpClientBuilder();
            StringContent data = new StringContent(this.DataToSend, System.Text.Encoding.UTF8, this.ContentType);

            this.Response = client.PutAsync(this.Url, data).Result;
            if (this.Response.IsSuccessStatusCode)
            {
                this.DataReceivedRaw = this.Response.Content.ReadAsStringAsync().Result;
                if (this.SendPlainText == false)
                { this.DataReceivedDecrypted = this.Encryptor.Decrypt(this.DataReceivedRaw); } else { this.DataReceivedDecrypted = this.DataReceivedRaw; }
            }

            return this.DataReceivedDecrypted;
        }

        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.SendAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task<string> SendAsync()
        {
            string returnValue = TypeExtension.DefaultString;
            HttpClientBuilder client = new HttpClientBuilder();
            StringContent data = new StringContent(this.DataToSend, System.Text.Encoding.UTF8, this.ContentType);

            this.Response = await client.PutAsync(this.Url, data);
            if (this.Response.IsSuccessStatusCode)
            {
                this.DataReceivedRaw = await this.Response.Content.ReadAsStringAsync();
                if (this.SendPlainText == false)
                { this.DataReceivedDecrypted = this.Encryptor.Decrypt(this.DataReceivedRaw); } else { this.DataReceivedDecrypted = this.DataReceivedRaw; }
            }

            return this.DataReceivedDecrypted;
        }
    }
}
