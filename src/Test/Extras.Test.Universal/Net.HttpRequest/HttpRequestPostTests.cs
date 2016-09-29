//-----------------------------------------------------------------------
// <copyright file="HttpRequestPostTests.cs" company="Genesys Source">
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
using Genesys.Extras.Collections;
using Genesys.Extras.Net;
using Genesys.Extras.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class HttpRequestPostTests
    {
        [TestMethod()]
        public async Task Net_HttpRequestPost_SendAsync()
        {
            KeyValuePairString dataIn = new KeyValuePairString() { Key = "MyKey", Value = "MyValue" };
            StringMutable dataOut = new StringMutable();
            HttpRequestPost<KeyValuePairString, StringMutable> request = new HttpRequestPost<KeyValuePairString, StringMutable>("http://sampler.dev.getframework.com/Sampler-for-Foundation-WebServices", dataIn);
            dataOut = await request.SendAsync();
            Assert.IsTrue(dataOut.Value.Length > 0, "Did not work");
        }
    }
}
