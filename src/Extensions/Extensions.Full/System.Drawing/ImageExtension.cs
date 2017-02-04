//-----------------------------------------------------------------------
// <copyright file="ImageExtension.cs" company="Genesys Source">
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Genesys.Extensions
{
    /// <summary>
    /// Extend System.Drawing.Image class
    /// </summary>
    [CLSCompliant(true)]
    public static class ImageExtension
    {
        /// <summary>
        /// 1px x 1px Transparent Image
        /// </summary>
        public static Image ImageEmpty
        {
            get
            {
                Bitmap returnValue = new Bitmap(1, 1);
                returnValue.MakeTransparent();
                return (Image)returnValue;
            }
        }

        /// <summary>
        /// Resizes an image, not allowing smallest edge to go below 1px
        /// </summary>
        /// <param name="item">Original image to resize</param>
        /// <param name="newSize">Desired size</param>
        /// <returns>Resized image</returns>
        public static Image Resize(this Image item, Size newSize)
        {
            Image returnValue = item;
            Size imageSize = item.Size;
            double multBy = 1.01;
            double w = imageSize.Width;
            double h = imageSize.Height;

            while (w < newSize.Width && h < newSize.Height)
            {
                w = imageSize.Width * multBy;
                h = imageSize.Height * multBy;
                multBy = multBy + 0.001;
            }
            while (w > newSize.Width || h > newSize.Height)
            {
                multBy = multBy - 0.001;
                w = imageSize.Width * multBy;
                h = imageSize.Height * multBy;
            }
            if (imageSize.Width < 1)
            {
                imageSize = new Size(imageSize.Width + -imageSize.Width + 1, imageSize.Height - imageSize.Width - 1);
            }
            if (imageSize.Height < 1)
            {
                imageSize = new Size(imageSize.Width - imageSize.Height - 1, imageSize.Height + -imageSize.Height + 1);
            }
            imageSize = new Size(Convert.ToInt32(w), Convert.ToInt32(h));
            returnValue = new Bitmap(item, imageSize);

            return returnValue;
        }

        /// <summary>
        /// Crops an image
        /// </summary>
        /// <param name="item">Image to crop</param>
        /// <param name="width">With of cropping area</param>
        /// <param name="height">Height of cropping area</param>
        /// <param name="x">X of top-left corner of cropping area</param>
        /// <param name="y">Y of top-left corner of cropping area</param>
        /// <returns>Cropped image</returns>
        public static Image Crop(this Image item, int width, int height, int x, int y)
        {
            Image returnValue = item;
            var ms = new MemoryStream();

            using (Bitmap bmp = new Bitmap(width, height))
            {
                bmp.SetResolution(item.HorizontalResolution, item.VerticalResolution);
                using (Graphics Graphic = Graphics.FromImage(bmp))
                {
                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Graphic.DrawImage(item, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                    bmp.Save(ms, item.RawFormat);
                    returnValue = ms.GetBuffer().ToImage();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a Image to a byte array
        /// </summary>
        /// <param name="item">Item to convert to byte array</param>
        /// <returns>Byte array containing image</returns>
        public static byte[] ToBytes(this Image item)
        {
            var returnValue = new MemoryStream();
            if ((item == null == false) && (item.Size.Width > 0 & item.Size.Height > 0))
            {
                item.Save(returnValue, item.RawFormat);
            }
            return returnValue.ToArray();
        }
    }
}
