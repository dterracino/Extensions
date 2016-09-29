//-----------------------------------------------------------------------
// <copyright file="TextCleaningTests.cs" company="Genesys Source">
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
using Genesys.Extras.Text.Cleansing;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class TextCleaningTests
    {
        private string safeTag1 = "Hello World";
        private string unsafeTag1 = "<script>function () { var unsafeX = 1;}</script>";
        private string unsafeTag2 = "<script type=\"text\\javascript/\">function () { var unsafeX = 2;}</script>";
        private string unsafeHtml { get { return string.Format("{0}{1}{2}", unsafeTag1, safeTag1, unsafeTag2); } }

        [TestMethod()]
        public void Text_Cleanser_HtmlUnsafe()
        {
            string safeHtml = TypeExtension.DefaultString;
            HtmlUnsafeCleanser cleanser = new HtmlUnsafeCleanser(unsafeHtml);
            safeHtml = cleanser.Cleanse();
            Assert.IsTrue(safeHtml.Contains(unsafeTag1.SubstringLeft(6)) == false, "Did not work.");
            Assert.IsTrue(safeHtml.Contains(safeTag1) == true, "Did not work.");
        }

        [TestMethod()]
        public void Text_Cleanser_Attribute()
        {
            var testItem = new CleanserAttributeTester() { CleanseMe = unsafeHtml };

            Cleanser.CleanseAll(testItem);

            Assert.IsTrue(testItem.CleanseMe.Contains(unsafeTag1.SubstringLeft(6)) == false, "Did not work.");
            Assert.IsTrue(testItem.CleanseMe.Contains(safeTag1) == true, "Did not work.");
        }

        private class CleanserAttributeTester
        {
            [CleanseFor(CleanserIDs.UnsafeHtml)]
            public string CleanseMe { get; set; }
        }
    }
}
