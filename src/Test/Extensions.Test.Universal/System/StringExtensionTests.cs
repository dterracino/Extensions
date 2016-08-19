//-----------------------------------------------------------------------
// <copyright file="StringExtensionTests.cs" company="Genesys Source">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Genesys.Extensions;

namespace Genesys.Extensions.Test
{
    [TestClass()]
    public class StringExtensionTests
    {
        [TestMethod()]
        public void String_SubstringRight()
        {
            String TestItem = "http://localhost/";

            // Test
            Assert.IsTrue(TestItem.SubstringRight(1) == TestItem.Substring(TestItem.Length - 1, 1), "SubstringRight() is not functioning properly.");
        }

        [TestMethod()]
        public void String_SubstringLeft()
        {
            String TestItem = "http://localhost/";

            // Test
            Assert.IsTrue(TestItem.SubstringLeft(1) == TestItem.Substring(0, 1), "SubstringLeft() is not functioning properly.");
        }

        [TestMethod()]
        public void String_SubstringSafe()
        {
            String TestItem = "http://localhost/";

            // Test
            Assert.IsTrue(TestItem.SubstringSafe(0, 1).Length == 1, "SubstringSafe() is not functioning properly.");
        }

        [TestMethod()]
        public void String_RemoveFirst()
        {
            String TestItem = "http://localhost/";

            // Test
            Assert.IsTrue(TestItem.RemoveFirst("h").Length == TestItem.Length - 1, "RemoveFirst() is not functioning properly.");
        }

        [TestMethod()]
        public void String_RemoveLast()
        {
            String TestItem = "http://localhost/";

            // Test
            Assert.IsTrue(TestItem.RemoveLast("/").Length == TestItem.Length - 1, "RemoveLast() is not functioning properly.");
        }

        [TestMethod()]
        public void String_ToPascalCase()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsCaseUpper()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsCaseLower()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsCaseMixed()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsFirst()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsLast()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsEmail()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_IsInteger()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseBoolean()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseInt32()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseInt64()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseGuid()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseDecimal()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseDouble()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseDateTime()
        {
            // ToDo: Assert.Fail();
        }

        [TestMethod()]
        public void String_TryParseTime()
        {
            // ToDo: Assert.Fail();
        }
    }
}
