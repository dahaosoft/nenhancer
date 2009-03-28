using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using NEnhancer.Logic.CodeConverter;
using VSLangProj;
using VSLangProj80;

namespace NEnhancer
{
    public partial class CodeConverter : AddInForm
    {
        public CodeConverter()
        {
            InitializeComponent();
        }

        private void CodeConverter_Load(object sender, EventArgs e)
        {
            if (!HasSolution)
            {
                lblMessage.Text = "Solution not opened";
                return;
            }
        }

        private bool HasSolution
        {
            get
            {
                return DTEObject.Solution.IsOpen;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            // Create a sln
            Solution3 newSln = DTEObject.Solution as Solution3;
            newSln.Create(@"C:\solutions", "mySln");

            // Add a proj
            string templatePath = newSln.GetProjectTemplate("ClassLibrary.zip", "CSharp");
            newSln.AddFromTemplate(templatePath, @"C:\solutions\NewProj", "mySln.NewProj", false);
            Project proj = newSln.Projects.Item(1);
            VSProject2 vsProj = proj.Object as VSProject2;
            vsProj.References.Add(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Drawing.dll");

            // Save the sln
            newSln.SaveAs(@"C:\solutions\mySln.sln");
            //newSln.Close(true);

            //ShowSlnInfo();
        }

        private void ShowSlnInfo()
        {
            StringBuilder info = new StringBuilder();

            Solution3 sln = DTEObject.Solution as Solution3;
            SolutionNode sn =
                new SolutionNode { FileName = sln.FileName, FullName = sln.FullName };
            foreach (Project proj in sln.Projects)
            {
                if (proj.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    AddSolutionFolder(sn, proj);
                    //sn.SolutionFolderList.Add();
                }
                else
                {
                    AddProject(sn, proj);
                    //info.AppendLine(proj.FullName);
                }
            }

            //MessageBox.Show(info.ToString());
            Solution3 newSln = DTEObject.Solution as Solution3;
            newSln.Create(@"C:\solutions", "mySln");
            MessageBox.Show(newSln.FullName);
        }

        private void AddProject(SolutionNode sn, Project proj)
        {
            ProjectNode pn = new ProjectNode();
            pn.Name = proj.Name;
            VSProject2 vsProj = proj.Object as VSProject2;

            foreach (Reference reference in vsProj.References)
            {
                ReferenceNode rn = new ReferenceNode();
                rn.Name = reference.Name;
                rn.Path = reference.Path;
                pn.ReferenceList.Add(rn);
            }

            foreach (ProjectItem item in proj.ProjectItems)
            {
                FileNode fn = new FileNode();
                fn.FileName = item.Name;
                //fn.FullName = item.Document.FullName;
                pn.FileList.Add(fn);
            }

            sn.ProjectList.Add(pn);
        }

        private void AddSolutionFolder(SolutionNode sln, Project sf)
        {
            SolutionFolderNode sfn = new SolutionFolderNode();
            sfn.Name = sf.Name;

            foreach (ProjectItem pi in sf.ProjectItems)
            {
                ProjectNode pn = new ProjectNode();
                pn.Name = pi.Name;
                sfn.ProjectList.Add(pn);
            }

            sln.SolutionFolderList.Add(sfn);
        }
    }
}
