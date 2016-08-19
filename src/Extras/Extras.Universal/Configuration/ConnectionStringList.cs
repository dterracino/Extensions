//-----------------------------------------------------------------------
// <copyright file="ConnectionStringList.cs" company="Genesys Source">
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
using Genesys.Extras.Text;

namespace Genesys.Extras.Configuration
{
    /// <summary>
    /// Generic list of connection strings
    /// </summary>
    [CLSCompliant(true)]
    public class ConnectionStringList : List<ConnectionStringSafe>
    {
        /// <summary>
        /// Name of the Xml node element
        /// </summary>
        public const string ElementName = "add";
        /// <summary>
        /// Name of the attribute key inside Xml node
        /// </summary>
        public const string ElementKeyName = "name";
        /// <summary>
        /// Name of the attribute value inside Xml node
        /// </summary>
        public const string ElementValueName = "connectionString";
        
        private string configStringsXmlRawField = TypeExtension.DefaultString;
        private XDocument configStringsXDocField = new XDocument();
        
        /// <summary>
        /// Raw XML document
        /// </summary>
        public XDocument RawXml { get { return configStringsXDocField; } }
        /// <summary>
        /// StatusMessage
        /// </summary>
        public string StatusMessage { get; set; } = TypeExtension.DefaultString;
        /// <summary>
        ///      AllowDuplicates
        /// </summary>
        public bool AllowDuplicates { get; set; } = TypeExtension.DefaultBoolean;
        /// <summary>
        /// ThrowException
        /// </summary>
        public bool ThrowException { get; set; } = TypeExtension.DefaultBoolean;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionStringList() : base()
        {
            this.StatusMessage = "No data loaded.";
#if (DEBUG)
            this.ThrowException = true;
#endif
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlRaw"></param>
        public ConnectionStringList(string xmlRaw) : this()
        {
            this.configStringsXmlRawField = xmlRaw;
            this.Load();
        }
        
        /// <summary>
        /// Loads all values from configuration file
        /// </summary>
        private void Load()
        {
            IEnumerable<XElement> elements = new List<XElement>();
            IEnumerable<XAttribute> attributes = new List<XAttribute>();
            try
            {
                // Only act if there is Xml to parse
                if (this.configStringsXmlRawField != TypeExtension.DefaultString)
                {
                    // Load the document
                    this.configStringsXDocField = XDocument.Parse(this.configStringsXmlRawField);
                    // Extract raw data
                    elements = this.configStringsXDocField.Descendants(ConnectionStringList.ElementName);
                    var KVPs = elements.Select(x => new {
                            Key = x.Attribute(ConnectionStringList.ElementKeyName).Value,
                            Value = x.Attribute(ConnectionStringList.ElementValueName).Value
                        }).ToList();
                    // Fill
                    foreach (var Item in KVPs) { this.Add(new ConnectionStringSafe(Item.Key, Item.Value)); }
                }
            }
            catch (NullReferenceException)
            {
              if (this.ThrowException == false)
                { this.StatusMessage = String.Format("Cannot load. Required elements are not present."); } 
              else { throw; }
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
        public StringMutable GetValue(StringMutable key)
        {
            StringMutable returnValue = new StringMutable();
            var x1 = key == "MyDataConnection" ? true : false;
            returnValue = this.FindSafe(x => x.Key == key.ToString()).Value;
            string key2 = key;
            returnValue = this.FindSafe(x => x.Key == key2).Value;
            returnValue = this.FindSafe(x => x.Key == key).Value;
            return returnValue;
        }
        
        /// <summary>
        /// Finds the index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int FindIndex(StringMutable key)
        {
            int returnValue = TypeExtension.DefaultInteger;
            for (int Count = 0; Count < this.Count - 1; Count++)
            {
                if (this[Count].Key == key)
                {
                    returnValue = Count;
                    break;
                }
            }
            return returnValue;
        }
        
        /// <summary>
        /// Adds float item, maintaining identity key
        /// </summary>
        /// <param name="itemToAdd"></param>
        public new void Add(ConnectionStringSafe itemToAdd)
        {
            // Check for ID clear-age
            List<ConnectionStringSafe> ConflictingItems = this.FindAll(x => x.Key == itemToAdd.Key);
            if (this.AllowDuplicates == false && this.ThrowException == true && ConflictingItems.Count > 0)
                throw new System.IndexOutOfRangeException("Unable to add new items, Identity Key conflict.");
            base.Add(itemToAdd);
        }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void Add(StringMutable key, StringMutable value)
        {
            // Self-normalize based on this.AllowDuplicates and this.ThrowException
            if (this.AllowDuplicates == false && this.ThrowException == false && this.GetValue(key) != TypeExtension.DefaultString)
            {
                this.RemoveAt(this.FindIndex(key));
            }
            base.Add(new ConnectionStringSafe() { Key = key, Value = value });
        }
        
        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="key"></param>
        /// <remarks></remarks>
        public void Remove(StringMutable key)
        {
            if (this.GetValue(key).ToStringSafe() != TypeExtension.DefaultString)
                this.RemoveAt(this.FindIndex(key));
        }
    }
}
