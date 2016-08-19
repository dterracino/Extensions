//-----------------------------------------------------------------------
// <copyright file="HttpRequestExtension.cs" company="Genesys Source">
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
using System.IO;
using System.Xml;

namespace Genesys.Extensions
{
    /// <summary>
    /// Extends the XmlDocument class
    /// </summary>
    public static class XmlDocumentExtension
    {
        /// <summary>
        /// Outputs a string equivalent to the xml document
        /// </summary>
        /// <param name="item">XmlDocument to output</param>
        /// <returns></returns>
        public static string Serialize(this XmlDocument item)
        {
            string returnValue = TypeExtension.DefaultString;

            using (var stringWrite = new StringWriter())
            using (var xmlWrite = XmlWriter.Create(stringWrite))
            {
                item.WriteTo(xmlWrite);
                xmlWrite.Flush();
                returnValue = stringWrite.GetStringBuilder().ToString();
            }

            return returnValue;
        }
    }
}
