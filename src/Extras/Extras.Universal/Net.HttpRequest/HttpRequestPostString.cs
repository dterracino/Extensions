//-----------------------------------------------------------------------
// <copyright file="HttpRequestPostString.cs" company="Genesys Source">
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
    /// Communicates via POST, string in and string out
    /// </summary>
    [CLSCompliant(true)]
    public class HttpRequestPostString : HttpRequestClient
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
        public HttpRequestPostString(string url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPostString(string url, string dataToSend) : this(url) { DataToSend = dataToSend; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPostString(string url, string dataToSend, IEncryptor encrptor) : this(url, dataToSend) { Encryptor = Encryptor; }
        
        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.Send()
        /// </summary>
        /// <returns></returns>
        public override string Send()
        {
            var returnValue = TypeExtension.DefaultString;
            var client = new HttpClientBuilder();
            var data = new StringContent(DataToSend, System.Text.Encoding.UTF8, this.ContentType);

            Response = client.PostAsync(this.Url, data).Result;
            if (this.Response.IsSuccessStatusCode)
            {
                DataReceivedRaw = this.Response.Content.ReadAsStringAsync().Result;
                if (SendPlainText == false)
                { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } else { DataReceivedDecrypted = DataReceivedRaw; }
            }

            return DataReceivedDecrypted;
        }

        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.SendAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task<string> SendAsync()
        {
            var returnValue = TypeExtension.DefaultString;
            var client = new HttpClientBuilder();
            var data = new StringContent(DataToSend, System.Text.Encoding.UTF8, this.ContentType);

            Response = await client.PostAsync(this.Url, data);
            if (this.Response.IsSuccessStatusCode)
            {
                DataReceivedRaw = await this.Response.Content.ReadAsStringAsync();
                if (SendPlainText == false)
                { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } else { DataReceivedDecrypted = DataReceivedRaw; }
            }

            return DataReceivedDecrypted;
        }
    }
}
