//-----------------------------------------------------------------------
// <copyright file="ByteExtension.cs" company="Genesys Source">
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
    /// Byte Extender
    /// </summary>
    [CLSCompliant(true)]
    public static class ByteExtension
    {
        /// <summary>
        /// Converts a byte array to a String
        /// </summary>
        /// <param name="item">Byte array containing the string.</param>
        /// <returns>string from the byte array, or empty string</returns>
        public static string ToString(this byte[] item)
        {
            var returnValue = TypeExtension.DefaultString;
            if ((item == null == false) && (item.Length > 0))
            {
                returnValue = System.Text.Encoding.UTF8.GetString(item, 0, item.Length - 1);
            }

            return returnValue;
        }
        
        /// <summary>
        /// Convert an image byte[] to RGBA
        /// </summary>
        /// <param name="item">Item to convert</param>
        /// <param name="heightInPixels">Height of image</param>
        /// <param name="widthInPixels">Width of image</param>
        /// <returns>Converted byte array</returns>
        public static byte[] ToRGB(this byte[] item, int heightInPixels, int widthInPixels)
        {
            var byteOffset = TypeExtension.DefaultInteger;
            byte[] returnValue = item;

            for (var row = 0; row < (uint)heightInPixels; row++)
            {
                for (var column = 0; column < (uint)widthInPixels; column++)
                {
                    byteOffset = (row * (int)widthInPixels * 4) + (column * 4);
                    byte b = returnValue[byteOffset];
                    byte g = returnValue[byteOffset + 1];
                    byte r = returnValue[byteOffset + 2];
                    byte a = returnValue[byteOffset + 3];
                    returnValue[byteOffset] = r; // Red
                    returnValue[byteOffset + 1] = g; // Green
                    returnValue[byteOffset + 2] = b; // Blue
                    returnValue[byteOffset + 3] = a; // Alpha
                }
            }

            return returnValue;
        }
    }
}
