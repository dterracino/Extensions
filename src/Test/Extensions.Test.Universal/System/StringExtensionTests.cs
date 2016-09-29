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
            string TestItem = "http://localhost/";
            Assert.IsTrue(TestItem.SubstringRight(1) == TestItem.Substring(TestItem.Length - 1, 1), "Did not work");
        }

        [TestMethod()]
        public void String_SubstringLeft()
        {
            string TestItem = "http://localhost/";
            Assert.IsTrue(TestItem.SubstringLeft(1) == TestItem.Substring(0, 1), "Did not work");
        }

        [TestMethod()]
        public void String_SubstringSafe()
        {
            string TestItem = "http://localhost/";
            Assert.IsTrue(TestItem.SubstringSafe(0, 1).Length == 1, "Did not work");
        }

        [TestMethod()]
        public void String_RemoveFirst()
        {
            string TestItem = "http://localhost/";
            Assert.IsTrue(TestItem.RemoveFirst("h").Length == TestItem.Length - 1, "Did not work");
        }

        [TestMethod()]
        public void String_RemoveLast()
        {
            string TestItem = "http://localhost/";
            Assert.IsTrue(TestItem.RemoveLast("/").Length == TestItem.Length - 1, "Did not work");
        }

        [TestMethod()]
        public void String_ToPascalCase()
        {
            string lower = "hello";
            Assert.IsTrue(lower.ToPascalCase().SubstringLeft(2) == "He", "Did not work");
        }

        [TestMethod()]
        public void String_IsCaseUpper()
        {
            string mixed = "Hello";
            string upper = "HELLO";
            Assert.IsTrue(mixed.IsCaseUpper() == false, "Did not work");
            Assert.IsTrue(upper.IsCaseUpper() == true, "Did not work");
        }

        [TestMethod()]
        public void String_IsCaseLower()
        {
            string mixed = "Hello";
            string lower = "hello";
            Assert.IsTrue(mixed.IsCaseLower() == false, "Did not work");
            Assert.IsTrue(lower.IsCaseLower() == true, "Did not work");
        }

        [TestMethod()]
        public void String_IsCaseMixed()
        {
            string mixed = "Hello";
            string upper = "HELLO";            
            Assert.IsTrue(mixed.IsCaseMixed() == true, "Did not work");
            Assert.IsTrue(upper.IsCaseMixed() == false, "Did not work");
        }

        [TestMethod()]
        public void String_IsFirst()
        {
            string testData = "Hello";
            Assert.IsTrue(testData.IsFirst("H") == true, "Did not work");
        }

        [TestMethod()]
        public void String_IsLast()
        {
            string testData = "Hello";
            Assert.IsTrue(testData.IsLast("o") == true, "Did not work");
        }

        [TestMethod()]
        public void String_IsEmail()
        {
            string testDataGood = "testing@getframework.com";
            string testDataBad = "testingATgetframework.com";
            Assert.IsTrue(testDataGood.IsEmail() == true, "Did not work");
            Assert.IsTrue(testDataBad.IsEmail() == false, "Did not work");
        }

        [TestMethod()]
        public void String_IsInteger()
        {
            string testDataGood = "1234";
            string testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.IsInteger() == true, "Did not work");
            Assert.IsTrue(testDataBad.IsInteger() == false, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseBoolean()
        {
            string testDataGood = "true";
            string testDataBad = "NotTrue";
            Assert.IsTrue(testDataGood.IsEmail() == true, "Did not work");
            Assert.IsTrue(testDataBad.IsEmail() == false, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseInt32()
        {
            string testDataGood = "1234";
            string testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseInt32() == 1234, "Did not work");
            Assert.IsTrue(testDataBad.TryParseInt32() == TypeExtension.DefaultInteger, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseInt64()
        {
            string testDataGood = "1234";
            string testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseInt64() == 1234, "Did not work");
            Assert.IsTrue(testDataBad.TryParseInt64() == TypeExtension.DefaultInteger, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseGuid()
        {
            string testDataGood = "A8CA69CE-F8C6-4FCC-9FED-6AF9F94879D9";
            string testDataBad = "A869CE-F8C6-4FCC-9FED-6AF994879D9";
            Assert.IsTrue(testDataGood.TryParseGuid() != TypeExtension.DefaultGuid, "Did not work");
            Assert.IsTrue(testDataBad.TryParseGuid() == TypeExtension.DefaultGuid, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseDecimal()
        {
            string testDataGood = "12.00";
            string testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseDecimal() == 12.00M, "Did not work");
            Assert.IsTrue(testDataBad.TryParseDecimal() == TypeExtension.DefaultDecimal, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseDouble()
        {
            string testDataGood = "12.00";
            string testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseDouble() == 12.00, "Did not work");
            Assert.IsTrue(testDataBad.TryParseDouble() == TypeExtension.DefaultDouble, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseDateTime()
        {
            string testDataGood = "08/24/2011";
            string testDataBad = "badDate";
            Assert.IsTrue(testDataGood.TryParseDateTime().Month == 8, "Did not work");
            Assert.IsTrue(testDataBad.TryParseDateTime() == TypeExtension.DefaultDate, "Did not work");
        }

        [TestMethod()]
        public void String_TryParseTime()
        {
            string testDataGood = "10:45 PM";
            string testDataBad = "badTime";
            Assert.IsTrue(testDataGood.TryParseTime().Minute == 45, "Did not work");
            Assert.IsTrue(testDataBad.TryParseTime().Minute == TypeExtension.DefaultDate.Minute , "Did not work");
        }
    }
}
