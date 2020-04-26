using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using System.Drawing;

namespace ScShoAlpha
{
    public static class ClsImageSave
    {
        // イメージ保存メソッド
        public static void ImageDesktopSaveFile(Image image)
        {
            try
            {
                switch (Settings.Instance.SaveModeType)
                {
                    case DFN.MODE_BMP:
                        {
                            ImageDesktopSaveFileBMP(image);
                            break;
                        }

                    case DFN.MODE_JPEG:
                        {
                            ImageDesktopSaveFileJPEG(image);
                            break;
                        }

                    case DFN.MODE_PNG:
                        {
                            ImageDesktopSaveFilePNG(image);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }
        }

        // BMP保存用
        public static void ImageDesktopSaveFileBMP(Image image)
        {
            var strPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var strFileName = Settings.Instance.SaveFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
            saveImage(0, strPath, image);
        }

        // JPEG保存用
        public static void ImageDesktopSaveFileJPEG(Image image)
        {
            var strPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var strFileName = Settings.Instance.SaveFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            saveImage(1, strPath, image);
        }

        // PNG保存用
        public static void ImageDesktopSaveFilePNG(Image image)
        {
            var strPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var strFileName = Settings.Instance.SaveFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            strPath = strPath + @"\" + strFileName;
            saveImage(2, strPath, image);
        }

        /// <summary>
        /// 画像保存
        /// </summary>
        /// <param name="mode">0:BMP 1:JPEG 2:PNG</param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="image"></param>
        public static void saveImage(int mode, String path, Image image)
        {
            switch (mode)
            {
                case 0:
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 1:
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case 2:
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                default:
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
        }
    }

}
