//-----------------------------------------------------------------------
// <copyright file="ConnectionStringSafe.cs" company="Genesys Source">
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
using Genesys.Extras.Collections;
using Genesys.Extensions;
using System.Reflection;

namespace Genesys.Extras.Configuration
{
    /// <summary>
    /// Container class for connection strings
    /// </summary>
    [CLSCompliant(true)]
    public class ConnectionStringSafe : KeyValuePairString, IFormattable
    {
        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlElements
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string ConnectionStrings = "connectionStrings";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Add = "add";
            /// <summary>
            /// Clear element
            /// </summary>
            public const string Clear = "clear";
            /// <summary>
            /// Remove element
            /// </summary>
            public const string Remove = "remove";
        }

        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlAttributes
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string Key = "name";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Value = "connectionString";
        }
        
        /// <summary>
        /// Types of apps that can consume a .config file
        /// </summary>
        public enum ConnectionStringTypes
        {
            /// <summary>
            /// ADO Data access
            /// </summary>
            ADO = 0,
            /// <summary>
            /// Entity Framework data access
            /// </summary>
            EF = 1
        }

        /// <summary>
        /// Mask of EF connection string
        /// </summary>
        private const string maskEF = @"metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string='{1}'";

        /// <summary>
        /// Mask of EF connection string
        /// </summary>
        private const string maskADO = @"{0}";

        /// <summary>
        /// ConnectionString.
        /// Use .Value instead. This is exclusively for System.Configuration.ConfigurationManager backward compatibility.
        /// </summary>
        public string Connectionstring { get { return this.Value; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionStringSafe() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionStringSafe(KeyValuePairString item) : base(item.Key, item.Value) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public ConnectionStringSafe(string key, string value) : base(key, value) { }
        
        /// <summary>
        /// EDMXFileName
        /// </summary>
        public string EDMXFileName { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// Determines type of connection string: ADO or EF
        /// </summary>
        /// <returns></returns>
        public ConnectionStringTypes ConnectionStringType
        {
            get
            {
                if (base.Value.Contains(".csdl") && base.Value.Contains(".ssdl") && base.Value.Contains(".msl"))
                { return ConnectionStringTypes.EF; } else { return ConnectionStringTypes.ADO; }
            }
        }
        
        /// <summary>
        /// Is an EF connection string?
        /// </summary>
        /// <returns></returns>
        public bool IsEF()
        {
            return (this.ConnectionStringType == ConnectionStringTypes.EF);
        }

        /// <summary>
        /// Is an EF connection string?
        /// </summary>
        /// <returns>True if this is an ADO connection string</returns>
        public bool IsADO()
        {
            return (this.ConnectionStringType == ConnectionStringTypes.ADO);
        }

        /// <summary>
        /// Formats data according to requesting format
        /// For returning ADO-style connection string from EF format: Assumes "provider connection string=" is the last key in the "connectionString=" value
        /// </summary>
        /// <param name="format">EF, ADO</param>
        /// <param name="formatProvider">ICustomFormatter compatible class</param>
        /// <returns>Name field formatted in common combinations (EF, ADO)</returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider != null)
            {
                ICustomFormatter fmt = formatProvider.GetFormat(this.GetType()) as ICustomFormatter;
                if (fmt != null) { return fmt.Format(format, this, formatProvider); }
            }
            switch (format)
            {
                case "EF":
                    if (this.IsEF() == true)
                    { return this.Value; } 
                    else { return String.Format(maskEF, EDMXFileName, this.Value); }
                case "ADO":
                    if (this.IsADO() == true)
                    { return this.Value; } 
                    else {
                        string cleansed = String.Format("{0}{1}", valueField.Value.Replace("\"", "").Replace("&quot;", "").RemoveLast(";"), ";");
                        string beginPhrase = "provider connection string=";
                        return cleansed.SubstringRight(cleansed.Length - (cleansed.IndexOf(beginPhrase) + beginPhrase.Length));
                    }
                default: return base.ToString();
            }
        }

        /// <summary>
        /// Returns ADO-style connection string from EF.
        /// Assumes "provider connection string=" is the last key in the "connectionString=" value
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Converts from ADO/EF raw connection strings to EF format
        /// </summary>
        /// <param name="dataAccessObject">Data access object. This object is used to format the EF filename from dataAccessObject.GetTypeInfo().Namespace</param>
        /// <returns></returns>
        public string ToEF(Type dataAccessObject)
        {
            string returnValue = TypeExtension.DefaultString;

            if (this.IsEF() == false)
            {
                this.EDMXFileName = dataAccessObject.GetTypeInfo().Namespace.Replace(".", TypeExtension.DefaultString);
                returnValue = this.ToString("EF");
            } else
            {
                returnValue = this.Value;
            }

            return returnValue;
        }

        /// <summary>
        /// Converts from EF raw connection strings to ADO format
        /// </summary>
        /// <returns></returns>
        public string ToADO()
        {
            string returnValue = TypeExtension.DefaultString;

            if (this.IsADO() == false)
            {
                returnValue = this.ToString("ADO");
            } else
            {
                returnValue = this.Value;
            }

            return returnValue;
        }
    }
}
