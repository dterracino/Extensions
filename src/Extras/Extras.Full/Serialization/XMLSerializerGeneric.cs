//-----------------------------------------------------------------------
// <copyright file="XmlSerializerFull.cs" company="Genesys Source">
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
    /// XML serialization and deserialization
    /// </summary>
    [CLSCompliant(true)]
    public class XmlSerializer<T> : Serializer<T> where T : new()
    {
        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        public new IListSafe<Type> KnownTypes { get; set; } = new ListSafe<Type>();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public XmlSerializer()
            : base()
        {

        }
        
        /// <summary>
        /// Serializes and returns the JSON as a string
        /// </summary>
        /// <param name="objectToSerialize">Item to serialize</param>
        /// <returns>string serialized with passed object</returns>
        public override string Serialize(T objectToSerialize)
        {
            return this.Serialize<T>(objectToSerialize);
        }

        /// <summary>
        /// Serializes the passed object to a string
        /// </summary>
        /// <typeparam name="TSerializeFrom">Type of incoming object</typeparam>
        /// <param name="objectToSerialize">Object to serialize</param>
        /// <returns>Json string</returns>
        public override string Serialize<TSerializeFrom>(TSerializeFrom objectToSerialize)
        {
            string returnValue = TypeExtension.DefaultString;
            MemoryStream stream = new MemoryStream();

            try
            {
                if (objectToSerialize == null && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is null. Unable to serialize null objects."); }
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
                XmlTextWriter textWriter = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
                System.Text.UTF8Encoding Encoding = new System.Text.UTF8Encoding();
                xs.Serialize(stream, objectToSerialize);
                stream = (MemoryStream)textWriter.BaseStream;
                returnValue = Encoding.GetString(stream.ToArray());
            }
            catch
            {
                if (this.ThrowException) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// Deserialize from JSON
        /// </summary>
        /// <param name="stringToDeserialize"></param>
        /// <returns>De-serialized object</returns>
        public override T Deserialize(string stringToDeserialize)
        {
            return this.Deserialize<T>(stringToDeserialize);
        }

        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <typeparam name="TDeserializeTo">Type of outgoing object</typeparam>
        /// <param name="stringToDeserialize">Object to deserialize</param>
        /// <returns>Concrete class</returns>
        public override TDeserializeTo Deserialize<TDeserializeTo>(string stringToDeserialize)
        {
            TDeserializeTo returnValue =  TypeExtension.InvokeConstructorOrDefault<TDeserializeTo>();

            try
            {
                if (stringToDeserialize == TypeExtension.DefaultString && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is empty. Unable to deserialize empty strings."); }
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] byteArray = (byte[])encoding.GetBytes(stringToDeserialize);
                MemoryStream memoryStream = new MemoryStream(byteArray);
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(TDeserializeTo));
                returnValue = (TDeserializeTo)xs.Deserialize(memoryStream);
            }
            catch (System.InvalidOperationException)
            {
                returnValue = (TDeserializeTo)Activator.CreateInstance(typeof(TDeserializeTo));
            }
            catch
            {
                if (this.ThrowException) throw;
            }

            return returnValue;
        }
    }
}
