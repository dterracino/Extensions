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
            this.loadData();
        }
        
        /// <summary>
        /// Loads content types into this object
        /// </summary>
        protected virtual void loadData()
        {
            // Unknown (similar to application/octet-stream, but avoids chrome throwing exception)
            this.Add(Types.ApplicationUnknown);
            // Image
            this.Add(Types.ImageUnknown);
            this.Add(Types.Bmp, ".bmp");
            this.Add(Types.Gif, ".gif");
            this.Add(Types.Jpg, ".jpg");
            this.Add(Types.Png, ".png");
            this.Add(Types.Tif, ".tif");
            //Documents
            this.Add(Types.Doc, ".doc");
            this.Add(Types.Docx, ".docx");
            this.Add(Types.Pdf, ".pdf");
            //Slide shows
            this.Add(Types.Ppt, ".ppt");
            this.Add(Types.Pptx, ".pptx");
            //Data
            this.Add(Types.Xls, ".xls");
            this.Add(Types.Xlsx, ".xlsx");            
            this.Add(Types.Csv, ".csv");
            this.Add(Types.Json, ".json");
            this.Add(Types.Xml, ".xml");
            this.Add(Types.Txt, ".txt");
            //Compressed Folders
            this.Add(Types.Zip, ".zip");
            //Audio
            this.Add(Types.Ogg, ".ogg");
            this.Add(Types.Mp3, ".mp3");
            this.Add(Types.Wma, ".wma");
            this.Add(Types.Wav, ".wav");
            //Video;
            this.Add(Types.Wmv, ".wmv");
            this.Add(Types.FlashSwf, ".swf");
            this.Add(Types.Avi, ".avi");
            this.Add(Types.Mp4, ".mp4");
            this.Add(Types.Mpeg, ".mpeg");
            this.Add(Types.Qt, ".qt");
        }
        
        /// <summary>
        /// Adds new content type
        /// </summary>
        /// <param name="name"></param>
        public void Add(string name)
        {
            this.Add(new ContentType(name));
        }

        /// <summary>
        /// Adds new content type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        public void Add(string name, string extension)
        {
            this.Add(new ContentType(name, extension));
        }
        
    }
}
