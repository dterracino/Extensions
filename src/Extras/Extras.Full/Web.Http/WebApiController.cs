//-----------------------------------------------------------------------
// <copyright file="WebApiControllerBase.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
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
using System.Web.Http;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;
using System.Threading.Tasks;
using Genesys.Extensions;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// WebAPI controller for any app services project for native app back ends
    /// </summary>
    public class WebApiController : ApiController
    {
        /// <summary>
        /// Standard return message for default route of services
        /// </summary>
        public const string MessageUpAndRunning = "Services up and running...";

        private ConfigurationManagerFull configurationManager = new ConfigurationManagerFull();

        /// <summary>
        /// Send is about to begin
        /// </summary>
        public event SendBeginEventHandler SendBegin;

        /// <summary>
        ///  Send has ended
        /// </summary>
        public event SendBeginEventHandler SendEnd;

        /// <summary>
        /// Worker for SendBegin event
        /// </summary>
        protected virtual void OnSendBegin() { if (SendBegin != null) { SendBegin(this, EventArgs.Empty); } }

        /// <summary>
        /// Worker for SendEnd event
        /// </summary>
        protected virtual void OnSendEnd() { if (SendEnd != null) { SendEnd(this, EventArgs.Empty); } }

        /// <summary>
        /// Middle-tier web service supporting this application
        /// </summary>  
        public Uri MyWebService { get { return new UrlInfo(configurationManager.AppSetting("MyWebService").Value).Uri; } }
        
        /// <summary>
        /// Parameterless constructor
        /// </summary>
        protected WebApiController() : base() { }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataOut> SendGetAsync<TDataOut>(string fullUrl) where TDataOut : new()
        {
            OnSendBegin();
            TDataOut returnValue =  TypeExtension.InvokeConstructorOrDefault<TDataOut>();
            HttpRequestGet<TDataOut> request = new HttpRequestGet<TDataOut>(fullUrl);
            returnValue = await request.SendAsync();
            OnSendEnd();

            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataOut> SendPostAsync<TDataIn, TDataOut>(string fullUrl, TDataIn itemToSend)
        {
            OnSendBegin();
            TDataOut returnValue =  TypeExtension.InvokeConstructorOrDefault<TDataOut>();
            HttpRequestPost<TDataIn, TDataOut> request = new HttpRequestPost<TDataIn, TDataOut>(fullUrl, itemToSend);
            returnValue = await request.SendAsync();
            OnSendEnd();

            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataOut> SendPutAsync<TDataIn, TDataOut>(string fullUrl, TDataIn itemToSend)
        {
            OnSendBegin();
            TDataOut returnValue =  TypeExtension.InvokeConstructorOrDefault<TDataOut>();
            HttpRequestPut<TDataIn, TDataOut> request = new HttpRequestPut<TDataIn, TDataOut>(fullUrl, itemToSend);
            returnValue = await request.SendAsync();
            OnSendEnd();
            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataOut> SendDeleteAsync<TDataOut>(string fullUrl) where TDataOut : new()
        {
            OnSendBegin();
            TDataOut returnValue =  TypeExtension.InvokeConstructorOrDefault<TDataOut>();
            HttpRequestDelete<TDataOut> request = new HttpRequestDelete<TDataOut>(fullUrl);
            returnValue = await request.SendAsync();
            OnSendEnd();
            return returnValue;
        }

        /// <summary>
        /// Workflow beginning. No custom to return.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event arguments</param>
        public delegate void SendBeginEventHandler(object sender, EventArgs e);
    }
}
