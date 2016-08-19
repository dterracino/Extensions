//-----------------------------------------------------------------------
// <copyright file="KeyValueListSafeTests.cs" company="Genesys Source">
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
    /// KeyValueListSafe Tests
    /// </summary>
    [TestClass()]
    public class KeyValueListSafeTests
    {
        [TestMethod()]
        public void Collections_KeyValueListSafe_Construct()
        {
            KeyValueListSafe<int, int> kvList = new KeyValueListSafe<int, int>();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Collections_KeyValueListSafe_Add()
        {
            KeyValueListSafe<int, int> kvList = new KeyValueListSafe<int, int>();
            kvList.Add(new KeyValuePairSafe<int, int>(0,0));
            kvList.Add(new KeyValuePairSafe<int, int>(1,1));
            kvList.Add(new KeyValuePairSafe<int, int>(2,2));

            Assert.AreNotEqual(2, kvList.Count);
            Assert.AreEqual(3, kvList.Count);
        }

        [TestMethod()]
        public void Collections_KeyValueListSafe_Remove()
        {
            // ToDo: Assert.Fail()
        }

        [TestMethod()]
        public void Collections_KeyValueListSafe_FindIndex()
        {
            // ToDo: Assert.Fail()
        }

        [TestMethod()]
        public void Collections_KeyValueListSafe_GetValue()
        {
            // ToDo: Assert.Fail()
        }
    }
}
