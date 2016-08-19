//-----------------------------------------------------------------------
// <copyright file="FileSearcherTests.cs" company="Genesys Source">
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
using System.IO;
using System.Linq;
using Genesys.Extras.IO;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// Exercises the file searcher class
    /// </summary>
    [TestClass()]
    public class FileSearcherTests
    {
        [TestMethod()]
        public void IO_FileSearcher()
        {
            // Initialize
            List<String> PathsToSearch = new List<String>() { Directory.GetCurrentDirectory() };
            String MaskToSearch = @"app.config";
            FileSearcher Searcher = new FileSearcher(PathsToSearch, MaskToSearch, 2);

            // Look for this assembly App.Config            
            Searcher.Search();

            // Ssarch
            Assert.IsTrue(Searcher.FoundFiles.Count() > 0, "Failed.");
        }
    }
}