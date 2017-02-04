//-----------------------------------------------------------------------
// <copyright file="ListExtensionTests.cs" company="Genesys Source">
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
using System.Collections.Generic;

namespace Genesys.Extensions.Test
{
    [TestClass()]
    public class ListExtensionTests
    {
        public List<string> names1 = new List<string>() { "Burke", "Connor", "Frank", "Everett", "Albert", "George", "Harris", "David" };
        public List<string> names2 = new List<string>() { "Joe", "James", "Jack" };

        public class ComplexObject
        {
            public string Name { get; set; }
        }
        public class ComplexList : List<ComplexObject>
        {
            public ComplexList() : base()
            {
                AddRange(new List<ComplexObject>() {
                    new ComplexObject() { Name = "Larry" }, new ComplexObject() { Name = "Curly" }, new ComplexObject() { Name = "Mo" }});
            }
        }

        [TestMethod()]
        public void List_FirstOrDefaultSafe()
        {
            Assert.IsTrue(names1.FirstOrDefaultSafe("Not found") == names1[0], "Did not work");
        }

        [TestMethod()]
        public void List_AddRange()
        {
            List<string> allNames = new List<string>();
            allNames.AddRange(names1);
            allNames.AddRange(names2);
            Assert.IsTrue(allNames.Count == (names1.Count + names2.Count), "Did not work");
        }

        [TestMethod()]
        public void List_Fill()
        {
            ComplexList fullList = new ComplexList();
            ComplexList emptyList = new ComplexList();

            emptyList.Clear();
            Assert.IsTrue(emptyList.Count == 0, "Did not work");
            emptyList.FillRange(fullList);
            Assert.IsTrue(emptyList.Count == fullList.Count, "Did not work");
        }
    }
}
