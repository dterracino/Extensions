//-----------------------------------------------------------------------
// <copyright file="Base64Encoder.cs" company="Genesys Source">
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
using System;
using Genesys.Extensions;

namespace Genesys.Extras.Text.Encoding
{
    /// <summary>
    /// Encoders and decodes Base64 text
    /// </summary>
    [CLSCompliant(true)]
    public class Base64Encoder : IEncoder
    {
        private string dataIn = TypeExtension.DefaultString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataToProcess">Data to encrypt or decrypt</param>
        public Base64Encoder(string dataToProcess) : base()
        {
            this.dataIn = dataToProcess;
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(this.dataIn);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <param name="stringToEncode"></param>
        /// <returns></returns>
        public static string Encode(string stringToEncode)
        {
            Base64Encoder encoder = new Base64Encoder(stringToEncode);
            return encoder.Encode();
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <returns></returns>
        public string Decode()
        {
            byte[] bytes = Convert.FromBase64String(this.dataIn);
            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length - 1);
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <param name="stringToDecode"></param>
        /// <returns></returns>
        public static string Decode(string stringToDecode)
        {
            Base64Encoder encoder = new Base64Encoder(stringToDecode);
            return encoder.Decode();
        }
    }
}
