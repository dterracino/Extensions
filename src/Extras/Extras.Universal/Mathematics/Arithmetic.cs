//-----------------------------------------------------------------------
// <copyright file="Arithmetic.cs" company="Genesys Source">
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

namespace Genesys.Extras.Mathematics
{
    /// <summary>
    /// MathHelper - Read Only Collection
    /// </summary>
    [CLSCompliant(true)]
    public class Arithmetic
    {
        private static object lockObject = new object();
        private static Random random = null;
        /// <summary>
        /// Singleton for random number generator
        /// </summary>
        private static Random RandomGenerator
        {
            get
            {
                if (random == null)
                {
                    lock (lockObject)
                    {
                            random = random ?? new Random();
                    }                    
                }
                return random;
            }
        }


        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="addend1">Addend 1</param>
        /// <param name="addend2">Addend 2</param>
        /// <returns>Sun of addends</returns>
        public static decimal Addition(decimal addend1, decimal addend2)
        {
            return (addend1 + addend2);
        }

        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="minued">Minued to be subtracted</param>
        /// <param name="subtrahend">Subtrahend to subtract</param>
        /// <returns>Difference of values</returns>
        public static decimal Subtraction(decimal minued, decimal subtrahend)
        {
            return (minued - subtrahend);
        }

        /// <summary>
        /// Multiplication
        /// </summary>
        /// <param name="multiplicand">Multiplicand to multiply</param>
        /// <param name="multiplier">Multiplier to multiply</param>
        /// <returns></returns>
        public static decimal Multiply(decimal multiplicand, decimal multiplier)
        {
            return (multiplicand * multiplier);
        }

        /// <summary>
        /// Division
        /// </summary>
        /// <param name="dividend">Dividend to be divided</param>
        /// <param name="divisor">Divisor to divide</param>
        /// <returns>Divided result</returns>
        public static int Divide(int dividend, int divisor)
        {
            int returnValue = TypeExtension.DefaultInteger;
            int Remainder = TypeExtension.DefaultInteger;

            if (divisor > 0)
            {
                returnValue = (dividend / divisor);
            }

            return returnValue;
        }

        /// <summary>
        /// Division
        /// </summary>
        /// <param name="dividend">Dividend to be divided</param>
        /// <param name="divisor">Divisor to divide</param>
        /// <returns>Divided result</returns>
        public static decimal Divide(Decimal dividend, decimal divisor)
        {
            // Local variable
            decimal returnValue = TypeExtension.DefaultDecimal;

            // Perform the divide
            if (divisor > 0)
            {
                returnValue = dividend / divisor;
            }

            return returnValue;
        }

        /// <summary>
        /// Averages a decimal list
        /// </summary>
        /// <param name="lineItems">Items to average</param>
        /// <returns>Divided result</returns>
        public static decimal AverageDecimal(System.Collections.Generic.List<Decimal> lineItems)
        {

            decimal returnValue = TypeExtension.DefaultDecimal;
            decimal Sum = TypeExtension.DefaultDecimal;

            foreach (decimal lineItem in lineItems)
            {
                Sum = Sum + lineItem;
            }
            if (lineItems.Count > 0)
            {
                returnValue = Sum / lineItems.Count;
            }

            return returnValue;
        }

        /// <summary>
        /// Calculates ROI
        /// </summary>
        /// <param name="currentValue">Current total</param>
        /// <param name="totalInvested">Total invested</param>
        /// <returns>Return on investment percentage</returns>
        public static decimal ROI(Decimal currentValue, decimal totalInvested)
        {
            decimal returnValue = TypeExtension.DefaultDecimal;

            // Calculate
            if (totalInvested != TypeExtension.DefaultDecimal)
            {
                returnValue = (currentValue - totalInvested) / totalInvested;
            }

            return returnValue;
        }

        /// <summary>
        /// Generates a random value with the given length
        /// </summary>
        /// <param name="digits">Number of digits to be returned. Only values between 1 and 10 will be accepted</param>
        /// <returns>Random number</returns>
        public static int Random(int digits = 4)
        {
            int returnValue = TypeExtension.DefaultInteger;

            // Handle for Int32 limitation of 2,147,483,647, low 10 digits
            digits = digits < 1 ? 1 : digits > 10 ? 10 : digits;
            int floor = Convert.ToInt32(Math.Pow(10, digits - 1));
            int ceiling = Convert.ToInt32((floor * 10) - 1);
            returnValue = Arithmetic.Random(floor, ceiling);

            return returnValue;
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="minValue">Floor used for random</param>
        /// <param name="maxValue">Ceiling used for random</param>
        /// <returns>Random integer value</returns>
        public static int Random(int minValue, int maxValue)
        {
            int returnValue = TypeExtension.DefaultInteger;
            returnValue = Arithmetic.RandomGenerator.Next(minValue, maxValue);

            return returnValue;
        }

        /// <summary>
        /// Gets Greatest Common Divisor of three numbers
        /// </summary>
        /// <param name="num1">First to get GCD</param>
        /// <param name="num2">Second to get GCD</param>
        /// <returns>Greatest common denominator</returns>
        public static int GCD(int num1, int num2)
        {
            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 = num1 - num2;
                if (num2 > num1)
                    num2 = num2 - num1;
            }

            return num1;
        }

        /// <summary>
        /// Gets Leas Common Multiplier
        /// </summary>
        /// <param name="num1">First to get LCD</param>
        /// <param name="num2">Second to get LCD</param>
        /// <returns>Least common denominator</returns>
        public static int LCM(int num1, int num2)
        {
            return (num1 * num2) / Arithmetic.GCD(num1, num2);
        }

        /// <summary>
        /// Gets new width for changing height
        /// </summary>
        /// <param name="originalWidth">Original items width</param>
        /// <param name="originanHeight">Original items heigth</param>
        /// <param name="newHeight">New height</param>
        /// <returns>Width given original item was resized</returns>
        public static int WidthGet(int originalWidth, int originanHeight, int newHeight)
        {
            int newWidth = TypeExtension.DefaultInteger;
            decimal multiplier = TypeExtension.DefaultDecimal;

            // Height is only specified, have to calculate width
            multiplier = Arithmetic.Divide(newHeight.ToDecimal(), originanHeight.ToDecimal());
            // Resize
            newWidth = Arithmetic.Multiply(originalWidth.ToDecimal(), multiplier).ToInt();

            return newWidth;
        }
    }
}
