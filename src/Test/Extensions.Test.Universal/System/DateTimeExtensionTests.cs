//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensionTests.cs" company="Genesys Source">
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
    public class DateTimeExtensionTests
    {
        [TestMethod()]
        public void DateTime_Tomorrow()
        {
            DateTime date = DateTime.Now;
            Assert.IsTrue(date.Tomorrow().Day == DateTime.Now.AddDays(1).Day, "Did not work");
        }

        [TestMethod()]
        public void DateTime_Yesterday()
        {
            DateTime date = DateTime.Now;
            Assert.IsTrue(date.Yesterday().Day == DateTime.Now.AddDays(-1).Day, "Did not work");
        }

        [TestMethod()]
        public void DateTime_FirstDayOfMonth()
        {
            DateTime date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.FirstDayOfMonth().Day == 1, "Did not work");
        }

        [TestMethod()]
        public void DateTime_LastDayOfMonth()
        {
            DateTime date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.LastDayOfMonth().Day == 31, "Did not work");
        }

        [TestMethod()]
        public void DateTime_IsSavable()
        {
            DateTime date = new DateTime(1700, 1, 1);
            Assert.IsTrue(date.IsSavable() == false, "Did not work");
        }
    }
}
