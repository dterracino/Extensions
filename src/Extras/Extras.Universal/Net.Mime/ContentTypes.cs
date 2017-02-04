//-----------------------------------------------------------------------
// <copyright file="ContentTypes.cs" company="Genesys Source">
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

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Extends Multipurpose Internet Mail Exchange (MIME) headers
    /// HTML content types
    /// </summary>
    /// <remarks></remarks>
    [CLSCompliant(true)]
    public class ContentTypes : List<ContentType>
    {
        /// <summary>
        /// Content Type Names as constants
        /// </summary>
        public struct Types
        {
            /// <summary>
            /// Unknown (similar to application/octet-stream, but avoids chrome throwing exception)
            /// </summary>
            public const string ApplicationUnknown = "application/unknown";
            /// <summary>
            /// Image
            /// </summary>
            public const string ImageUnknown = "image/unknown";
            /// <summary>
            /// Image
            /// </summary>
            public const string Bmp = "image/bmp";
            /// <summary>
            /// Image
            /// </summary>
            public const string Gif = "image/gif";
            /// <summary>
            /// Image
            /// </summary>
            public const string Jpg = "image/jpeg";
            /// <summary>
            /// Image
            /// </summary>
            public const string Png = "image/png";
            /// <summary>
            /// Image
            /// </summary>
            public const string Tif = "image/tiff";
            /// <summary>
            /// Documents
            /// </summary>
            public const string Doc = "application/msword";
            /// <summary>
            /// Documents
            /// </summary>
            public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Documents
            /// </summary>
            public const string Pdf = "application/pdf";
            /// <summary>
            /// Slideshows
            /// </summary>
            public const string Ppt = "application/vnd.ms-powerpoint";
            /// <summary>
            /// Slideshows
            /// </summary>
            public const string Pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            /// <summary>
            /// Data
            /// </summary>
            public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            /// <summary>
            /// Data
            /// </summary>
            public const string Xls = "application/vnd.ms-excel";
            /// <summary>
            /// Data: Json text
            /// </summary>
            public const string Json = "application/json";
            /// <summary>
            /// Data: JsonP with callback
            /// </summary>
            public const string JsonP_Javascript = "application/javascript";
            /// <summary>
            /// Compressed
            /// </summary>
            public const string Zip = "application/zip";
            /// <summary>
            /// Audio
            /// </summary>
            public const string Ogg = "application/ogg";
            /// <summary>
            /// Data
            /// </summary>
            public const string Csv = "text/csv";
            /// <summary>
            /// Data
            /// </summary>
            public const string Xml = "text/xml";
            /// <summary>
            /// Data
            /// </summary>
            public const string Txt = "text/plain";
            /// <summary>
            /// Audio
            /// </summary>
            public const string Mp3 = "audio/mpeg";
            /// <summary>
            /// Audio
            /// </summary>
            public const string Wma = "audio/x-ms-wma";
            /// <summary>
            /// Audio
            /// </summary>
            public const string Wav = "audio/x-wav";
            /// <summary>
            /// Video
            /// </summary>
            public const string Wmv = "audio/x-ms-wmv";
            /// <summary>
            /// Video
            /// </summary>
            public const string FlashSwf = "application/x-shockwave-flash";
            /// <summary>
            /// Video
            /// </summary>
            public const string Avi = "video/avi";
            /// <summary>
            /// Video
            /// </summary>
            public const string Mp4 = "video/mp4";
            /// <summary>
            /// Video
            /// </summary>
            public const string Mpeg = "video/mpeg";
            /// <summary>
            /// Video
            /// </summary>
            public const string Qt = "video/quicktime";
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ContentTypes()
            : base()
        {
            loadData();
        }
        
        /// <summary>
        /// Loads content types into this object
        /// </summary>
        protected virtual void loadData()
        {
            // Unknown (similar to application/octet-stream, but avoids chrome throwing exception)
            Add(Types.ApplicationUnknown);
            // Image
            Add(Types.ImageUnknown);
            Add(Types.Bmp, ".bmp");
            Add(Types.Gif, ".gif");
            Add(Types.Jpg, ".jpg");
            Add(Types.Png, ".png");
            Add(Types.Tif, ".tif");
            //Documents
            Add(Types.Doc, ".doc");
            Add(Types.Docx, ".docx");
            Add(Types.Pdf, ".pdf");
            //Slide shows
            Add(Types.Ppt, ".ppt");
            Add(Types.Pptx, ".pptx");
            //Data
            Add(Types.Xls, ".xls");
            Add(Types.Xlsx, ".xlsx");            
            Add(Types.Csv, ".csv");
            Add(Types.Json, ".json");
            Add(Types.Xml, ".xml");
            Add(Types.Txt, ".txt");
            //Compressed Folders
            Add(Types.Zip, ".zip");
            //Audio
            Add(Types.Ogg, ".ogg");
            Add(Types.Mp3, ".mp3");
            Add(Types.Wma, ".wma");
            Add(Types.Wav, ".wav");
            //Video;
            Add(Types.Wmv, ".wmv");
            Add(Types.FlashSwf, ".swf");
            Add(Types.Avi, ".avi");
            Add(Types.Mp4, ".mp4");
            Add(Types.Mpeg, ".mpeg");
            Add(Types.Qt, ".qt");
        }
        
        /// <summary>
        /// Adds new content type
        /// </summary>
        /// <param name="name"></param>
        public void Add(string name)
        {
            Add(new ContentType(name));
        }

        /// <summary>
        /// Adds new content type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        public void Add(string name, string extension)
        {
            Add(new ContentType(name, extension));
        }
        
    }
}
