//-----------------------------------------------------------------------
// <copyright file="ListSafeTests.cs" company="Genesys Source">
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
using Genesys.Extras.Collections;
using Genesys.Extensions;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// ListSafe Tests
    /// </summary>
    [TestClass()]
    public class ListSafeTests
    {
        [TestMethod()]
        public void Collections_ListSafe_Construct()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Collections_ListSafe_Add()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            Assert.AreEqual(1, kvList.Count, "Did not work");
        }

        [TestMethod()]
        public void Collections_ListSafe_Remove()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            kvList.Remove("TestKey1");
            Assert.IsTrue(kvList.Count == 1, "Did not work");
        }

        [TestMethod()]
        public void Collections_ListSafe_FindIndex()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            Assert.IsTrue(kvList.FindIndex("TestKey2") == 2, "Did not work");
        }

        [TestMethod()]
        public void Collections_ListSafe_GetValue()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            Assert.IsTrue(kvList.GetValue("TestKey2") != TypeExtension.DefaultString, "Did not work");
        }
    }
}
