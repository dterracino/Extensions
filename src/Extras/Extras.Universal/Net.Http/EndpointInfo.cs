//-----------------------------------------------------------------------
// <copyright file="EndpointInfo.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
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
using Genesys.Extras.Net;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Metadata on endpoints that follow the Web API convention
    /// Assumes that all data in and out are the same type.
    /// </summary>
    /// <typeparam name="TDataInOut">Type of data transmitted in the requests/responses</typeparam>
    public class EndpointInfo<TDataInOut> where TDataInOut : new()
    {
        /// <summary>
        /// Endpoint for HttpGet requests
        /// </summary>
        public HttpGetEndpoint<TDataInOut> GetEndpoint;

        /// <summary>
        /// Endpoint for HttpPut requests
        /// </summary>
        public HttpPutEndpoint<TDataInOut> PutEndpoint;

        /// <summary>
        /// Endpoint for HttpPost requests
        /// </summary>
        public HttpPostEndpoint<TDataInOut> PostEndpoint;

        /// <summary>
        /// Endpoint for HttpDelete requests
        /// </summary>
        public HttpDeleteEndpoint<TDataInOut> DeleteEndpoint;

        /// <summary>
        /// Constructs the main Http verb endpoints for the passed controller
        /// </summary>
        /// <param name="urlRoot">Url path to the root of the Web API service</param>
        /// <param name="controllerName">Controller accepting requests for all Http verbs</param>
        /// <param name="id">ID of the HttpGet and HttpDelete request</param>
        /// <param name="putData">Data for the HttpPut request</param>
        /// <param name="postData">Data for the HttpPut request</param>
        public EndpointInfo(string urlRoot, string controllerName, int id, TDataInOut putData, TDataInOut postData) 
        {
            GetEndpoint = new HttpGetEndpoint<TDataInOut>(urlRoot, controllerName, id);
            PutEndpoint = new HttpPutEndpoint<TDataInOut>(urlRoot, controllerName, putData);
            PostEndpoint = new HttpPostEndpoint<TDataInOut>(urlRoot, controllerName, postData);
            DeleteEndpoint = new HttpDeleteEndpoint<TDataInOut>(urlRoot, controllerName, id);
        }

        /// <summary>
        /// Constructs only the HttpGet endpoint, for complex parameters or format
        /// </summary>
        /// <param name="fullUrlWithParameters">Url path with parameter for the HttpGet endpoint only</param>
        public EndpointInfo(string fullUrlWithParameters)
        {
            GetEndpoint = new HttpGetEndpoint<TDataInOut>(fullUrlWithParameters);
            PutEndpoint = new HttpPutEndpoint<TDataInOut>(fullUrlWithParameters);
            PostEndpoint = new HttpPostEndpoint<TDataInOut>(fullUrlWithParameters);
            DeleteEndpoint = new HttpDeleteEndpoint<TDataInOut>(fullUrlWithParameters);
        }
    }

    /// <summary>
    /// Endpoint accepting HttpGet requests
    /// </summary>
    public class HttpGetEndpoint<TDataOut> : HttpRequestGet<TDataOut> where TDataOut : new()
    {
        /// <summary>
        /// Constructs a default route style HttpGet endpoint
        /// Default convention: urlRoot/controllerName/id
        /// </summary>
        /// <param name="urlRoot">Url path to the root of the Web API service</param>
        /// <param name="controllerName">Controller accepting requests</param>
        /// <param name="id">ID of the request</param>
        /// <param name="urlMask">Mask to be used to form the endpoint url</param>
        public HttpGetEndpoint(string urlRoot, string controllerName, int id, string urlMask = "{0}/{1}/{2}") 
            : base(String.Format(urlMask, urlRoot, controllerName, id))
        { }

        /// <summary>
        /// Constructs a default route style HttpGet endpoint
        /// Default convention: urlRoot/controllerName/id
        /// </summary>
        /// <param name="fullUrlWithParameters">Fully formed url to the endpoint</param>
        public HttpGetEndpoint(string fullUrlWithParameters) : base(fullUrlWithParameters)
        {
            Url = fullUrlWithParameters;
        }
    }

    /// <summary>
    /// Endpoint accepting HttpPut requests
    /// Default convention: urlRoot/controllerName
    /// </summary>
    public class HttpPutEndpoint<TDataInOut> : HttpRequestPut<TDataInOut> where TDataInOut : new()
    {
        private const string urlMask = "{0}/{1}";

        /// <summary>
        /// Constructs a default route style HttpGet endpoint
        /// Default convention: urlRoot/controllerName
        /// </summary>
        /// <param name="urlRoot">Url path to the root of the Web API service</param>
        /// <param name="controllerName">Controller accepting requests</param>
        /// <param name="dataIn">Data to be sent in the request</param>
        public HttpPutEndpoint(string urlRoot, string controllerName, TDataInOut dataIn)
            : base(String.Format(HttpPutEndpoint<TDataInOut>.urlMask, urlRoot, controllerName), dataIn)
        { }

        /// <summary>
        /// Constructs a default route style HttpGet endpoint
        /// Default convention: urlRoot/controllerName/id
        /// </summary>
        /// <param name="fullUrlWithParameters">Fully formed url to the endpoint</param>
        public HttpPutEndpoint(string fullUrlWithParameters) : base(fullUrlWithParameters)
        {
            Url = fullUrlWithParameters;
        }
    }

    /// <summary>
    /// Endpoint accepting HttpPost requests
    /// Default convention: urlRoot/controllerName
    /// </summary>
    public class HttpPostEndpoint<TDataInOut> : HttpRequestPost<TDataInOut> where TDataInOut : new()
    {
        private const string urlMask = "{0}/{1}";

        /// <summary>
        /// Constructs a default route style HttpGet endpoint
        /// Default convention: urlRoot/controllerName
        /// </summary>
        /// <param name="urlRoot">Url path to the root of the Web API service</param>
        /// <param name="controllerName">Controller accepting requests</param>
        /// <param name="dataIn">Data to be sent in the request</param>
        public HttpPostEndpoint(string urlRoot, string controllerName, TDataInOut dataIn)
            : base(String.Format(HttpPostEndpoint<TDataInOut>.urlMask, urlRoot, controllerName), dataIn)
        { }

        /// <summary>
        /// Constructs a default route style HttpGet endpoint
        /// Default convention: urlRoot/controllerName/id
        /// </summary>
        /// <param name="fullUrlWithParameters">Fully formed url to the endpoint</param>
        public HttpPostEndpoint(string fullUrlWithParameters) : base(fullUrlWithParameters)
        {
            Url = fullUrlWithParameters;
        }
    }

    /// <summary>
    /// Endpoint accepting HttpDelete requests
    /// </summary>
    public class HttpDeleteEndpoint<TDataOut> : HttpRequestDelete<TDataOut> where TDataOut : new()
    {
        /// <summary>
        /// Constructs a default route style HttpDelete endpoint
        /// Default convention: urlRoot/controllerName/id
        /// </summary>
        /// <param name="urlRoot">Url path to the root of the Web API service</param>
        /// <param name="controllerName">Controller accepting requests</param>
        /// <param name="id">ID of the request</param>
        /// <param name="urlMask">Mask to be used to form the endpoint url</param>
        public HttpDeleteEndpoint(string urlRoot, string controllerName, int id, string urlMask = "{0}/{1}/{2}")
            : base(String.Format(urlMask, urlRoot, controllerName, id))
        { }

        /// <summary>
        /// Constructs a default route style HttpDelete endpoint
        /// </summary>
        /// <param name="fullUrlWithParameters">Fully formed url to the endpoint</param>
        public HttpDeleteEndpoint(string fullUrlWithParameters) : base(fullUrlWithParameters)
        {
            Url = fullUrlWithParameters;
        }
    }
}
