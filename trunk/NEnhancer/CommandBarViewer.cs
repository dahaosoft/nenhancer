using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace NEnhancer
{
    public partial class CommandBarViewer : AddInForm
    {
        public CommandBarViewer()
        {
            InitializeComponent();
        }

        private void CommandBarViewer_Load(object sender, EventArgs e)
        {
            BindCommandBars();
        }

        private void BindCommandBars()
        {
            CommandBars cmdBars = (DTEObject.CommandBars as CommandBars);

            foreach (CommandBar bar in cmdBars)
            {
                TreeNode node = new TreeNode();
                node.Text = bar.Name;
                node.Tag = "bar";
                // Add a dummy node
                node.Nodes.Add("dummyNode");

                tvCommandBars.Nodes.Add(node);
            }

            // TODO: Find node by text, Or sort by name.
            //tvCommandBars.Select();
            //tvCommandBars.SelectedNode = tvCommandBars.Nodes[tvCommandBars.Nodes.Count - 1];

            #region SortedNames

            // TODO:
            //SortedList<string, int> names = new SortedList<string, int>(cmdBars.Count);
            //foreach (CommandBar bar in cmdBars)
            //{
            //    if (!names.ContainsKey(bar.Name))
            //    {
            //        names.Add(bar.Name, bar.Id);
            //    }
            //}

            //foreach (string name in names.Keys)
            //{
            //    TreeNode node = new TreeNode();
            //    node.Text = name;
            //    node.Tag = "bar";
            //    // Add a dummy node
            //    node.Nodes.Add("dummyNode");

            //    tvCommandBars.Nodes.Add(node);
            //}

            #endregion
        }

        private void tvCommandBars_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            string nodeType = node.Tag as string;

            if (nodeType == "bar")
            {
                FillBar(node);
            }
            else if (nodeType == "popup")
            {
                FillPopup(node);
            }
        }

        private void FillBar(TreeNode cmdBarNode)
        {
            cmdBarNode.Nodes.Clear();
            CommandBar bar = (DTEObject.CommandBars as CommandBars)[cmdBarNode.Text];
            foreach (CommandBarControl ctrl in bar.Controls)
            {
                if (!string.IsNullOrEmpty(ctrl.Caption))
                {
                    TreeNode ctrlNode = new TreeNode();
                    ctrlNode.Text = ctrl.Caption.Replace("&", "");

                    if (ctrl is CommandBarPopup)
                    {
                        ctrlNode.Tag = "popup";
                        // Add a dummy node
                        ctrlNode.Nodes.Add("dummyNode");
                    }

                    cmdBarNode.Nodes.Add(ctrlNode);
                }
            }
        }

        private void FillPopup(TreeNode popupNode)
        {
            popupNode.Nodes.Clear();
            CommandBar bar = (DTEObject.CommandBars as CommandBars)[popupNode.Parent.Text];
            CommandBarPopup popup = bar.Controls[popupNode.Text] as CommandBarPopup;

            foreach (CommandBarControl ctrl in popup.Controls)
            {
                if (!string.IsNullOrEmpty(ctrl.Caption))
                {
                    TreeNode ctrlNode = new TreeNode();
                    ctrlNode.Text = ctrl.Caption.Replace("&", "");
                    popupNode.Nodes.Add(ctrlNode);
                }
            }
        }
    }
}
