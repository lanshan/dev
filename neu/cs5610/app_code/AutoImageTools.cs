﻿/*
    File: ~/app_code/AutoImageTools.cs
    
    Copyright 2011,
    Richard Rasala,
    College of Computer and Information Science
    Northeastern University, Boston, MA 02115
    rasala@ccs.neu.edu
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace edu.neu.ccis.rasala
{

    public class AutoImageTools
    {

        /// <summary>
        /// The pseudo file name used to trigger the
        /// auto image IHttpHandler: autoimage.aspx
        /// </summary>
        public const string autoImageName = "autoimage.aspx";

        /// <summary>
        /// The title for any page generated by the
        /// auto image viewer IHttpHandler: Images
        /// </summary>
        public const string autoImageTitle = "Images";

        /// <summary>
        /// The CSS style for the auto image viewer.
        /// </summary>
        public const string autoImageCssStyle =
@"
<style type='text/css'>
    body { font-size:125%; }

    .indent { margin-left:5%; }

    .center { text-align:center; }

    a {
        color: #08f;
        font-weight: bold;
        text-decoration: none;
    }

    a:visited {
        color: #88f;
    }

    a:hover {
        color: #f00;
    }
</style>
";

        /// <summary>
        /// Make the body markup for the AutoImage utility.
        /// </summary>
        /// <param name="context">Web site HttpContext object</param>
        /// <param name="onlyPublic">If true restrict to public subdirectories</param>
        public static string MakeAutoImageMarkup
            (HttpContext context, bool onlyPublic)
        {
            StringBuilder builder = new StringBuilder();

            // path in the file system
            string directoryPath =
                FileTools.GetDirectoryPath(context);

            // path as a tilde web directory path
            string tildeDirectoryPath =
                HttpContextTools.GetTildeDirectoryPath(context);

            List<string> subdirectoryList =
                HttpContextTools.MakeSubdirectoryList(context, onlyPublic);

            List<string> imageList =
                HttpContextTools.MakeFileList(context, FileTools.IMAGE);

            int M = subdirectoryList.Count;
            int N = imageList.Count;

            // create markup

            builder.Append("\n<p><b>Web Directory: ");
            builder.Append(tildeDirectoryPath);
            builder.Append("</b></p>\n");

            if (M > 0)
            {
                builder.Append("\n<p><b>Subdirectories:</b></p>\n");

                MakeSubdirectoryLinks(builder, subdirectoryList);
            }

            builder.Append("\n<p><b>Image Count in this Web Directory: ");
            builder.Append(N);
            builder.Append("</b></p>\n");

            MakeImageLinks(builder, directoryPath, imageList);

            return builder.ToString();
        }


        public static void MakeSubdirectoryLinks
            (StringBuilder builder, List<string> subdirectoryList)
        {
            foreach (string name in subdirectoryList)
                MakeSubdirectoryLink(builder,name);
        }


        public static void MakeSubdirectoryLink
            (StringBuilder builder, string name)
        {
            builder.Append("\n<p class='indent'>\n");
            builder.Append("<a href='");
            builder.Append(name);
            builder.Append("/");
            builder.Append(autoImageName);
            builder.Append("'>");
            builder.Append(name);
            builder.Append("</a>\n");
            builder.Append("</p>\n");
        }


        public static void MakeImageLinks
            (StringBuilder builder, string directory, List<string> imageList)
        {
            foreach (string name in imageList)
                MakeImageLink(builder, directory, name);
        }


        public static void MakeImageLink
            (StringBuilder builder, string directory, string name)
        {
            int width = 0;
            int height = 0;

            string filepath = directory + name;

            using(FileStream stream =
                new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    Bitmap bitmap = new Bitmap(stream);
                    width = bitmap.Width;
                    height = bitmap.Height;
                }
                catch { }
            }

            string namePlusSize = name + ": " + width + " by " + height;

            builder.Append("\n<p class='center'>\n");
            builder.Append("<img src='");
            builder.Append(name);
            builder.Append("' alt='");
            builder.Append(namePlusSize);
            builder.Append("' title='");
            builder.Append(namePlusSize);
            builder.Append("'></img>");
            builder.Append("\n</p>\n");

            builder.Append("\n<p class='center'><b>\n");
            builder.Append(namePlusSize);
            builder.Append("\n</b></p>\n");

            builder.Append("\n<br />\n");
        }


    }
}