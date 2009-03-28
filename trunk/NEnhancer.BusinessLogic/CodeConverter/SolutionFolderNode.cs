using System;
using System.Collections.Generic;
using System.Text;

namespace NEnhancer.Logic.CodeConverter
{
    public class SolutionFolderNode
    {
        private IList<ProjectNode> projectList = new List<ProjectNode>();
        private IList<FileNode> fileList = new List<FileNode>();
        
        #region Properties

        public IList<ProjectNode> ProjectList
        {
            get
            {
                return projectList;
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

        #endregion
    }
}
