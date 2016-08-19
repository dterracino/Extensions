//-----------------------------------------------------------------------
// <copyright file="DateRelative.cs" company="Genesys Source">
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

namespace Genesys.Extras.Text
{
    /// <summary>
    /// Relative date and time based on now/utc now.
    /// Responds with english text for relative time lapse
    /// </summary>
    [CLSCompliant(true)]
    public class DateRelative
    {
        private DateTime nowDate = DateTime.UtcNow;
        /// <summary>
        /// DateToCompare
        /// </summary>
        public DateTime DateToCompare { get; private set; } = TypeExtension.DefaultDate;
        /// <summary>
        /// Difference
        /// </summary>
        public TimeSpan Difference { get; private set; } = new TimeSpan(0);
        /// <summary>
        /// RelativeToDate
        /// </summary>
        public string RelativeToDate { get { return this.RelativeDifferenceGet(this.DateToCompare); } private set { } }        
        
        /// <summary>
        /// Constructor
        /// </summary>
        private DateRelative() : base()
        {
            this.nowDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor that accepts date to compare to UtcNow
        /// </summary>
        /// <param name="dateToCompare"></param>
        public DateRelative(DateTime dateToCompare) : this()
        {
            this.DateToCompare = dateToCompare; 
        }
        
        /// <summary>
        /// Determines relative difference between now and date
        /// </summary>
        /// <returns>English language representation of difference</returns>
        public override string ToString()
        {
            return this.RelativeToDate;
        }

        /// <summary>
        /// Helper to get 
        /// </summary>
        /// <param name="compareDate"></param>
        /// <returns></returns>
        private string RelativeDifferenceGet(DateTime compareDate)
        {
            string returnValue = TypeExtension.DefaultString;
            TimeSpan diffTime = compareDate.Subtract(this.nowDate);

            // Determine difference
            if (diffTime.TotalDays >= 365)
                returnValue = String.Concat("on ", this.nowDate.ToString("MMMM d, yyyy"));
            if (diffTime.TotalDays >= 7)
                returnValue = String.Concat("on ", this.nowDate.ToString("MMMM d"));
            else if (diffTime.TotalDays > 1)
                returnValue = String.Format("{0:N0} days ago", diffTime.TotalDays);
            else if (diffTime.TotalDays == 1)
                returnValue = "yesterday";
            else if (diffTime.TotalHours >= 2)
                returnValue = String.Format("{0:N0} hours ago", diffTime.TotalHours);
            else if (diffTime.TotalMinutes >= 60)
                returnValue = "more than an hour ago";
            else if (diffTime.TotalMinutes >= 5)
                returnValue = String.Format("{0:N0} minutes ago", diffTime.TotalMinutes);
            if (diffTime.TotalMinutes >= 1)
                returnValue = "a few minutes ago";
            else
                returnValue = "less than a minute ago";

            // return data
            return returnValue;
        }
    }
}
