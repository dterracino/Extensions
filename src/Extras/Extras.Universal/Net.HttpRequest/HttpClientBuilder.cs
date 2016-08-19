//-----------------------------------------------------------------------
// <copyright file="HttpClientBuilder.cs" company="Genesys Source">
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
using System.Net.Http;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Builds an HttpClient object
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class HttpClientBuilder : HttpClient
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HttpClientBuilder()
            : this(2560000, new KeyValuePair<String, String>("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)"))
        {            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpClientBuilder(Int64 maxResponseContentBufferSize, KeyValuePair<String, String> header)
            : base()
        {
            base.MaxResponseContentBufferSize = maxResponseContentBufferSize;
            base.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
}
