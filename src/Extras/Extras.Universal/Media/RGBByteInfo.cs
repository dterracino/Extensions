//-----------------------------------------------------------------------
// <copyright file="RGBByteInfo.cs" company="Genesys Source">
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


namespace Genesys.Extras.Media
{    
    /// <summary>
    /// Color info in RGB converted format
    /// </summary>
    [CLSCompliant(true)]
    public class RGBByteInfo
    {
        /// <summary>
        /// Alpha channel (transparency)
        /// </summary>
        public byte Alpha { get; set; } = TypeExtension.DefaultByte;
        /// <summary>
        /// Blue channel
        /// </summary>
        public byte Blue { get; set; } = TypeExtension.DefaultByte;
        /// <summary>
        /// Green channel
        /// </summary>
        public byte Green { get; set; } = TypeExtension.DefaultByte;
        /// <summary>
        /// Red channel
        /// </summary>
        public byte Red { get; set; } = TypeExtension.DefaultByte;
        
        /// <summary>
        /// Converts RGB to Hex #RRGGBB
        /// </summary>
        /// <returns></returns>
        public string ToHex()
        {
            return String.Format("#{0}{1}{2}", this.Red.ToString("X2"), this.Green.ToString("X2"), this.Blue.ToString("X2"));
        }
        /// <summary>
        /// Converts RGB to RGB(RR,GG,BB)
        /// </summary>
        /// <returns></returns>
        public string ToRGBString()
        {
            return String.Format("RGB({0},{1},{2})", this.Red.ToString(), this.Green.ToString(), this.Blue.ToString());
        }       
    }
}
