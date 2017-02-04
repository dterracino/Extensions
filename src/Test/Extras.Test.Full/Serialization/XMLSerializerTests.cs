//-----------------------------------------------------------------------
// <copyright file="XMLSerializerTests.cs" company="Genesys Source">
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
using Genesys.Extras.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class XmlSerializerTests
    {        
        public class MyClass
        {
            public static string XmlData = "<?xml version=\"1.0\"?>\r\n<MyClass xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <MyProperty>PropertyData</MyProperty>\r\n</MyClass>";
            public string MyProperty { get; set; } = "PropertyData";
        }

        [TestMethod()]
        public void Serialization_Xml_Serialize()
        {
            XmlSerializer serializer = new XmlSerializer();
            MyClass testClass = new MyClass();
            var deserializedData = serializer.Serialize<MyClass>(testClass);
            Assert.IsTrue(deserializedData == MyClass.XmlData, "Did not work");
        }

        [TestMethod()]
        public void Serialization_Xml_Deserialize()
        {
            XmlSerializer serializer = new XmlSerializer();
            MyClass serializedData = serializer.Deserialize<MyClass>(MyClass.XmlData);
            Assert.IsTrue(serializedData.MyProperty == new MyClass().MyProperty, "Did not work");
        }
    }
}
