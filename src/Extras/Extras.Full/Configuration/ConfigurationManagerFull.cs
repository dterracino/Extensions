//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafeFull.cs" company="Genesys Source">
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Genesys.Extensions;

namespace Genesys.Extras.Configuration
{
    /// <summary>
    /// Simulates the System.Configuration.ConfigurationManager class for local XML files with <appSettings></appSettings> nodes.
    /// </summary>
    [CLSCompliant(true)]
    public class ConfigurationManagerFull : ConfigurationManagerSafe
    {
        /// <summary>
        /// All application settings in the referenced config file
        /// </summary>
        public new static AppSettingList AppSettings
        {
            get
            {
                return new ConfigurationManagerFull().appSettingsField;
            }
        }

        /// <summary>
        /// All connection strings in the referenced config file
        /// </summary>
        public new static ConnectionStringList ConnectionStrings
        {
            get
            {
                return new ConfigurationManagerFull().connectionStringsField;
            }
        }
        /// <summary>
        /// Types of applications that can consume a .config file
        /// </summary>
        public enum ApplicationTypes
        {
            /// <summary>
            /// Http request/response type application
            /// </summary>
            Web = 0,
            /// <summary>
            /// Native application, mobile, desktop, service, etc.
            /// </summary>
            Native = 1
        }

        /// <summary>
        /// ConfigFileWeb
        /// </summary>
        protected const string ConfigFileWeb = @"Web.config";

        /// <summary>
        /// ConfigFileNative
        /// </summary>
        protected const string ConfigFileNative = @"App.config";
        
        /// <summary>
        /// ApplicationType
        /// </summary>
        public ApplicationTypes ApplicationType { get { if (this.PathAndFile.EndsWith(ConfigFileWeb)) { return ApplicationTypes.Web; } else { return ApplicationTypes.Native; } } }

        /// <summary>
        /// PathAndFile
        /// </summary>
        public string PathAndFile { get; protected set; } = TypeExtension.DefaultString;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationManagerFull() : base()
        {
            #if (DEBUG)
                        ThrowException = true;
            #endif
            // Init ConfigurationManagerSafe, except do not use it to load .config XML. It has too many variations
            //    XML .configs are nearing obsolescence. Json .configs are much easier to work with
            PathAndFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            Load();
        }

        /// <summary>
        /// Constructor that accepts ConfigurationManager.AppSettings and ConfigurationManager.ConnectionStrings
        /// </summary>
        /// <param name="appSettings">ConfigurationManager.AppSettings</param>
        /// /// <param name="connectionStrings">ConfigurationManager.ConnectionStrings</param>
        public ConfigurationManagerFull(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings) 
            : base(appSettings.ToArraySafe(), null)
        {
            // ConnectionStringSettingsCollection is difficult to work with and is sealed, so cant extend. Just convert to array manually.
            foreach(ConnectionStringSettings connection in connectionStrings ?? new ConnectionStringSettingsCollection())
            {
                connectionStringsField.Add(connection.Name, connection.ConnectionString);
            }                
        }
        
        /// <summary>
        /// Loads from XML data
        /// </summary>
        public void Load()
        {
            var appSettings = new NameValueCollection();
            var connectionStrings = new ConnectionStringSettingsCollection();
            
            try { appSettings = System.Configuration.ConfigurationManager.AppSettings; }
                catch (NullReferenceException) { if (ThrowException) throw; }
            try { connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings; }
                catch (NullReferenceException) { if (ThrowException) throw; }                        
            foreach (string Item in appSettings)
            {
                appSettingsField.Add(new AppSettingSafe(Item, appSettings.GetValues(Item).FirstOrDefault()));
            }
            foreach (System.Configuration.ConnectionStringSettings Item in connectionStrings)
            {
                connectionStringsField.Add(new ConnectionStringSafe(Item.Name, Item.ConnectionString));
            }
        }
    }
}




