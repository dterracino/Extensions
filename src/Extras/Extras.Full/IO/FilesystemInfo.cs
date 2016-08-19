//***************************************************************************
//*   AppSettingSafe
//*   -------------------
//*   Copyright (c) vGo, Inc.
//*
//*   All rights are reserved. Reproduction or transmission in whole or in part, in
//*   any form or by any means, electronic, mechanical or otherwise, is prohibited
//*   without the prior written consent of the copyright owner.
//* 
//***************************************************************************
#region Using
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vgo.Framework.Extensions;
#endregion

namespace Vgo.Framework.IO
{
    /// <summary>
    /// Search a set of paths on a drive for a folder. 
    ///     Configure with auto search parent and children a certain levels in.
    /// </summary>
    public class FileSearcher
    {
        #region Fields
        private DriveInfo DriveField = new DriveInfo(@"C:\\");
        private List<DirectoryInfo> PathsField = new List<DirectoryInfo>();
        private List<FileInfo> FoundFilesField = new List<FileInfo>();
        #endregion

        #region Properties
        public DriveInfo Drive { get { return DriveField; } }
        public IEnumerable<DirectoryInfo> Paths { get { return PathsField; } }
        public String FileNameOrMask { get; private set; } = TypeExtension.DefaultString;
        public Int32 ParentLevels { get; private set; } = TypeExtension.DefaultInt32;
        public IEnumerable<FileInfo> FoundFiles { get; private set; } = new List<FileInfo>();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public FileSearcher() : base() { this.FileNameOrMask = "*.*"; this.ParentLevels = 0; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DriveToSearch"></param>
        public FileSearcher(String DriveToSearch) : this()
        {
            this.DriveField = new DriveInfo(DriveToSearch);

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DriveToSearch"></param>
        public FileSearcher(String DriveToSearch, List<DirectoryInfo> PathsToSearch, String FileOrMaskToSearch) : this(DriveToSearch)
        {
            this.PathsField.AddRange(PathsToSearch);
            this.FileNameOrMask = FileOrMaskToSearch;
        }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        public IEnumerable<FileInfo> Search()
        {
            // Local variables
            List<FileInfo> ReturnValue = new List<FileInfo>();
            DirectoryInfo Root = Drive.RootDirectory;

            // Complete initialization/setup
            this.Initialize();
            // Perform the search
            foreach (DirectoryInfo Item in this.Paths)
            {
                this.FoundFilesField.AddRange(Item.GetFiles(this.FileNameOrMask));
            }

            // Return data
            return this.FoundFiles;
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Adds to path collection based on parent/child levels to look up or down
        /// </summary>
        private void Initialize()
        {
            // Add parents
            foreach (DirectoryInfo Item in this.Paths)
            {
                for (Int32 Count = 0; Count < this.ParentLevels; Count++)
                {
                    this.PathsField.Add(Item.Parent);
                }
            }
        }
        #endregion
    }

    #region Sytem.IO Reference
    // Path
    //Path.IsPathRooted(path3)
    //Path.HasExtension(path1)
    //Path.GetFullPath(path3)
    //Path.GetTempPath()
    //Path.GetTempFileName()

    // DriveInfo
    //DriveInfo drive.RootDirectory;
    //drive.TotalFreeSpace
    //drive.VolumeLabel

    // Dir
    //DirectoryInfo drive.RootDirectory directory.Attributes.ToString())
    //System.IO.DirectoryInfo[] dirInfos = dirInfo.GetDirectories("*.*");
    //foreach (System.IO.DirectoryInfo d in dirInfos)
    //{
    //    Console.WriteLine(d.Name);
    //}
    // Get the current application directory.
    //string currentDirName = System.IO.Directory.GetCurrentDirectory();

    // File
    //FileInfo[] fileNames = directory.GetFiles("*.*");
    //foreach (System.IO.FileInfo file in fileNames)
    //{
    //    Console.WriteLine("{0}: {1}: {2}", file.Name, file.LastAccessTime, file.Length);
    //}

    // String based file searching
    // Get an array of file names as strings rather than FileInfo objects.
    // Use this method when storage space is an issue, and when you might
    // hold on to the file name reference for a while before you try to access
    // the file.
    //string[] files = System.IO.Directory.GetFiles(currentDirName, "*.txt");
    //        foreach (string s in files)
    //        {
    //            // Create the FileInfo object only when needed to ensure
    //            // the information is as current as possible.
    //            System.IO.FileInfo fi = null;
    //            try
    //            {
    //                fi = new System.IO.FileInfo(s);
    //            }
    //            catch (System.IO.FileNotFoundException e)
    //            {
    //                // To inform the user and continue is
    //                // sufficient for this demonstration.
    //                // Your application may require different behavior.
    //                Console.WriteLine(e.Message);
    //                continue;
    //            }
    //            Console.WriteLine("{0} : {1}", fi.Name, fi.Directory);
    //        }

    //public String Parent(Int32 Levels = 1)
    //    {
    //        // Local variables
    //        String ReturnValue = TypeExtension.DefaultString;

    //        for (Int32 Count = 0; Count < Levels; Count++)
    //        {
    //            ReturnValue += "..\\";
    //        }
    //        ReturnValue += this.CurrentPath;

    //        // Return data
    //        return ReturnValue;
    //    }

    //public static void CreateIfDoesntExist(String Path = "")
    //{
    //    //// Change the directory. In this case, first check to see
    //    //// whether it already exists, and create it if it does not.
    //    //// If this is not appropriate for your application, you can
    //    //// handle the System.IO.IOException that will be raised if the
    //    //// directory cannot be found.
    //    //if (!System.IO.Directory.Exists(@"C:\Users\Public\TestFolder\"))
    //    //{
    //    //    System.IO.Directory.CreateDirectory(@"C:\Users\Public\TestFolder\");
    //    //}

    //    //System.IO.Directory.SetCurrentDirectory(@"C:\Users\Public\TestFolder\");

    //    //currentDirName = System.IO.Directory.GetCurrentDirectory();
    //    //Console.WriteLine(currentDirName);
    //}
    #endregion
}
