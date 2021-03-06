//-----------------------------------------------------------------------
// <copyright file="JsonSerializerGenericTests.cs" company="Genesys Source">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Genesys.Extensions;
using Genesys.Extras.Serialization;
using Genesys.Extras.Text;
using System.Runtime.Serialization.Json;

namespace Genesys.Extras.Test
{
    public class PersonInfo
    {
        public string FirstName { get; set; } = TypeExtension.DefaultString;
        public string LastName { get; set; } = TypeExtension.DefaultString;
        public DateTime BirthDate { get; set; } = TypeExtension.DefaultDate;
    }

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
            var data1= TypeExtension.DefaultString;
            var Testdata1= "TestDataHere";
            ISerializer<object> Serialzer1 = new JsonSerializer<object>();
            data1= Serialzer1.Serialize(Testdata1);
            Assert.IsTrue(Serialzer1.Deserialize(data1).ToString() == Testdata1, "Did not work");
            
            var data = TypeExtension.DefaultString;
            StringMutable TestData = "TestDataHere";
            var Serialzer = new JsonSerializer<StringMutable>();
            data = Serialzer.Serialize(TestData);
            Assert.IsTrue(Serialzer.Deserialize(data).ToString() == TestData.ToString(), "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_ReferenceTypes()
        {
            // Collections, etc
            var ItemL = new List<int> { 1, 2, 3 };
            var Serializer = new JsonSerializer<List<int>>();
            var SerializedDataL = Serializer.Serialize(ItemL);
            Assert.IsTrue(ItemL.Count == Serializer.Deserialize(SerializedDataL).Count, "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_PersonInfo()
        {
            // Collections, etc
            var personObject = new PersonInfo() { FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1977, 11, 20) };
            var personObjectSerialized = TypeExtension.DefaultString;
            var personJsonDefaultDate = "{\"BirthDate\":\"\\/Date(248860800000-0800)\\/\",\"FirstName\":\"John\",\"LastName\":\"Doe\"}";
            var personJsonISODate = "{\"FirstName\":\"John\",\"MiddleName\":\"Michelle\",\"LastName\":\"Doe\",\"BirthDate\":\"1977-11-20T00:00:00\",\"ID\":-1,\"Key\":\"00000000-0000-0000-0000-000000000000\"}";
            var personJsonReSerialized = TypeExtension.DefaultString;
            var personJsonDeserialized = new PersonInfo();
            var serializer = new JsonSerializer<PersonInfo>();

            // stringISODate -> object -> string
            serializer = new JsonSerializer<PersonInfo>();
            personJsonDeserialized = serializer.Deserialize(personJsonISODate);
            Assert.IsTrue(personJsonDeserialized.FirstName == "John", "Did not work");
            Assert.IsTrue(personJsonDeserialized.LastName == "Doe", "Did not work");
            Assert.IsTrue(personJsonDeserialized.BirthDate == new DateTime(1977, 11, 20), "Did not work");
            personJsonReSerialized = serializer.Serialize(personJsonDeserialized);
            Assert.IsTrue(personJsonReSerialized.Length > 0, "Did not work");

            // object -> string -> object
            personObjectSerialized = serializer.Serialize(personObject);
            Assert.IsTrue(personObjectSerialized.Length > 0, "Did not work");
            personObject = serializer.Deserialize(personObjectSerialized);
            Assert.IsTrue(personObject.FirstName == "John", "Did not work");
            Assert.IsTrue(personObject.LastName == "Doe", "Did not work");
            Assert.IsTrue(personObject.BirthDate == new DateTime(1977, 11, 20), "Did not work");

            // stringNONISODate (default date) -> object -> string
            DataContractJsonSerializer defaultSerializer = new DataContractJsonSerializer(typeof(PersonInfo));
            serializer = new JsonSerializer<PersonInfo>();
            serializer.DateTimeFormat = defaultSerializer.DateTimeFormat;
            personJsonDeserialized = serializer.Deserialize(personJsonDefaultDate);
            Assert.IsTrue(personJsonDeserialized.FirstName == "John", "Did not work");
            Assert.IsTrue(personJsonDeserialized.LastName == "Doe", "Did not work");
            Assert.IsTrue(personJsonDeserialized.BirthDate == new DateTime(1977, 11, 20), "Did not work");
            personJsonReSerialized = serializer.Serialize(personJsonDeserialized);
            Assert.IsTrue(personJsonReSerialized.Length > 0, "Did not work");
        }        

        [TestMethod()]
        public void Serialization_Json_String()
        {
            var serializer = new JsonSerializer<string>();

            Assert.IsTrue(testPhraseSerialized == serializer.Serialize(testPhrase), "Did not work");
            Assert.IsTrue(testPhrase == serializer.Deserialize(testPhraseSerialized), "Did not work");
        }

        [TestMethod()]
        public void Serialization_Json_StringMutable()
        {
            StringMutable testPhraseMutable = testPhrase;
            var result = TypeExtension.DefaultString;
            StringMutable resultMutable = TypeExtension.DefaultString;
            var serializerMutable = new JsonSerializer<StringMutable>();
           
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
            var result = TypeExtension.DefaultString;
            StringMutable resultMutable = TypeExtension.DefaultString;
            var serializerMutable = new JsonSerializer<StringMutable>();
            var serializer = new JsonSerializer<string>();

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
