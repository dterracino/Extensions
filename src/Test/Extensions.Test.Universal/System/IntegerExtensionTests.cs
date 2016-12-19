//-----------------------------------------------------------------------
// <copyright file="IntegerExtensionTests.cs" company="Genesys Source">
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

namespace Genesys.Extensions.Test
{
    [TestClass()]
    public class IntegerExtensionTests
    {
        [TestMethod()]
        public void Integer_ToDecimal()
        {
            var testItem = 10;
            Assert.IsTrue(testItem.ToDecimal() == 10.00M, "Did not work");
        }

        [TestMethod()]
        public void Integer_ToGuid()
        {
            Guid itemGuid = new Guid("00003039-0000-0000-0000-000000000000");
            var itemInt = 12345;

            Assert.IsTrue(itemGuid.ToInteger() == itemInt, "Did not work");
            Assert.IsTrue(itemInt.ToGuid() == itemGuid, "Did not work");
        }

        [TestMethod()]
        public void Integer_Negate()
        {
            var testItem = 10;
            Assert.IsTrue(testItem.Negate() == (testItem * -1), "Did not work");
        }
    }
}
