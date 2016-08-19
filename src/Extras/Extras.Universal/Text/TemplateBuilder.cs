//-----------------------------------------------------------------------
// <copyright file="TemplateBuilder.cs" company="Genesys Source">
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
using Genesys.Extensions;

namespace Genesys.Extras.Text
{
    /// <summary>
    /// Handles formatting html text, such as filling HTML templates.
    /// </summary>
    [CLSCompliant(true)]
    public class TemplateBuilder
    {        
        private List<String> templateDataField = new List<String>();
        private string templateEmptyField = TypeExtension.DefaultString;
        private string templateFilledField = TypeExtension.DefaultString;
        private bool isHTML = TypeExtension.DefaultBoolean;
        /// <summary>
        /// Setting to re-throw exception
        /// </summary>
        public bool ThrowException { get; set; } = TypeExtension.DefaultBoolean;
        
        /// <summary>
        /// Constructor forcing Immutability
        /// </summary>
        private TemplateBuilder() : base() {
#if (DEBUG)
            this.ThrowException = true;
#endif
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateBuilder(string template, List<String> data)
            : base()
        {
            this.isHTML = true;
            this.templateEmptyField = template;
            this.templateDataField = data;
        }
        
        /// <summary>
        /// Fills template
        /// </summary>
        private void TemplateFill()
        {            
            string dataFormatted = TypeExtension.DefaultString;

            this.IsValid(); // throw exception if bad data
            for (int dataCount = 0; dataCount <= this.templateDataField.Count - 1; dataCount++)
            {
                dataFormatted = this.templateDataField[dataCount].Replace(Environment.NewLine, "<br />").Replace("\n", "<br />");
                this.templateFilledField = this.templateFilledField.Replace("{" + dataCount.ToString() + "}", dataFormatted);
            }
        }
        
        /// <summary>
        /// Returns built HTML template with data
        /// </summary>
        /// <returns></returns>
        public override string ToString() 
        {
            this.TemplateFill();
            return this.templateFilledField;
        }
        
        /// <summary>
        ///  Ensures data can be merged with template
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool returnValue = TypeExtension.DefaultBoolean;
            for(int count = 0; count < this.templateDataField.Count() - 1; count++ )
            {
                if (!this.templateEmptyField.Contains("{" + count + "}")) throw new System.Exception("Error merging template and data. Not enough data to fill the template.");
            }
            return returnValue;
        }
    }
}
