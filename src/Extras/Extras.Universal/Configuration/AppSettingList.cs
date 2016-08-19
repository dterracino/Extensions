//-----------------------------------------------------------------------
// <copyright file="AppSettingList.cs" company="Genesys Source">
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
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Genesys.Extensions;

namespace Genesys.Extras.Configuration
{
    /// <summary>
    /// Generic list of application settings from .config file
    /// </summary>
    [CLSCompliant(true)]
    public class AppSettingList : List<AppSettingSafe>
    {
        /// <summary>
        /// Name of the Xml node element
        /// </summary>
        public const string ElementName = "add";
        /// <summary>
        /// Name of the attribute key inside Xml node
        /// </summary>
        public const string ElementKeyName = "key";
        /// <summary>
        /// Name of the attribute value inside Xml node
        /// </summary>
        public const string ElementValueName = "value";

        private string appSettingsXmlRawField = TypeExtension.DefaultString;
        private XDocument appSettingsXDocField = new XDocument();
        
        /// <summary>
        /// Raw XML document
        /// </summary>
        public XDocument AppSettingsXDoc { get { return appSettingsXDocField; } }
        /// <summary>
        /// Status message
        /// </summary>
        public string StatusMessage { get; set; } = TypeExtension.DefaultString;
        /// <summary>
        /// Setting to allow duplicates
        /// </summary>
        public bool AllowDuplicates { get; set; } = TypeExtension.DefaultBoolean;
        /// <summary>
        /// Setting to throw exception if rules are broken
        /// </summary>
        public bool ThrowException { get; set; } = TypeExtension.DefaultBoolean;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingList() : base()
        {
            this.StatusMessage = "No data loaded.";
#if (DEBUG)
            this.ThrowException = true;
#endif
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlRaw">Raw Xml string containing all data</param>
        public AppSettingList(string xmlRaw) : this()
        {
            this.appSettingsXmlRawField = xmlRaw;
            this.Load();
        }
        
        /// <summary>
        /// Loads all values from config file
        /// </summary>
        private void Load()
        {
            IEnumerable<XElement> elements = new List<XElement>();
            IEnumerable<XAttribute> attributes = new List<XAttribute>();
            try
            {
                // Only act if there is Xml to parse
                if (this.appSettingsXmlRawField != TypeExtension.DefaultString)
                {
                    // Load the document
                    this.appSettingsXDocField = XDocument.Parse(this.appSettingsXmlRawField);
                    // Extract raw data
                    elements = this.appSettingsXDocField.Descendants(AppSettingList.ElementName);
                    var KVPs = elements.Select(x => new {
                        Key = x.Attribute(AppSettingList.ElementKeyName).Value,
                        Value = x.Attribute(AppSettingList.ElementValueName).Value
                    }).ToList();
                    // Fill data structure
                    foreach (var Item in KVPs) { this.Add(new AppSettingSafe(Item.Key, Item.Value)); }
                }
            }
            catch (NullReferenceException)
            {
                if (this.ThrowException == false)
                { this.StatusMessage = "Cannot load. Required elements are not in the file"; } else { throw; }
            }
            finally
            {
                this.StatusMessage = String.Format("{0} records loaded.", this.Count);
            }
        }
        
        /// <summary>
        /// Gets value for a key
        /// </summary>
        /// <param name="key">Key of the element</param>
        /// <remarks></remarks>
        public string GetValue(string key)
        {
            string returnValue = TypeExtension.DefaultString;
            returnValue = this.FindSafe(x => x.Key == key).Value;
            return returnValue;
        }
        
        /// <summary>
        /// Finds the index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int FindIndex(string key)
        {
            int returnValue = TypeExtension.DefaultInteger;
            for (int count = 0; count < this.Count - 1; count++)
            {
                if (this[count].Key == key)
                {
                    returnValue = count;
                    break;
                }
            }
            return returnValue;
        }
        
        /// <summary>
        /// Adds float item, maintaining identity key
        /// </summary>
        /// <param name="itemToAdd"></param>
        public new void Add(AppSettingSafe itemToAdd)
        {
            // Check for ID
            List<AppSettingSafe> conflictingItems = this.FindAll(x => x.Key == itemToAdd.Key);
            if (this.AllowDuplicates == false && this.ThrowException == true && conflictingItems.Count > 0)
                throw new System.IndexOutOfRangeException("Unable to add new items, Identity Key conflict.");
            base.Add(itemToAdd);
        }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void Add(string key, string value)
        {
            // Self-normalize based on this.AllowDuplicates and this.ThrowException
            if (this.AllowDuplicates == false && this.ThrowException == false && this.GetValue(key) != TypeExtension.DefaultString)
            {
                this.RemoveAt(this.FindIndex(key));
            }
            base.Add(new AppSettingSafe(key, value));
        }
        
        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="key"></param>
        /// <remarks></remarks>
        public void Remove(string key)
        {
            if (this.GetValue(key).ToStringSafe() != TypeExtension.DefaultString)
                this.RemoveAt(this.FindIndex(key));
        }
    }
}
