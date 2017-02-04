//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafeTests.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Extras.Configuration;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public partial class ConfigurationManagerFullTests
    {
        [TestMethod()]
        public void Configuration_ConfigurationManagerFull_AppSettings()
        {
            var itemToTestString = TypeExtension.DefaultString;
            AppSettingSafe itemToTest = new AppSettingSafe();
            ConfigurationManagerSafe configuration = new ConfigurationManagerFull();
            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString, "Did not work");
            itemToTestString = ConfigurationManagerFull.AppSettings.GetValue("TestAppSetting");
            Assert.IsTrue(itemToTestString != TypeExtension.DefaultString, "Did not work");
        }

        [TestMethod()]
        public void Configuration_ConfigurationManagerFull_ConnectionStrings()
        {
            ConnectionStringSafe itemToTest = new ConnectionStringSafe();
            ConfigurationManagerSafe configuration = new ConfigurationManagerFull();
            itemToTest = configuration.ConnectionString("TestEFConnection");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString, "Did not work");
            Assert.IsTrue(itemToTest.IsEF(), "Did not work");
            itemToTest = configuration.ConnectionString("TestADOConnection");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString, "Did not work");
            Assert.IsTrue(itemToTest.IsADO(), "Did not work");
        }        
    }
}
