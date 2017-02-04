//-----------------------------------------------------------------------
// <copyright file="HttpRequestExtension.cs" company="Genesys Source">
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
using System.Net;

namespace Genesys.Extensions
{
    /// <summary>
    /// HttpRequestBaseExtension
    /// </summary>
    [CLSCompliant(true)]
    public static class HttpWebRequestExtension
    {
        /// <summary>
        /// Finds the root of the URL in format: HTTP://SERVER_NAME:SERVER_PORT
        /// </summary>
        /// <param name="item">Item to parse</param>
        /// <returns>Url from item</returns>
        public static string TryParseUrl(this HttpWebRequest item)
        {
            return item.RequestUri.AbsolutePath;
        }
        
        /// <summary>
        /// Checks for HTTPS, or returns true if ://localhost
        /// </summary>
        /// <param name="item">Item to parse</param>
        /// <returns>True if secured</returns>
        public static bool IsSecured(this HttpWebRequest item)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            if (item.IsSecured() | item.RequestUri.ToString().ToString().Contains("://localhost"))
            {
                returnValue = true;
            }

            return returnValue;
        }               
    }
}
