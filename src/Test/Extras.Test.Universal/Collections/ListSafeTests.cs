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
            kvList.Add("TestKey");
            Assert.AreEqual(1, kvList.Count);
            //kvString.Add("TestKey", "TestValue2");
            //Assert.AreNotEqual(2, kvString.Count);

        }

        [TestMethod()]
        public void Collections_ListSafe_Remove()
        {
            // ToDo: Assert.Fail()
        }

        [TestMethod()]
        public void Collections_ListSafe_FindIndex()
        {
            // ToDo: Assert.Fail()
        }

        [TestMethod()]
        public void Collections_ListSafe_GetValue()
        {
            // ToDo: Assert.Fail()
        }
    }
}
