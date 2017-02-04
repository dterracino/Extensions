//-----------------------------------------------------------------------
// <copyright file="HttpVerbSender.cs" company="Genesys Source">
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
using System.Threading.Tasks;
using Genesys.Extensions;
using Genesys.Extras.Net;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Handles sending HttpRequests to endpoints and receiving a response
    /// </summary>
    public abstract class HttpVerbSender
    {
        /// <summary>
        /// Send is about to begin
        /// </summary>
        public event SendBeginEventHandler SendBegin;

        /// <summary>
        /// Send is complete
        /// </summary>
        public event SendBeginEventHandler SendEnd;

        /// <summary>
        /// OnSendBegin()
        /// </summary>
        protected virtual void OnSendBegin() { if (SendBegin != null) { SendBegin(this, EventArgs.Empty); } }

        /// <summary>
        /// OnSendEnd()
        /// </summary>
        protected virtual void OnSendEnd() { if (SendEnd != null) { SendEnd(this, EventArgs.Empty); } }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        protected HttpVerbSender()
            : base()
        {
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<string> SendGetAsync(string fullUrl)
        {
            OnSendBegin();
            var returnValue = TypeExtension.DefaultString;
            HttpRequestGetString request = new HttpRequestGetString(fullUrl);
            returnValue = await request.SendAsync();
            OnSendEnd();

            return returnValue;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataOut> SendGetAsync<TDataOut>(string fullUrl) where TDataOut : new()
        {
            OnSendBegin();
            TDataOut returnValue = default(TDataOut);
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
            TDataOut returnValue = default(TDataOut);
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
            TDataOut returnValue = default(TDataOut);
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
            TDataOut returnValue = default(TDataOut);
            HttpRequestDelete<TDataOut> request = new HttpRequestDelete<TDataOut>(fullUrl);
            returnValue = await request.SendAsync();
            OnSendEnd();
            return returnValue;
        }

        /// <summary>
        /// Workflow beginning. No custom to return.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Arguments passed to the event handler</param>
        public delegate void SendBeginEventHandler(object sender, EventArgs e);
    }
}
