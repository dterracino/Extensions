//-----------------------------------------------------------------------
// <copyright file="JsonSerializerGenericTests.cs" company="Genesys Source">
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
using System.Collections.Generic;
using Genesys.Extensions;
using Genesys.Extras.Serialization;
using Genesys.Extras.Text;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class JsonSerializerGenericTests
    {
        private const string testPhrase = "Services up and running...";
        private const string testPhraseSerialized = "\"Services up and running...\"";
        private const string testPhraseMutableSerialized = "{\"Value\":\"Services up and running...\"}";

        [TestMethod()]
        public void Serialization_Json_ValueTypes()
        {
            // Immutable string class
            string Data1 = TypeExtension.DefaultString;
            string TestData1 = "TestDataHere";
            ISerializer<Object> Serialzer1 = new JsonSerializer<Object>();
            Data1 = Serialzer1.Serialize(TestData1);
            Assert.IsTrue(Serialzer1.Deserialize(Data1).ToString() == TestData1, "Did not work");

            
            string Data = Data = TypeExtension.DefaultString;
            StringMutable TestData = "TestDataHere";
            ISerializer<StringMutable> Serialzer = new JsonSerializer<StringMutable>();
            Data = Serialzer.Serialize(TestData);
            Assert.IsTrue(Serialzer.Deserialize(Data).ToString() == TestData.ToString(), "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_ReferenceTypes()
        {
            // Collections, etc
            List<Int32> ItemL = new List<Int32> { 1, 2, 3 };
            JsonSerializer<List<Int32>> Serializer = new JsonSerializer<List<Int32>>();
            string SerializedDataL = Serializer.Serialize(ItemL);
            Assert.IsTrue(ItemL.Count == Serializer.Deserialize(SerializedDataL).Count, "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_String()
        {
            JsonSerializer<string> serializer = new JsonSerializer<string>();

            Assert.IsTrue(testPhraseSerialized == serializer.Serialize(testPhrase), "Did not work");
            Assert.IsTrue(testPhrase == serializer.Deserialize(testPhraseSerialized), "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_StringMutable()
        {
            StringMutable testPhraseMutable = testPhrase;
            string result = TypeExtension.DefaultString;
            StringMutable resultMutable = TypeExtension.DefaultString;
            JsonSerializer<StringMutable> serializerMutable = new JsonSerializer<StringMutable>();
           
            // Serialization            
            testPhraseMutable = testPhrase;
            result = serializerMutable.Serialize(testPhraseMutable);
            Assert.IsTrue(result == testPhraseMutableSerialized, "Did not work");

            // Deserialization
            resultMutable = serializerMutable.Deserialize(testPhraseMutableSerialized);
            Assert.IsTrue(resultMutable == testPhrase, "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_StringToStringMutable()
        {
            StringMutable testPhraseMutable = testPhrase;
            string result = TypeExtension.DefaultString;
            StringMutable resultMutable = TypeExtension.DefaultString;
            JsonSerializer<StringMutable> serializerMutable = new JsonSerializer<StringMutable>();
            JsonSerializer<string> serializer = new JsonSerializer<string>();

            // string Mutable can be serialized as string, then deserialized as string after transport 
            //  So that consumers don't need to know original was StringMutable
            result = serializer.Serialize(testPhraseMutable);
            Assert.IsTrue(testPhraseSerialized == result, "Did not work");

            // StringMutable serialize -> string deserialize
            result = serializerMutable.Deserialize(testPhraseSerialized); // Not supported scenario, should default ot empty string
            Assert.IsTrue(result == TypeExtension.DefaultString, "Did not work"); 

            result = serializerMutable.Deserialize(testPhraseMutableSerialized);
            Assert.IsTrue(result == testPhrase, "Did not work");
            resultMutable = serializerMutable.Deserialize(testPhraseMutableSerialized);
            Assert.IsTrue(resultMutable == testPhrase, "Did not work");
        }
    }
}
