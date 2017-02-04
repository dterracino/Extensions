//-----------------------------------------------------------------------
// <copyright file="PropertyInfoExtension.cs" company="Genesys Source">
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
using System.Reflection;

namespace Genesys.Extensions
{
    /// <summary>
    /// HttpRequestBaseExtension
    /// </summary>
    [CLSCompliant(true)]
    public static class PropertyInfoExtension
    {
        /// <summary>
        /// Gets the value of an attribute that implements IAttributeValue
        /// </summary>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns></returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this PropertyInfo item, TValue notFoundValue) where TAttribute : Attribute, IAttributeValue<TValue>
        {
            TValue returnValue = notFoundValue;

            foreach (object attribute in item.GetCustomAttributes(false))
            {
                if ((attribute is TAttribute) == true)
                {
                    returnValue = ((TAttribute)attribute).Value;
                    break;
                }
            }

            return returnValue;
        }
    }
}
