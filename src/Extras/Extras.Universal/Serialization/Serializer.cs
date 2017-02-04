//-----------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Genesys Source">
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
using Genesys.Extensions;
using Genesys.Extras.Collections;

namespace Genesys.Extras.Serialization
{
    /// <summary>
    /// Generically typed Serialization and Deserialization
    /// </summary>
    public abstract class Serializer : ISerializer
    {
        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        public IListSafe<Type> KnownTypes { get; set; } = new ListSafe<Type>();
        /// <summary>
        /// Setting to throw exception
        /// </summary>
        public bool ThrowException { get; set; } = TypeExtension.DefaultBoolean;
        /// <summary>
        /// Setting to throw exception
        /// </summary>
        public bool EmptyStringAndNullSupported { get; set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Constructor
        /// </summary>
        public Serializer() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Serializer(IListSafe<Type> knownTypes) : this()
        {
            {
                KnownTypes.AddRange(knownTypes);
            }
        }     
          
        /// <summary>
        /// Serializes and returns the object as a string
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public abstract string Serialize<T>(T objectToSerialize);

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="stringToDeserialize"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public abstract T Deserialize<T>(string stringToDeserialize);
    }    
}
