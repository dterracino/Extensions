//-----------------------------------------------------------------------
// <copyright file="IconExtension.cs" company="Genesys Source">
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
using System.Drawing;
using System.IO;

namespace Genesys.Extensions
{
    /// <summary>
    /// Icon Extension
    /// </summary>
    [CLSCompliant(true)]
    public static class IconExtension
    {
        /// <summary>
        /// Converts a System.Drawing.Icon to a byte array
        /// </summary>
        /// <returns>Byte array containing the icon</returns>
        public static byte[] ToBytes(this Icon item)
        {
            var returnValue = new MemoryStream();
            if ((item == null == false) && (item.Size.Width > 0 & item.Size.Height > 0))
            {
                item.Save(returnValue);
            }
            return returnValue.ToArray();
        }
    }
}
