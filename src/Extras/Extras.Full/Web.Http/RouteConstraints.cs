//-----------------------------------------------------------------------
// <copyright file="NoParametersRouteConstraint.cs" company="Genesys Source">
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
using System.Web;
using System.Web.Routing;
using Genesys.Extensions;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// Restricts http verbs to route that has the specified parameter (like id) in the route
    /// Usage: public static void RegisterRoutes(RouteCollection routes)
    ///         {routes.MapHttpRoute("DefaultApi","api/{controller}/{id}",
    ///             new { id = RouteParameter.Optional},
    ///             new { id = new NoParameterRouteConstraint() } );}
    /// </summary>
    public class ParameterNotAllowed : IRouteConstraint
    {
        string httpMethod = TypeExtension.DefaultString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method"></param>
        public ParameterNotAllowed(string method)
        {
            httpMethod = method;
        }

        /// <summary>
        /// Match criteria
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        /// <param name="routeName">Route name</param>
        /// <param name="parameterName">ParameterName, typically ID</param>
        /// <param name="valueList">List of dictionary values</param>
        /// <param name="routeDirection">Direction of the route</param>
        /// <returns></returns>
        public bool Match(HttpContextBase httpContext, Route routeName, string parameterName, RouteValueDictionary valueList, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest && httpContext.Request.HttpMethod == httpMethod && valueList[parameterName] != null)
            {
                return false;
            }

            return true;
        }
    }
}
