//-----------------------------------------------------------------------
// <copyright file="SerializerGenericTests.cs" company="Genesys Source">
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
    /// <summary>
    /// Serialization Tests
    /// </summary>
    [TestClass()]
    public class SerializerGenericTests
    {
        /// <summary>
        /// Test standard value types
        /// </summary>
        [TestMethod()]
        public void Serialization_SerializerGeneric_ValueTypes()
        {
            // Immutable string class
            String Data1 = TypeExtension.DefaultString;
            String TestData1 = "TestDataHere";
            ISerializer<Object> Serialzer1 = new JsonSerializer<Object>();
            Data1 = Serialzer1.Serialize(TestData1);
            Assert.IsTrue(Serialzer1.Deserialize(Data1).ToString() == TestData1);

            // Mutable string class
            String Data = Data = TypeExtension.DefaultString;
            StringMutable TestData = "TestDataHere";
            ISerializer<StringMutable> Serialzer = new JsonSerializer<StringMutable>();
            Data = Serialzer.Serialize(TestData);
            Assert.IsTrue(Serialzer.Deserialize(Data).ToString() == TestData.ToString());
        }

        /// <summary>
        /// Test standard reference types
        /// </summary>
        [TestMethod()]
        public void Serialization_SerializerGeneric_ReferenceTypes()
        {
            // Collections, etc
            List<Int32> ItemL = new List<Int32> { 1, 2, 3 };
            JsonSerializer<List<Int32>> Serializer = new JsonSerializer<List<Int32>>();
            String SerializedDataL = Serializer.Serialize(ItemL);
            Assert.IsTrue(ItemL.Count == Serializer.Deserialize(SerializedDataL).Count, "Failed.");
        }
    }
}
