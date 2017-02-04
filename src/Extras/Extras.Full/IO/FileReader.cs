//-----------------------------------------------------------------------
// <copyright file="FileReaderFull.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
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
using System.IO;
using System.Text;
using Genesys.Extensions;

namespace Genesys.Extras.IO
{
    /// <summary>
    /// Search a set of paths on a drive for a folder. 
    ///     Configure with auto search parent and children a certain levels in.
    /// </summary>
    [CLSCompliant(true)]
    public class FileReader
    {
        private string file = TypeExtension.DefaultString; 
        /// <summary>
        /// Constructor
        /// </summary>
        public FileReader() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Path and file to read</param>
        public FileReader(string fileName)
            : this()
        {
            file = fileName;
        }

        /// <summary>
        /// Reads a file using StreamReader class
        /// </summary>
        public string ReadToEnd()
        {
            var returnValue = TypeExtension.DefaultString;
            using (StreamReader streamReader = new StreamReader(file, Encoding.UTF8))
            {
                returnValue = streamReader.ReadToEnd();
            }
            return returnValue;
        }        
    }
}
