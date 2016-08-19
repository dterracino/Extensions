//-----------------------------------------------------------------------
// <copyright file="TypeExtension.cs" company="Genesys Source">
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Genesys.Extensions
{
	/// <summary>
	/// Extends System.Type
	/// </summary>
    [CLSCompliant(true)]
    public static class  TypeExtension
    {        
        /// <summary>
        /// default() for int
        /// </summary>
        public static readonly int DefaultInteger = -1;
        /// <summary>
        /// default() for Int16
        /// </summary>
        public static readonly short DefaultInt16 = -1;
        /// <summary>
        /// default() for int
        /// </summary>
        public static readonly int DefaultInt32 = -1;
        /// <summary>
        /// default() for Int64
        /// </summary>
        public static readonly long DefaultInt64 = -1;
        /// <summary>
        /// default() for Int16
        /// </summary>
        public static readonly short DefaultShort = -1;
        /// <summary>
        /// default() for unsigned int
        /// </summary>
        public static readonly int DefaultUInteger = 0;
        /// <summary>
        /// default() for Int64
        /// </summary>
        public static readonly long DefaultLong = -1;
        /// <summary>
        /// default() for Guid
        /// </summary>
        public static readonly Guid DefaultGuid = Guid.Empty;       
        /// <summary>
        /// default() for Decimal
        /// </summary>
        public static readonly decimal DefaultDecimal = 0m;
        /// <summary>
        /// default() for Double
        /// </summary>
        public static readonly double DefaultDouble = 0.0;
        /// <summary>
        /// default() for Single
        /// </summary>
        public static readonly float DefaultSingle = 0.0f;
        
        /// <summary>
        /// default() for String
        /// </summary>
        public static readonly string DefaultString = String.Empty;
        /// <summary>
        /// default() for Boolean
        /// </summary>
        public static readonly bool DefaultBoolean = false;
        /// <summary>
        /// default() for Char
        /// </summary>
        public static readonly char DefaultChar = Char.MinValue;
        
        /// <summary>
        /// default() for DateTime
        /// </summary>
        public static readonly DateTime DefaultDate = new DateTime(1900, 01, 01);
        /// <summary>
        /// Generate new date equal to DateTime.UtcNow 
        /// </summary>
        public static DateTime DefaultDateNew() { return DateTime.UtcNow; }
        
        /// <summary>
        /// default() for string Uri
        /// </summary>        
        public static readonly string DefaultUri = "http://localhost:80";
        
        /// <summary>
        /// Default byte array - 0x0
        /// </summary>
        public static readonly byte DefaultByte = 0;
        /// <summary>
        /// Default byte array - 0x0
        /// </summary>
        public static readonly byte[] DefaultBytes = new byte[] { 0, 0, 0, 1 };
        /// <summary>
        /// Default hex - 0x0
        /// </summary>
        public static readonly string DefaultHex = "0x0";

        /// <summary>
        /// Invokes the parameterless constructor. If no parameterless constructor, returns default()
        /// </summary>
        /// <typeparam name="T">Type to invoke</typeparam>
        public static T InvokeConstructorOrDefault<T>()
        {
            T returnValue = default(T);

            if (TypeExtension.HasParameterlessConstructor<T>())
            {
                returnValue = Activator.CreateInstance<T>();
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if type has a parameterless constructor
        /// </summary>
        /// <typeparam name="T">Type to interrogate for parameterless constructor</typeparam>
        /// <returns></returns>
        public static bool HasParameterlessConstructor<T>()
        {
            IEnumerable<ConstructorInfo> constructors = typeof(T).GetTypeInfo().DeclaredConstructors;
            return constructors.Where(x => x.GetParameters().Count() == 0).ToList().Count == 0 ? false : true;
        }
    }
}
