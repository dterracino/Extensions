//-----------------------------------------------------------------------
// <copyright file="TemplateBuilderTests.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Extras.Text.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class Base64EncoderTests
    {
        [TestMethod()]
        public void Text_Encoding_Base64Encoder()
        {
            string rawValue = "Raw data value";
            string encodedValue = TypeExtension.DefaultString;

            Base64Encoder encoder = new Base64Encoder(rawValue);
            encodedValue = encoder.Encode();
            encoder = new Base64Encoder(encodedValue);
            Assert.IsTrue(encoder.Decode() == rawValue, "Did not work.");
        }
    }
}
