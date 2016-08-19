//-----------------------------------------------------------------------
// <copyright file="ImageFormatExtension.cs" company="Genesys Source">
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
using System.Drawing.Imaging;

namespace Genesys.Extensions
{
    /// <summary>
    /// Extends the Image Format class
    /// </summary>
    [CLSCompliant(true)]
    public static class ImageFormatExtension
    {
        /// <summary>
        /// Returns mime content type for an Image Format item
        /// </summary>
        /// <param name="item">MIME content type of item</param>
        /// <returns>String containing the MIME content type text</returns>
        public static string ToContentType(this ImageFormat item)
        {
            string returnValue = "image/unknown";
            Guid imgguid = item.Guid;
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == imgguid)
                {
                    returnValue = codec.MimeType;
                    break;
                }
            }

            return returnValue;
        }
    }
}
