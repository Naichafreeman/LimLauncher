﻿using LimLauncher.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LimLauncher.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ShortcutInfo
    {
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string FileFullPath { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get { return System.IO.Path.GetFileNameWithoutExtension(FileFullPath); } }

        [PropertyChanged.AlsoNotifyFor("FileRenameDisp")]
        public string FileRename { get; set; }

        public string FileRenameDisp { get { return string.IsNullOrWhiteSpace(FileRename) ? FileName : FileRename; } }

        private ImageSource FileIconSave { get; set; }
        /// <summary>
        /// 文件图标
        /// </summary>
        public ImageSource FileIcon
        {
            get
            {
                if (FileIconSave == null)
                    FileIconSave = IconManager.FindIconForFilename(FileFullPath, true);
                return FileIconSave;
            }
        }

        public string FileSize { get { return System.IO.Directory.Exists(FileFullPath) ? "" : Common.GetString(new System.IO.FileInfo(FileFullPath).Length); } }

        public string FileTypeDescription { get { return Common.GetFileTypeDescription(FileFullPath); } }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="FileName"></param>
        public void StartFile()
        {
            new System.Threading.Thread(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start(FileFullPath);
                }
                catch { }
            }).Start();
        }
    }
}
