//-----------------------------------------------------------------------
// <copyright file="EnumExtensionTests.cs" company="Genesys Source">
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
using Genesys.Extensions;
using System;

namespace Genesys.Extensions.Test
{
    [TestClass()]
    public class EnumExtensionTests
    {
        [TestMethod()]
        public void Enum_Contains()
        {
            EnumConsumer consumer = new EnumConsumer();
            Assert.IsTrue(consumer.enumFlag.Contains(0x01) == true, "Did not work");
        }

        [TestMethod()]
        public void Enum_ToDictionary()
        {
            var dict = EnumConsumer.MyEnumInts.one.ToDictionary();
            Assert.IsTrue(dict.Count > 0 == true, "Did not work");
        }
        public class EnumConsumer
        {
            public enum MyEnumInts
            {
                one = 1,
                two = 2,
                three = 3
            }

            [Flags]
            public enum MyEnumFlags
            {
                one = 0x01,
                two = 0x02,
                four = 0x04,
                eight = 0x08
            }

            public MyEnumInts enumInt = MyEnumInts.one;
            public MyEnumFlags enumFlag = MyEnumFlags.one;
        }

    }
}
