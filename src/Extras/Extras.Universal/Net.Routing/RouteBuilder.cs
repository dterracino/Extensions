//-----------------------------------------------------------------------
// <copyright file="RouteBuilder.cs" company="Genesys Source">
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
using System;
using Genesys.Extensions;
using Genesys.Extras.Collections;
using Genesys.Extras.Text;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Builds URLs similary to and dependent on the UriBuilder class
    /// Uri Layout: [scheme]://[user]:[password]@[host/authority]:[port]/[path];[params]?[querystring]#[fragment]
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class RouteBuilder
    {       
        private string routePrefix = TypeExtension.DefaultString; // Route Prefix for controller class
        private string routeVersion = TypeExtension.DefaultString; // Optional route version to be appended to controller route prefix. I.e. /OAuth/v2/Authorize (Prefix=OAuth, Action=Authorize, Version inserted inbetween)
        private string routeSuffix = TypeExtension.DefaultString; // Route mask for controller action method. Includes any hand-typed parameters
        private KeyValueListSafe<StringMutable, StringMutable> ParameterNameAndMask = new KeyValueListSafe<StringMutable, StringMutable>(); // List of KVPs for /{Variable}/ActionMask
        private bool overrideAllPrefix { get; set; } = TypeExtension.DefaultBoolean;
        
        /// <summary>
        /// Constructor
        /// </summary>
        private RouteBuilder() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public RouteBuilder(string rullRouteMask) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public RouteBuilder(string routePrefix, string routeSuffix) { this.routePrefix = routePrefix; this.routeSuffix = routeSuffix; }
        
        /// <summary>
        /// handles URL formation issues
        /// </summary>
        /// <returns>Returns Url as string</returns>
        public override string ToString()
        {
            var returnValue = TypeExtension.DefaultString;
            // Default to Prefix/Suffix, until expand class to be dynamic based on IFormattable and logic per data condition
            returnValue = RouteBuilder.Format(routePrefix, routeSuffix);
            return returnValue;
        }
        
        /// <summary>
        /// Formats full URL based on Mvc pattern segments
        /// </summary>
        /// <param name="routePrefix">Prefix of route</param>
        /// <param name="routeSuffix">Suffix of route</param>
        /// <returns>Prefix/Suffix formed route</returns>
        public static string Format(string routePrefix, string routeSuffix)
        {
            return String.Format("{0}/{1}", routePrefix, routeSuffix);
        }                
    }    
}
