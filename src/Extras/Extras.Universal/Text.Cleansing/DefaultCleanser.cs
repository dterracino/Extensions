//-----------------------------------------------------------------------
// <copyright file="DefaultCleanser.cs" company="Genesys Source">
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

namespace Genesys.Extras.Text.Cleansing
{
    /// <summary>
    /// Cleanses and removes Html unsafe characters
    /// </summary>
    [CLSCompliant(true)]
    public class DefaultCleanser : Cleanser
    {
        /// <summary>
        /// Target of this cleanser
        /// </summary>
        public override CleanserIDs CleanserID { get; } = CleanserIDs.Default;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public DefaultCleanser() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textToCleanse">Plain text to have characters cleansed</param>
        public DefaultCleanser(string textToCleanse)
            : this()
        {
            TextToCleanse = textToCleanse;
        }

        /// <summary>
        /// Cleanses a string
        /// </summary>
        public override string Cleanse()
        {
            TextCleansed = TextToCleanse;
            return TextCleansed;
        }
    }
}
