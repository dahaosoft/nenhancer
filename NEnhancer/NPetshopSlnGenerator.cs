using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using VSLangProj;

namespace NEnhancer
{
    public partial class NPetshopSlnGenerator : AddInForm
    {
        #region Constants

        private const string ExternalBinDirectoryName = "External-bin";

        #endregion

        private string currentSlnPath = string.Empty;

        private string ExternalBinPath
        {
            get
            {
                return Path.Combine(currentSlnPath, ExternalBinDirectoryName);
            }
        }

        public NPetshopSlnGenerator()
        {
            InitializeComponent();
        }

        private void NPetshopSlnGenerater_Load(object sender, EventArgs e)
        {
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateCtrls())
            {
                return;
            }

            try
            {
                FolderBrowserDialog dlgFolder = new FolderBrowserDialog();
                if (dlgFolder.ShowDialog() == DialogResult.OK)
                {
                    currentSlnPath = dlgFolder.SelectedPath;
                    Generate(txtSlnName.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message);
            }
        }

        private void Generate(string slnName)
        {
            Solution3 sln = (DTEObject.Solution as Solution3);
            sln.Create(currentSlnPath, slnName);

            CopyExternalBinFolder(currentSlnPath);

            // Create solution folder
            Project sfProj = sln.AddSolutionFolder(ExternalBinDirectoryName);
            foreach (string file in Directory.GetFiles(ExternalBinPath))
            {
                sfProj.ProjectItems.AddFromFile(file);
            }

            // Create *.Domain Project
            string classLibProjTemplatePath =
                sln.GetProjectTemplate("ClassLibrary.zip", "CSharp");
            string domainProjName = slnName + "." + "Domain";
            sln.AddFromTemplate(classLibProjTemplatePath, Path.Combine(currentSlnPath, domainProjName),
                domainProjName, false);
            Project domainProj = GetProjectByName(sln, domainProjName);
            VSProject vsDomainProj = domainProj.Object as VSProject;
            vsDomainProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.Common.dll"));

            // Create *.Persistence Project
            string persistProjName = slnName + "." + "Persistence";
            sln.AddFromTemplate(classLibProjTemplatePath, Path.Combine(currentSlnPath, persistProjName),
                persistProjName, false);
            Project persistProj = GetProjectByName(sln, persistProjName);
            VSProject vsPersistProj = persistProj.Object as VSProject;
            vsPersistProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.Common.dll"));
            vsPersistProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.DataAccess.dll"));
            vsPersistProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.DataMapper.dll"));
            vsPersistProj.References.AddProject(domainProj);

            // Create *.Service Project
            string serviceProjName = slnName + "." + "Service";
            sln.AddFromTemplate(classLibProjTemplatePath, Path.Combine(currentSlnPath, serviceProjName),
                serviceProjName, false);
            Project serviceProj = GetProjectByName(sln, serviceProjName);
            VSProject vsServiceProj = serviceProj.Object as VSProject;
            vsServiceProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.Common.dll"));
            vsServiceProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.DataAccess.dll"));
            vsServiceProj.References.Add(Path.Combine(ExternalBinPath, "Castle.DynamicProxy.dll"));
            vsServiceProj.References.Add(Path.Combine(ExternalBinPath, "log4net.dll"));
            vsServiceProj.References.AddProject(domainProj);
            vsServiceProj.References.AddProject(persistProj);

            // Create *.Presentation Project
            string presentProjName = slnName + "." + "Presentation";
            sln.AddFromTemplate(classLibProjTemplatePath, Path.Combine(currentSlnPath, presentProjName),
                presentProjName, false);
            Project presentProj = GetProjectByName(sln, presentProjName);
            VSProject vsPresentProj = presentProj.Object as VSProject;
            vsPresentProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.Common.dll"));
            vsPresentProj.References.AddProject(domainProj);
            vsPresentProj.References.AddProject(serviceProj);

            // Create *.Web Project
            string webProjTemplatePath =
                sln.GetProjectTemplate("WebApplicationProject.zip", "CSharp");
            string webProjName = slnName + "." + "Web";
            sln.AddFromTemplate(webProjTemplatePath, Path.Combine(currentSlnPath, webProjName),
                webProjName, false);
            Project webProj = GetProjectByName(sln, webProjName);
            VSProject vsWebProj = webProj.Object as VSProject;
            vsWebProj.References.Add(Path.Combine(ExternalBinPath, "Castle.DynamicProxy.dll"));
            vsWebProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.Common.dll"));
            vsWebProj.References.Add(Path.Combine(ExternalBinPath, "IBatisNet.Common.Logging.Log4Net.dll"));
            vsWebProj.References.Add(Path.Combine(ExternalBinPath, "log4net.dll"));
            vsWebProj.References.AddProject(domainProj);
            vsWebProj.References.AddProject(presentProj);

            webProj.ProjectItems.AddFolder("css", Constants.vsProjectItemKindPhysicalFolder);
            webProj.ProjectItems.AddFolder("images", Constants.vsProjectItemKindPhysicalFolder);
            webProj.ProjectItems.AddFolder("Maps", Constants.vsProjectItemKindPhysicalFolder);
            webProj.ProjectItems.AddFolder("UserControls", Constants.vsProjectItemKindPhysicalFolder);

            webProj.ProjectItems.AddFromFileCopy(
                Path.Combine(GetAddinPath(), @"ibatis-config\dao.config"));
            webProj.ProjectItems.AddFromFileCopy(
                Path.Combine(GetAddinPath(), @"ibatis-config\providers.config"));
            webProj.ProjectItems.AddFromFileCopy(
                Path.Combine(GetAddinPath(), @"ibatis-config\SqlMap.config"));

            sln.SaveAs(slnName);
        }

        private void CopyExternalBinFolder(string slnPath)
        {
            string binDir = Path.Combine(GetAddinPath(), "ibatis-bin");
            string targetDir = ExternalBinPath;
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            // Copy files
            foreach (string file in Directory.GetFiles(binDir))
            {
                string fileName = Path.GetFileName(file);
                File.Copy(file, Path.Combine(targetDir, fileName), true);
            }
        }

        private bool ValidateCtrls()
        {
            if (string.IsNullOrEmpty(txtSlnName.Text.Trim())) { return false; }

            return true;
        }

        // TODO: Move to helper class.
        private string GetAddinPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        // TODO: Move to helper class.
        private Project GetProjectByName(Solution2 sln, string projName)
        {
            foreach (Project p in sln.Projects)
            {
                if (p.Name == projName)
                {
                    return p;
                }
            }

            return null;
        }
    }
}
