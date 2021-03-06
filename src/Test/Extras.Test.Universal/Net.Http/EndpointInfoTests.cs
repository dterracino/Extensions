//-----------------------------------------------------------------------
// <copyright file="EndpointInfoTests.cs" company="Genesys Source">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Genesys.Extras.Net;
using Genesys.Extras.Text;
using Genesys.Extras.Configuration;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class EndpointInfoTests
    {
        [TestMethod()]
        public void Net_Http_EndpointInfo()
        {
            var controller = "HomeApi";
            ConfigurationManagerSafe configuration = ConfigurationManagerSafeTests.Create();
            StringMutable testData = "Hello world";
            EndpointInfo<StringMutable> endpoint = new EndpointInfo<StringMutable>(configuration.AppSettingValue("MyWebService"), controller, -1, testData, testData);
            Assert.IsTrue(endpoint.GetEndpoint.Url.Contains("http"), "Did not work");
            Assert.IsTrue(endpoint.GetEndpoint.Url.Contains(controller), "Did not work");
        }
    }
}
