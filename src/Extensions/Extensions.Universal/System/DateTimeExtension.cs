//-----------------------------------------------------------------------
// <copyright file="DateTimeExtension.cs" company="Genesys Source">
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

namespace Genesys.Extensions
{
    /// <summary>
    /// Extends DateTIme class
    /// </summary>
    [CLSCompliant(true)]
    public static class DateTimeExtension
    {
        /// <summary>
        /// Tomorrow from date
        /// </summary>
        /// <param name="item">Date to add a day</param>
        /// <returns>DateTime exactly one day from the original date.</returns>
        public static DateTime Tomorrow(this DateTime item)
        {
            return item.AddDays(1);
        }

        /// <summary>
        /// Yesterday from date
        /// </summary>
        /// <param name="item">Date to subtract a day</param>
        /// <returns>DateTime exactly one day in the past from the original date.</returns>
        public static DateTime Yesterday(this DateTime item)
        {
            return item.AddDays(-1);
        }

        /// <summary>
        /// First day of a month
        /// </summary>
        /// <param name="item">DateTime to get the month and year</param>
        /// <returns>1st day of the passed DateTime.</returns>
        public static DateTime FirstDayOfMonth(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, 1);
        }

        /// <summary>
        /// Last day of a month and year
        /// </summary>
        /// <param name="item">DateTime to get the month and year</param>
        /// <returns>Last day of the passed DateTime.</returns>
        public static DateTime LastDayOfMonth(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, DateTime.DaysInMonth(item.Year, item.Month));
        }
        
        /// <summary>
        /// Adds number of weekdays to date (skipping Saturday and Sunday)
        /// </summary>
        /// <param name="item">Item in which to add number of weekdays to.</param>
        /// <param name="weekdays">Number of M-F days to add to this date.</param>
        /// <returns>Date after adding number of weekdays (skipping Saturday and Sunday)</returns>
        public static DateTime AddWeekdays(this DateTime item, int weekdays)
        {
            var sign = weekdays < 0 ? -1 : 1;
            var unsignedDays = Math.Abs(weekdays);
            var weekdaysAdded = 0;
            DateTime returnValue = TypeExtension.DefaultDate;

            while (weekdaysAdded < unsignedDays)
            {
                item = item.AddDays(sign);
                if (item.DayOfWeek != DayOfWeek.Saturday && item.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdaysAdded++;
                }
            }
            returnValue = item;

            return returnValue;
        }
        
        /// <summary>
        /// Age in years based on date to today
        /// </summary>
        /// <param name="item">DateTime to determine age.</param>
        /// <returns>Age in years of the DateTime.</returns>
        public static int Age(this DateTime item)
        {
            var returnValue = 0;

            if (item != TypeExtension.DefaultDate)
            {
                returnValue = DateTime.Today.Year - item.Year;
                if (item.AddYears(returnValue) > DateTime.Today)
                {
                    returnValue = returnValue - 1;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Ensures date is savable in SQL Server
        /// </summary>
        /// <param name="item">DateTime to validate.</param>
        /// <returns>True for if the date can be saved to SQL Server.</returns>
        public static bool IsSavable(this DateTime item)
        {
            DateTime sqlMinimumDate = new DateTime(1753, 01, 01); // Minimum Date SQL Accepts
            DateTime sqlMaximumDate = new DateTime(9999, 01, 01); // Maximum Date SQL Accepts
            bool returnValue = TypeExtension.DefaultBoolean;
            var minResult = DateTime.Compare(item, sqlMinimumDate);
            var maxResult = DateTime.Compare(item, sqlMaximumDate);

            if ((minResult >= 0) & (maxResult <= 0))
            {
                returnValue = true;
            }
            
            return returnValue;
        }
    }
}
