//-----------------------------------------------------------------------
// <copyright file="HtmlUnsafeCleanser.cs" company="Genesys Source">
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Genesys.Extensions;

namespace Genesys.Extras.Text.Cleansing
{
    /// <summary>
    /// Cleanses and removes Html unsafe characters
    /// </summary>
    [CLSCompliant(true)]
    public class HtmlUnsafeCleanser : Cleanser
    {
        private string wrapperTag = "wrapperTag";
        private string safeTag = "span";

        /// <summary>
        /// Target of this cleanser
        /// </summary>
        public override CleanserIDs CleanserID { get; } = CleanserIDs.UnsafeHtml;

        /// <summary>
        /// Array of safe tags
        /// </summary>
        public string[] SafeTags { get; set; } = { "#text", "p", "br", "strong", "b", "em", "i", "u", "strike", "ol", "ul", "li", "a", "q", "site", "abbr", "acronym", "del", "ins" };

        /// <summary>
        /// Array of safe attributes
        /// </summary>
        public string[] SafeAttributes { get; set; } = { "height", "width", "alt" };

        /// <summary>
        /// Array of safe keys
        /// </summary>
        public string[] SafeStyleKeys { get; set; } = { "" };

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlUnsafeCleanser() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textToCleanse">Plain text to have characters cleansed</param>
        public HtmlUnsafeCleanser(string textToCleanse)
            : this()
        {
            TextToCleanse = textToCleanse;
        }

        /// <summary>
        /// Cleanses a string
        /// </summary>
        public override string Cleanse()
        {
            var docToParse = XDocument.Parse(String.Format("{0}{1}{2}", FormatBeginTag(wrapperTag), this.TextToCleanse, FormatEndTag(wrapperTag)));

            CleanseUnsafeHtml(docToParse);
            TextCleansed = docToParse.ToString();
            TextCleansed = this.TextCleansed.RemoveFirst(FormatBeginTag(wrapperTag)).RemoveLast(FormatEndTag(wrapperTag));

            return TextCleansed;
        }

        /// <summary>
        /// Recursive parser
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="rootElement"></param>
        private void CleanseUnsafeHtml(XDocument doc, XElement rootElement = null)
        {
            IEnumerable<XElement> elements;
            rootElement = rootElement ?? doc.Elements(wrapperTag).FirstOrDefault(); // We forced a root placeholder element to ensure one child begins the tree
            elements = rootElement.Elements();

            foreach (XElement element in elements)
            {
                if (!(SafeTags.Any(x => x.ToLower() == element.Name.LocalName.ToLower())))
                {
                    element.Name = safeTag;
                    element.RemoveAttributes();
                } 
                else if (element.Attributes() != null)
                {
                    var attrList = element.Attributes().OfType<XAttribute>().ToList();
                    foreach (XAttribute attr in attrList)
                    {
                        if (!(SafeAttributes.Any(x => x.ToLower() == attr.Name)))
                        {
                            element.RemoveAttributes();
                        }
                    }
                }
                if (element.Descendants().Count() > 0)
                    CleanseUnsafeHtml(doc, element);
            }
        }

        /// <summary>
        /// Formats a begin tag
        /// </summary>
        /// <param name="tagName">Name of tag</param>
        /// <returns></returns>
        private string FormatBeginTag(string tagName)
        {
            return String.Format("<{0}>", tagName);
        }

        /// <summary>
        /// Formats a end tag
        /// </summary>
        /// <param name="tagName">Name of tag</param>
        /// <returns></returns>
        private string FormatEndTag(string tagName)
        {
            return String.Format("</{0}>", tagName);
        }
    }
}
