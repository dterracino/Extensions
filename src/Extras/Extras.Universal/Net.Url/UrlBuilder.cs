//-----------------------------------------------------------------------
// <copyright file="UrlBuilder.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Extras.Text.Encoding;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Builds URLs similary to and dependent on the UriBuilder class
    /// Uri Layout: [scheme]://[user]:[password]@[host/authority]:[port]/[path];[params]?[querystring]#[fragment]
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class UrlBuilder : UriBuilder
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string fullUrl) : base(fullUrl) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string rootUrl, string path) : this(rootUrl.RemoveLast("/") + "/" +  path.RemoveLast("/")) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string rootUrl, string controller, string action) : this(UrlBuilder.Format(rootUrl, controller, action)) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlBuilder(string rootUrl, string controller, string action, string parametersWithNoLeadingQuestionMark) : this(rootUrl.RemoveLast("/"), controller, action)
        {
            this.Query = parametersWithNoLeadingQuestionMark;
        }
        
        /// <summary>
        /// handles URL formation issues
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string returnValue = TypeExtension.DefaultString;
            try
            {
                returnValue = base.ToString();
            }
            catch
            {
                returnValue = "http://UnableToformURL";
            }
            return returnValue;
        }
        
        /// <summary>
        /// Formats full URL based on Mvc pattern segments
        /// </summary>
        /// <param name="rootUrl"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Format(string rootUrl, string controller, string action)
        {
            return String.Format("{0}/{1}/{2}", rootUrl.RemoveLast("/"), controller, action);
        }
        
        /// <summary>
        /// Encodes to URL friendly
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Encode(string originalString)
        {
            return UrlEncoder.Encode(originalString);
        }

        /// <summary>
        /// Decodes to URL friendly
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Decode(string originalString)
        {
            return UrlEncoder.Decode(originalString);
        }
    }
}
