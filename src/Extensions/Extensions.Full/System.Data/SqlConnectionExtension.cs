//-----------------------------------------------------------------------
// <copyright file="SqlConnectionExtension.cs" company="Genesys Source">
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Genesys.Extensions
{
    /// <summary>
    /// Extends System.Type
    /// </summary>
    [CLSCompliant(true)]
    public static class SqlConnectionExtension
    {
        /// <summary>
        /// Constructs a connection string from multiple string elements
        /// </summary>
        public static void ConnectionString(this SqlConnection connection, string serverName, string databaseName, int timeoutInSeconds = 3)
        {
            StringBuilder connectionString = new StringBuilder();
            connectionString.Append("Data Source=").Append(serverName).Append(";Initial Catalog=");
            connectionString.Append(databaseName).Append(";Persist Security Info=True;Trusted_connection=Yes;").Append(";Connect Timeout=").Append(timeoutInSeconds);
            connection.ConnectionString = connectionString.ToString();
        }
        
        /// <summary>
        /// Tests a connection to see if can open
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>True if this connection can be opened</returns>
        public static bool CanOpen(this SqlConnection connection)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    returnValue = true;
                    connection.Close();
                }
            }
            catch
            {
                returnValue = false;
            }
            finally
            {
                connection.Dispose();
            }

            return returnValue;
        }
    }
}
