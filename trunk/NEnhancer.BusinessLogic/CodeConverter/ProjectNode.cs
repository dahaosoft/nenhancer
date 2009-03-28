using System;
using System.Collections.Generic;
using System.Text;

namespace NEnhancer.Logic.CodeConverter
{
    public class ProjectNode
    {
        private IList<FileNode> fileList = new List<FileNode>();
        private IList<ReferenceNode> referenceList = new List<ReferenceNode>();
        private IList<FolderNode> folderList = new List<FolderNode>();

        #region Properties

        public IList<ReferenceNode> ReferenceList
        {
            get
            {
                return referenceList;
            }
        }

        public IList<FolderNode> FolderList
        {
            get
            {
                return folderList;
            }
        }

        public IList<FileNode> FileList
        {
            get
            {
                return fileList;
            }
        }

        public string Name { get; set; }
        public string FullName { get; set; }
        public string FileName { get; set; } 

        #endregion
    }
}
