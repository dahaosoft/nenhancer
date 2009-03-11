using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NEnhancer.BusinessLogic.CodeConverter
{
    public class SolutionNode
    {
        private IList<ProjectNode> projectList = new List<ProjectNode>();
        private IList<SolutionFolderNode> solutionFolderList = new List<SolutionFolderNode>();

        #region Properties

        public IList<ProjectNode> ProjectList
        {
            get
            {
                return projectList;
            }
        }

        public IList<SolutionFolderNode> SolutionFolderList
        {
            get
            {
                return solutionFolderList;
            }
        }

        public string FullName { get; set; }
        public string FileName { get; set; }

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FullName);
            }
        }

        #endregion
    }
}
