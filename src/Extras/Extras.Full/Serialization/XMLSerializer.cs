//-----------------------------------------------------------------------
// <copyright file="XMLSerializer.cs" company="Genesys Source">
//      Copyright (c) 2016 Genesys Source. All rights reserved.
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
using System.Xml;
using Genesys.Extensions;
using Genesys.Extras.Collections;

namespace Genesys.Extras.Serialization
{
    /// <summary>
    /// Xml serialization and deserialization
    /// </summary>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class XMLSerializer : Serializer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public XMLSerializer() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public XMLSerializer(IListSafe<Type> knownTypes) : base(knownTypes) { }
        
        /// <summary>
        /// Serializes the passed object to a string
        /// </summary>
        /// <typeparam name="T">Type of incoming object</typeparam>
        /// <param name="objectToSerialize">Object to serialize</param>
        /// <returns>Xml string</returns>
        public override string Serialize<T>(T objectToSerialize)
        {
            string returnValue = TypeExtension.DefaultString;
            MemoryStream Stream = new MemoryStream();

            try
            {
                if (objectToSerialize == null && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is null. Unable to serialize null objects."); }
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
                XmlTextWriter textWriter = new XmlTextWriter(Stream, System.Text.Encoding.UTF8);
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                xs.Serialize(Stream, objectToSerialize);
                Stream = (MemoryStream)textWriter.BaseStream;
                returnValue = encoding.GetString(Stream.ToArray());
            }
            catch
            {
                if (this.ThrowException) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <typeparam name="T">Type of outgoing object</typeparam>
        /// <param name="stringToDeserialize">Object to deserialize</param>
        /// <returns>Concrete class</returns>
        public override T Deserialize<T>(string stringToDeserialize)
        {
            T returnValue = TypeExtension.InvokeConstructorOrDefault<T>();

            try
            {
                if (stringToDeserialize == TypeExtension.DefaultString && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is empty. Unable to deserialize empty strings."); }
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] ByteArray = (byte[])encoding.GetBytes(stringToDeserialize);
                MemoryStream memoryStream = new MemoryStream(ByteArray);
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                returnValue = (T)xs.Deserialize(memoryStream);
            }
            catch
            {
                if (this.ThrowException) throw;
            }

            return returnValue;
        }
    }
}
