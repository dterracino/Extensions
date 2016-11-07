//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafeTests.cs" company="Genesys Source">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using System.Collections.Specialized;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// ConfigurationManagerSafe Tests
    /// </summary>
    [TestClass()]
    public partial class ConfigurationManagerSafeTests
    {
        /// <summary>
        /// Connection strings in safe version of configuration manager
        /// </summary>
        [TestMethod()]
        public void Configuration_ConfigurationManagerSafe_AppSettings()
        {
            AppSettingSafe ItemToTest = new AppSettingSafe();
            NameValueCollection itemToConvert = ConfigurationManager.AppSettings;
            string[,] configData = new string[itemToConvert.Count, 2];

            for (int count = 0; count < itemToConvert.Count; count++)
            {
                foreach (string itemKey in itemToConvert)
                {
                    configData[count, 0] = itemKey;
                    configData[count, 1] = itemToConvert[count];
                }
            }

            ConfigurationManagerSafe Configuration = new ConfigurationManagerSafe(configData, null);
            ItemToTest = Configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(ItemToTest.Value != TypeExtension.DefaultString, "Did not work");
        }

        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Configuration_ConfigurationManagerSafe_ConnectionStrings()
        {
            ConnectionStringSafe ItemToTest = new ConnectionStringSafe();
            ConnectionStringSettingsCollection itemToConvert = ConfigurationManager.ConnectionStrings;
            string[,] configData = new string[itemToConvert.Count, 2];

            for (int count = 0; count < itemToConvert.Count; count++)
            {
                configData[count, 0] = itemToConvert[count].Name;
                configData[count, 1] = itemToConvert[count].ConnectionString;
            }
            ConfigurationManagerSafe Configuration = new ConfigurationManagerSafe(null, configData);

            ItemToTest = Configuration.ConnectionString("TestADOConnection");
            Assert.IsTrue(ItemToTest.Value != TypeExtension.DefaultString, "Did not work");
            ItemToTest.EDMXFileName = "TestEDMXFile";
            Assert.IsTrue(ItemToTest.ToString("EF") != TypeExtension.DefaultString);
            Assert.IsTrue(ItemToTest.ToEF(this.GetType()) != TypeExtension.DefaultString);

            ItemToTest = Configuration.ConnectionString("TestEFConnection");
            Assert.IsTrue(ItemToTest.Value != TypeExtension.DefaultString, "Did not work");
            Assert.IsTrue(ItemToTest.ToString("ADO") != TypeExtension.DefaultString);
            Assert.IsTrue(ItemToTest.ToADO() != TypeExtension.DefaultString);
        }
    }
}
