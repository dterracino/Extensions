//-----------------------------------------------------------------------
// <copyright file="UrlInfo.cs" company="Genesys Source">
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

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Encapsulates common Uri and Routing components
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class UrlInfo : UrlBuilder
    {        
        /// <summary>
        /// DisplayName of this Url
        /// </summary>
        public string Name { get; set; } = TypeExtension.DefaultString;
        /// <summary>
        /// RootValue
        /// </summary>
        private string RootValue = TypeExtension.DefaultString;
        /// <summary>
        /// Root
        /// </summary>
        public string Root { get { return RootValue; } protected set { RootValue = value.RemoveLast("/"); } }
        /// <summary>
        /// Controller
        /// </summary>
        public string Controller { get; protected set; } = TypeExtension.DefaultString;
        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; protected set; } = TypeExtension.DefaultString;
        /// <summary>
        /// Route
        /// </summary>
        public string Route { get { return Path.RemoveFirst("/").RemoveLast("/"); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlInfo() : base() { }
        /// <summary>
        /// Constructor
        /// </summary>
        public UrlInfo(string rootUrl, string path) : base(rootUrl, path) { }
        /// <summary>
        /// Constructor tuned for MVC pattern
        /// </summary>
        public UrlInfo(string rootUrl, string controller, string action) : base(String.Format("{0}/{1}/{2}", rootUrl.RemoveLast("/"), controller, action))
        {
            Root = rootUrl;
            Controller = controller;
            Action = action;
        }
        /// <summary>
        /// Constructor tuned for MVC pattern
        /// </summary>
        public UrlInfo(string fullUrl) : base(fullUrl) { }
        
    }    
}
