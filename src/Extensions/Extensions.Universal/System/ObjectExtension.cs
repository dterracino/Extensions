//-----------------------------------------------------------------------
// <copyright file="ObjectExtension.cs" company="Genesys Source">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Genesys.Extensions
{
    /// <summary>
    /// object Extensions
    /// </summary>
    [CLSCompliant(true)]
    public static class ObjectExtension
    {
        /// <summary>
        /// Gets the string value of an attribute that implements IAttributeValue
        /// </summary>
        /// <typeparam name="TAttribute">Attribute to get the value</typeparam>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns>Value, or passed notFoundValue if not found</returns>
        public static string GetAttributeValue<TAttribute>(this object item, string notFoundValue) where TAttribute : Attribute, IAttributeValue<string>
        {
            return item.GetAttributeValue<TAttribute, string>(notFoundValue);
        }

        /// <summary>
        /// Gets the value of an attribute that implements IAttributeValue
        /// </summary>
        /// <typeparam name="TAttribute">Attribute to get the value</typeparam>
        /// <typeparam name="TValue">Type of the value to be returned</typeparam>
        /// <param name="item">Object containing the attribute</param>
        /// <param name="notFoundValue">Will use this string if no attribute is found</param>
        /// <returns>Value, or passed notFoundValue if not found</returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this object item, TValue notFoundValue) where TAttribute : Attribute, IAttributeValue<TValue>
        {
            TypeInfo itemType = item.GetType().GetTypeInfo();
            TValue returnValue = notFoundValue;

            foreach (object attribute in itemType.GetCustomAttributes(false))
            {
                if ((attribute is TAttribute) == true)
                {
                    returnValue = ((TAttribute)attribute).Value;
                    break;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Get list of properties decorated with the passed attribute
        /// </summary>
        /// <param name="item"></param>
        /// <param name="myAttribute"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute(this object item, Type myAttribute)
        {
            TypeInfo itemType = item.GetType().GetTypeInfo();

            var returnValue = itemType.DeclaredProperties.Where(
                p => p.GetCustomAttributes(myAttribute, false).Any() == true);

            return returnValue;
        }

        /// <summary>
        /// Safe Type Casting based on .Net default() method
        /// </summary>
        /// <typeparam name="T">default(DestinationType)</typeparam>
        /// <param name="item">Item to default.</param>
        /// <returns>default(DestinationType)</returns>
        public static T DefaultSafe<T>(this object item)
        {
            T returnValue = TypeExtension.InvokeConstructorOrDefault<T>();

            try
            {
                if ((item == null) == false)
                {
                    return (T)new Object();
                }
            }
            catch
            {
                returnValue = TypeExtension.InvokeConstructorOrDefault<T>();
            }
            return returnValue;
        }

        /// <summary>
        /// Safe Type Casting based on TypeExtension.Default{Type} conventions
        /// </summary>
        /// <typeparam name="T">Type to default, or create new()</typeparam>
        /// <param name="item">Item to cast</param>
        /// <returns>Defaulted type, or created new()</returns>
        public static T DirectCastSafe<T>(this object item) where T : new()
        {
            T returnValue = new T();

            try
            {
                if ((item == null) == false)
                {
                    returnValue = (T)item;
                }
            }
            catch
            {
                returnValue = new T();
            }
            return returnValue;
        }

        /// <summary>
        /// Item to exception-safe cast to string
        /// </summary>
        /// <param name="item">Item to cast</param>
        /// <returns>Converted string, or ""</returns>
        public static string ToStringSafe(this object item)
        {
            string returnValue = TypeExtension.DefaultString;

            if (item == null == false)
            {
                returnValue = item.ToString();
            }

            return returnValue;
        }


        /// <summary>
        /// Fills this object with another object's data, by matching property names
        /// </summary>
        /// <typeparam name="T">Type of original object.</typeparam>
        /// <param name="item">Destination object to fill</param>
        /// <param name="sourceItem">Source object</param>
        public static void Fill<T>(this T item, object sourceItem)
        {
            Type sourceType = sourceItem.GetType();

            foreach (PropertyInfo sourceProperty in sourceType.GetRuntimeProperties())
            {
                PropertyInfo destinationProperty = typeof(T).GetRuntimeProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.CanWrite == true)
                {
                    // Copy data only for Primitive-ish types including Value types, Guid, String, etc.
                    Type destinationPropertyType = destinationProperty.PropertyType;
                    if (destinationPropertyType.GetTypeInfo().IsPrimitive || destinationPropertyType.GetTypeInfo().IsValueType
                        || (destinationPropertyType == typeof(string)) || (destinationPropertyType == typeof(Guid)))
                    {
                        destinationProperty.SetValue(item, sourceProperty.GetValue(sourceItem, null), null); 
                    }                    
                }
            }
        }

        /// <summary>
        /// Initialize all Root properties of an object to TypeExtension.Default* conventions
        /// </summary>
        /// <typeparam name="ObjectType">Type of object to initialize</typeparam>
        /// <param name="Item">Item to initialize</param>
        /// <returns>Initialized object to TypeExtension.Default* conventions</returns>
        public static ObjectType Initialize<ObjectType>(this Object Item)
        {
            // Initialize
            Type CurrentObjectType = Item.GetType();

            // Loop through all new item's properties
            foreach (PropertyInfo CurrentProperty in CurrentObjectType.GetRuntimeProperties())
            {
                try
                {
                    // Copy the data using reflection
                    if (CurrentProperty.CanWrite == true)
                    {
                        if (CurrentProperty.PropertyType.Equals(typeof(Int32)) || CurrentProperty.PropertyType.Equals(typeof(int)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Int32>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<int>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultInt32, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(Int64)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Int64>)) || CurrentProperty.PropertyType.Equals(typeof(long)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<long>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultDouble, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(Double)) || CurrentProperty.PropertyType.Equals(typeof(double)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Double>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<double>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultDouble, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(Decimal)) || CurrentProperty.PropertyType.Equals(typeof(decimal)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Decimal>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<decimal>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultDecimal, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(String)) || CurrentProperty.PropertyType.Equals(typeof(string)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultString, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(Char)) || CurrentProperty.PropertyType.Equals(typeof(char)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Char>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<char>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultChar, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(Guid)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Guid>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultGuid, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(Boolean)) || CurrentProperty.PropertyType.Equals(typeof(bool)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<Boolean>)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<bool>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultBoolean, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(DateTime)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<DateTime>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultDate, null);
                        } else if (CurrentProperty.PropertyType.Equals(typeof(TimeSpan)) || CurrentProperty.PropertyType.Equals(typeof(Nullable<TimeSpan>)))
                        {
                            CurrentProperty.SetValue(Item, TypeExtension.DefaultDate, null);
                        } else if (CurrentProperty.GetValue(Item, null) == null)
                        {
                            Type PropType = CurrentProperty.PropertyType;
                            object NewProp = Activator.CreateInstance(PropType);
                            CurrentProperty.SetValue(Item, NewProp, null);
                        }
                    }
                }
                catch
                {
                    // do nothing, cant handle
                }
            }

            // Return data
            return (ObjectType)Item;
        }
    }
}
