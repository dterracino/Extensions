//-----------------------------------------------------------------------
// <copyright file="ListExtension.cs" company="Genesys Source">
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

namespace Genesys.Extensions
{
    /// <summary>
    /// List Extension
    /// </summary>
    [CLSCompliant(true)]
    public static class ListExtension
    {
        /// <summary>
        /// Returns first item in a list, or empty constructed class
        /// </summary>
        /// <typeparam name="T">Type of the generic list</typeparam>
        /// <param name="item">List to get first item</param>
        /// <returns>First item, or new() constructed item</returns>
        public static T FirstOrDefaultSafe<T>(this List<T> item) where T : new()
        {
            if ((item == null) == true || item.Any() == false)
            {
                return new T();
            } 
            else
            {
                return item.FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns first item in a list, or empty constructed class
        /// </summary>
        /// <typeparam name="T">Type of the generic list</typeparam>
        /// <param name="item">List to get first item</param>
        /// <param name="defaultValue">DefaultValue if first item is not found</param>
        /// <returns>First item, or DefaultValue</returns>
        public static T FirstOrDefaultSafe<T>(this List<T> item, T defaultValue)
        {
            if ((item == null) == true || item.Any() == false)
            {
                return defaultValue;
            } 
            else
            {
                return item.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// Returns first found item in a list, or empty constructed class.
        /// Exception-safe.
        /// </summary>
        /// <typeparam name="T">Type of generic list.</typeparam>
        /// <param name="item">Item to search.</param>
        /// <param name="index">Index position to search</param>
        /// <returns>Found item or constructed equivalent.</returns>
        public static T Item<T>(this List<T> item, int index) where T : new()
        {
            return item[index].DirectCastSafe<T>();
        }

        /// <summary>
        /// Exception safe Find()
        /// </summary>
        /// <typeparam name="T">Generic type of list</typeparam>
        /// <param name="item">Item to search.</param>
        /// <param name="query">Predicate query to search for data</param>
        /// <returns>Found item in list based on predicate</returns>
        public static T FindSafe<T>(this List<T> item, Predicate<T> query) where T : new()
        {
            return item.Find(query).DirectCastSafe<T>();
        }
        
        /// <summary>
        /// Adds list to current list
        /// </summary>
        /// <typeparam name="T">Type of lists</typeparam>
        /// <param name="item">Destination list</param>
        /// <param name="itemsToAdd">Source list</param>
        public static void AddRange<T>(this List<T> item, List<T> itemsToAdd)
        {
            foreach (T itemToAdd in itemsToAdd)
            {
                item.Add(itemToAdd);
            }
        }
        
        /// <summary>
        /// Returns type of Generic.List
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="_">Item to determine type</param>
        /// <returns>Type of generic list</returns>
        public static Type GetListType<T>(this List<T> _)
        {
            return typeof(T);
        }

        /// <summary>
        /// Returns type of IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="_">Item to determine type</param>
        /// <returns>Type of generic list</returns>>
        public static Type GetEnumerableType<T>(this IEnumerable<T> _)
        {
            return typeof(T);
        }

        /// <summary>
        /// Fills this IEnumerable list with another IEnumerable list that has types with matching properties.
        /// </summary>
        /// <typeparam name="T">Type of original object.</typeparam>
        /// <param name="item">Destination object to fill</param>
        /// <param name="sourceList">Source object</param>
        public static void FillRange<T>(this List<T> item, IEnumerable sourceList) where T : new()
        {
            T newItem = new T();

            foreach (var sourceItem in sourceList)
            {
                newItem = new T();
                newItem.Fill(sourceItem);
                item.Add(newItem);
            }
        }
    }
}
