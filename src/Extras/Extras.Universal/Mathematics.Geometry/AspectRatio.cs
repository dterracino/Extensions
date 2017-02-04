//-----------------------------------------------------------------------
// <copyright file="AspectRatio.cs" company="Genesys Source">
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
using Genesys.Extensions;

namespace Genesys.Extras.Mathematics
{
    /// <summary>
    /// MathHelper - Read Only Collection
    /// </summary>
    [CLSCompliant(true)]
    public class AspectRatio
    {
        /// <summary>
        /// Gets new width for changing height
        /// </summary>
        /// <param name="original">Original square</param>
        /// <param name="newHeight">New height</param>
        /// <returns>Width given original item was resized</returns>
        public static Square WidthChange(Square original, int newHeight)
        {
            Square returnValue = new Square();
            decimal multiplier = TypeExtension.DefaultDecimal;

            // Height is only specified, have to calculate width
            multiplier = Arithmetic.Divide(newHeight.ToDecimal(), original.Height.ToDecimal());
            // Resize
            returnValue.Width = Arithmetic.Multiply(original.Width.ToDecimal(), multiplier).ToInt();

            return returnValue;
        }

        /// <summary>
        /// Gets new width for changing height
        /// </summary>
        /// <param name="original">Original square</param>
        /// <param name="newWidth">New width</param>
        /// <returns>Width given original item was resized</returns>
        public static Square HeightChange(Square original, int newWidth)
        {
            Square returnValue = new Square();
            decimal multiplier = TypeExtension.DefaultDecimal;

            // Height is only specified, have to calculate width
            multiplier = Arithmetic.Divide(newWidth.ToDecimal(), original.Width.ToDecimal());
            // Resize
            returnValue.Height = Arithmetic.Multiply(original.Height.ToDecimal(), multiplier).ToInt();

            return returnValue;
        }
    }
}
