//-----------------------------------------------------------------------
// <copyright file="HttpClientBuilder.cs" company="Genesys Source">
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
    public class RGBStandardInfo
    {
        /// <summary>
        /// Alpha channel (transparency)
        /// </summary>
        public float Alpha { get; set; } = TypeExtension.DefaultSingle;
        /// <summary>
        /// Blue channel
        /// </summary>
        public float Blue { get; set; } = TypeExtension.DefaultSingle;
        /// <summary>
        /// Green channel
        /// </summary>
        public float Green { get; set; } = TypeExtension.DefaultSingle;
        /// <summary>
        /// Red channel
        /// </summary>
        public float Red { get; set; } = TypeExtension.DefaultSingle;
        
        /// <summary>
        /// Inverses the current RGB values
        /// </summary>
        /// <returns></returns>
        public RGBStandardInfo Inverse()
        {
            RGBStandardInfo returnValue = new RGBStandardInfo();
            returnValue.Red = (1.0f - this.Red);
            returnValue.Green = (1.0f - this.Green);
            returnValue.Blue = (1.0f - this.Blue);
            return returnValue;
        }
    }
}
