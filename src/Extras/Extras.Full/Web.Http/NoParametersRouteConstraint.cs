//-----------------------------------------------------------------------
// <copyright file="NoParametersRouteConstraint.cs" company="Genesys Source">
//      Copyright (c) 2016 Genesys Source. All rights reserved.
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
using System.Web;
using System.Web.Routing;
using Genesys.Extensions;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// Restricts parameters with post
    /// Usage: public static void RegisterRoutes(RouteCollection routes)
    ///{routes.MapHttpRoute("DefaultApi","api/{controller}/{id}",new { id = RouteParameter.Optional},
    ///        new { id = new NoParameterRouteConstraint() } );}
    /// </summary>
    public class NoParameterRouteConstraint : IRouteConstraint
    {
        private string httpMethod = TypeExtension.DefaultString;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpMethod"></param>
        public NoParameterRouteConstraint(string httpMethod = "POST")
        {
            this.httpMethod = httpMethod;
        }
        
        /// <summary>
        /// Match
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="route"></param>
        /// <param name="parameterName"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest &&
                httpContext.Request.HttpMethod == this.httpMethod &&
                values[parameterName] != null)
            {
                return false;
            }

            return true;
        }
    }
}
