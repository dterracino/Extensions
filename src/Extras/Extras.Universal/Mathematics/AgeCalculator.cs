//-----------------------------------------------------------------------
// <copyright file="AgeCalculator.cs" company="Genesys Source">
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

namespace Genesys.Extras.Mathematics
{
    /// <summary>
    /// Calculates age in days and years
    /// </summary>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class Age
    {
        private DateTime birthDayField = TypeExtension.DefaultDate;
        private DateTime todayField = TypeExtension.DefaultDate;
        private int yearsField = TypeExtension.DefaultInteger;
        private int daysField = TypeExtension.DefaultInteger;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dateToAge">Date that will be used to calculate age</param>
        public Age(DateTime dateToAge)
            : base()
        {
            yearsField = 0;
            daysField = 0;
            birthDayField = dateToAge;
            todayField = DateTime.UtcNow;
        }

        /// <summary>
        /// Age in years
        /// </summary>
        public int Years
        {
            get
            {
                if (birthDayField != TypeExtension.DefaultDate)
                {
                    yearsField = DateTime.Today.Year - birthDayField.Year;
                    if (birthDayField.AddYears(yearsField) > DateTime.Today)
                    {
                        yearsField = yearsField - 1;
                    }
                }
                return yearsField;
            }
        }

        /// <summary>
        /// Age in days
        /// </summary>
        public int Days
        {
            get
            {
                if (birthDayField != TypeExtension.DefaultDate)
                {
                    daysField = todayField.Subtract(birthDayField).Days;
                }
                return daysField;
            }
        }
    }
}
