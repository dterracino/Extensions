//-----------------------------------------------------------------------
// <copyright file="GuidExtension.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
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

namespace Genesys.Extensions
{
    /// <summary>
    /// DoubleExtension
    /// </summary>    
    [CLSCompliant(true)]
    public static class  GuidExtension
    {
        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Integer to convert to guid.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static int ToInteger(this Guid item)
        {
            var returnValue = TypeExtension.DefaultInteger;

            try
            {
            var b = item.ToByteArray();
                var bint = BitConverter.ToInt32(b, 0);
                returnValue = bint;
            }
            catch
            {
                returnValue = TypeExtension.DefaultInteger;
            }
            return returnValue;
        }
    }
}
