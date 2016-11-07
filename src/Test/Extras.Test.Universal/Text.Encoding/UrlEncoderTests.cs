//-----------------------------------------------------------------------
// <copyright file="UrlEncoderTests.cs" company="Genesys Source">
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
using Genesys.Extras.Text.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class UrlEncoderTests
    {
        [TestMethod()]
        public void Text_Encoding_UrlEncoder()
        {
            UrlEncoder testObject = new UrlEncoder("& and < and /");
            string result = testObject.Encode();
            Assert.IsTrue(result.Length > 0, "Item did not work.");
            Assert.IsTrue(result.Contains("&") == false, "Item did not work.");
            Assert.IsTrue(result.Contains("<") == false, "Item did not work.");
            Assert.IsTrue(result.Contains("/") == false, "Item did not work.");
        }
    }
}
