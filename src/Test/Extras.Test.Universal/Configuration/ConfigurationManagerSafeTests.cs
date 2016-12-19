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
            AppSettingSafe itemToTest = new AppSettingSafe();
            ConfigurationManagerSafe configuration = ConfigurationManagerSafeTests.Create();

            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString, "Did not work");
        }

        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Configuration_ConfigurationManagerSafe_ConnectionStrings()
        {
            ConnectionStringSafe itemToTest = new ConnectionStringSafe();
            ConfigurationManagerSafe configuration = ConfigurationManagerSafeTests.Create();

            itemToTest = configuration.ConnectionString("TestADOConnection");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString, "Did not work");
            itemToTest.EDMXFileName = "TestEDMXFile";
            Assert.IsTrue(itemToTest.ToString("EF") != TypeExtension.DefaultString);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != TypeExtension.DefaultString);

            itemToTest = configuration.ConnectionString("TestEFConnection");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString, "Did not work");
            Assert.IsTrue(itemToTest.ToString("ADO") != TypeExtension.DefaultString);
            Assert.IsTrue(itemToTest.ToADO() != TypeExtension.DefaultString);
        }

        /// <summary>
        /// Universal cant access ConfigurationManager directly. 
        ///  This method uses the ConfigurationManager to get data, then returns as a cross-platform friendly array
        /// </summary>
        /// <returns></returns>
        public static string[,] AppSettingsGet()
        {
            var itemToConvert = ConfigurationManager.AppSettings ?? new NameValueCollection();
            string[,] returnValue = new string[itemToConvert.Count, 2];

            for (var count = 0; count < itemToConvert.Count; count++)
            {
                returnValue[count, 0] = itemToConvert.Keys[count];
                returnValue[count, 1] = itemToConvert[count];
            }

            return returnValue;
        }

        /// <summary>
        /// Universal cant access ConfigurationManager directly. 
        ///  This method uses the ConfigurationManager to get data, then returns as a cross-platform friendly array
        /// </summary>
        /// <returns></returns>
        public static string[,] ConnectionStringsGet()
        {
            var itemToConvert = ConfigurationManager.ConnectionStrings ?? new ConnectionStringSettingsCollection();
            string[,] returnValue = new string[itemToConvert.Count, 2];

            for (var count = 0; count < itemToConvert.Count; count++)
            {
                returnValue[count, 0] = itemToConvert[count].Name;
                returnValue[count, 1] = itemToConvert[count].ConnectionString;
            }

            return returnValue;
        }

        /// <summary>
        /// Constructs a current instance of .config AppSettings and ConnectionStrings nodes
        /// Universal/Core does not support ConfigurationManager, so have to construct using Universal friendly means
        /// </summary>
        /// <returns></returns>
        public static ConfigurationManagerSafe Create()
        {
            return new ConfigurationManagerSafe(ConfigurationManagerSafeTests.AppSettingsGet(), ConfigurationManagerSafeTests.ConnectionStringsGet());
        }

    }
}
