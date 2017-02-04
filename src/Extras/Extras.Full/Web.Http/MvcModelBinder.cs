//-----------------------------------------------------------------------
// <copyright file="MvcControllerBase.cs" company="Genesys Source">
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
using Genesys.Extensions;
using System;
using System.Web.Mvc;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// Overrides the MVC DefaultModelBinder
    ///    You’ll need to add the following to the Global.asax Application_Start method to use your custom model binder.
    ///     C#: ModelBinders.Binders.Add(typeof(string), new MvcModelBinder());
    /// </summary>
    /// <remarks></remarks>
    public class MvcModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Binds model to view. 
        /// typeof(string): Null-safe binding for strings, posted models will contain empty string instead of null
        /// </summary>
        /// <param name="controllerContext">Context of controller</param>
        /// <param name="bindingContext">Context of view model binding</param>
        /// <returns>typeof(string): Returns empty string instead of null</returns>
        /// <remarks></remarks>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            dynamic value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            object returnValue = null;
            
            if (value == null == false)
            {
                returnValue = value.AttemptedValue;
                if (bindingContext.ModelType == typeof(string))
                {
                    bindingContext.ModelMetadata.ConvertEmptyStringToNull = false;
                    if (string.IsNullOrEmpty(value.AttemptedValue))
                    {
                        returnValue = TypeExtension.DefaultString;
                    }
                } 
            }

            return returnValue;
        }
    }
}
