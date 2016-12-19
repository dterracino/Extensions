//-----------------------------------------------------------------------
// <copyright file="HttpRequestDeleteTests.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class HttpRequestDeleteTests
    {
        [TestMethod()]
        public async Task Net_HttpRequestDeleteString_SendAsync()
        {
            var dataOut = TypeExtension.DefaultString;
            ConfigurationManagerSafe configuration = ConfigurationManagerSafeTests.Create();
            HttpRequestDeleteString request = new HttpRequestDeleteString(configuration.AppSettingValue("MyWebService") + "/HomeApi");
            dataOut = await request.SendAsync();
            Assert.IsTrue(request.Response.IsSuccessStatusCode == true, "Did not work");
        }

        [TestMethod()]
        public async Task Net_HttpRequestDelete_SendAsync()
        {
            object dataOut;
            ConfigurationManagerSafe configuration = ConfigurationManagerSafeTests.Create();
            HttpRequestDelete<object> request = new HttpRequestDelete<object>(configuration.AppSettingValue("MyWebService") + "/HomeApi");
            dataOut = await request.SendAsync();
            Assert.IsTrue(request.Response.IsSuccessStatusCode == true, "Did not work");
        }
    }
}
