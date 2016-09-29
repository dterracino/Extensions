//-----------------------------------------------------------------------
// <copyright file="ObjectExtensionTests.cs" company="Genesys Source">
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

namespace Genesys.Extensions.Test
{
    /// <summary>
    /// Tests the extensions
    /// </summary>
    [TestClass()]
    public class ObjectExtensionTests
    {
        public interface IMyClass
        {
            string MyProperty { get; set; }
        }
        public class MyClass1 : IMyClass
        {
            public string MyProperty { get; set; } = "PropertyData1";
        }
        public class MyClass2 : IMyClass
        {
            public string MyProperty { get; set; } = "PropertyData2";
        }
        public class MyClass3
        {
            public string MyProperty { get; set; } = "PropertyData3";
        }

        /// <summary>
        /// Extensions_Object_DirectCastSafe
        /// </summary>
        [TestMethod()]
        public void Object_DirectCastSafe()
        {
            MyClass1 testItem = new MyClass1();
            MyClass3 compareItem = new MyClass3();
            Assert.IsTrue(testItem.DirectCastSafe<MyClass3>().GetType() == compareItem.GetType(), "Did not work");
        }

        /// <summary>
        /// Extensions_Object_FillByProperty
        /// </summary>
        [TestMethod()]
        public void Object_FillByProperty()
        {
            MyClass1 testItem = new MyClass1();
            MyClass2 fillItem1 = new MyClass2();
            MyClass3 fillItem2 = new MyClass3();
            fillItem1.FillByProperty(testItem);
            fillItem2.FillByProperty(testItem);
            Assert.IsTrue(testItem.MyProperty == fillItem1.MyProperty, "Did not work");
            Assert.IsTrue(testItem.MyProperty == fillItem2.MyProperty, "Did not work");
            Assert.IsTrue(testItem.MyProperty != new MyClass2().MyProperty, "Did not work");
        }

        /// <summary>
        /// Extensions_Object_FillByInterface
        /// </summary>
        [TestMethod()]
        public void Object_FillByInterface()
        {
            MyClass1 testItem = new MyClass1();
            MyClass2 fillItem1 = new MyClass2();
            MyClass3 fillItem2 = new MyClass3();
            fillItem1.FillByInterface(testItem);
            fillItem2.FillByInterface(testItem);
            Assert.IsTrue(testItem.MyProperty == fillItem1.MyProperty, "Did not work");
            Assert.IsTrue(testItem.MyProperty != fillItem2.MyProperty, "Did not work");
            Assert.IsTrue(testItem.MyProperty != new MyClass2().MyProperty, "Did not work");
        }
    }
}
