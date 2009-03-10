using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;

namespace NEnhancer
{
    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        #region Command Names

        private static readonly string COMMAND_VIEWER_COMMAND_NAME = "CommandViewer";
        private static readonly string CLOSE_ALL_DOCUMENTS_COMMAND_NAME = "CloseAllDocuments";
        private static readonly string CODE_CONVERTER_COMMAND_NAME = "CodeConverter";

        #endregion
        
        private DTE2 _applicationObject;
        private AddIn _addInInstance;

        public Connect()
        {
        }

        #region IDTExtensibility2 interface methods

        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;

            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                // Get main menu bar
                CommandBar menuBarCommandBar =
                    ((CommandBars)_applicationObject.CommandBars)["MenuBar"];
                
                #region Add CommandViewer command

                // TODO: So many hard coded strings...
                // Get Tools menu
                string toolsMenuName = GetCulturedMenuName("Tools");
                CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

                // Add a new command
                AddNamedCommand2(toolsPopup.CommandBar, COMMAND_VIEWER_COMMAND_NAME, "CommandViewer",
                    "View All Commands", true, 59, toolsPopup.Controls.Count + 1); 

                #endregion

                #region Add CloseAllDocuments command

                // Get "Easy MDI Document Window" command bar
                CommandBar mdiDocCommandBar = GetCommandBarByName("Easy MDI Document Window");
                // Place the command below "Close All But This" menu item
                CommandBarControl closeAllButThisCmd = mdiDocCommandBar.Controls["Close All But This"];
                int closeAllCmdIndex = (closeAllButThisCmd == null) ? 1 : (closeAllButThisCmd.Index + 1);
                // Add a new command
                AddNamedCommand2(mdiDocCommandBar, CLOSE_ALL_DOCUMENTS_COMMAND_NAME,
                    "Close All Documents", "Close All Documents", false, 0, closeAllCmdIndex); 

                #endregion

                #region Add CodeConverter command

                AddNamedCommand2(toolsPopup.CommandBar, CODE_CONVERTER_COMMAND_NAME, "CodeConverter",
                    "Convert codes between C# and VB.NET", true, 59, toolsPopup.Controls.Count + 1); 

                #endregion
            }
        }

        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }
	
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        public void OnStartupComplete(ref Array custom)
        {
        }

        public void OnBeginShutdown(ref Array custom)
        {
        }

        #endregion

        #region IDTCommandTarget interface methods

        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, 
            ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == GetCommandFullName(COMMAND_VIEWER_COMMAND_NAME))
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
                else if (commandName == GetCommandFullName(CLOSE_ALL_DOCUMENTS_COMMAND_NAME))
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
                else if (commandName == GetCommandFullName(CODE_CONVERTER_COMMAND_NAME))
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
            }
        }

        public void Exec(string commandName, vsCommandExecOption executeOption, 
            ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (commandName == GetCommandFullName(COMMAND_VIEWER_COMMAND_NAME))
                {
                    ShowCmdBarViewer();

                    handled = true;
                    return;
                }
                else if (commandName == GetCommandFullName(CLOSE_ALL_DOCUMENTS_COMMAND_NAME))
                {
                    CloseAllDocuments();

                    handled = true;
                    return;
                }

                else if (commandName == GetCommandFullName(CODE_CONVERTER_COMMAND_NAME))
                {
                    ConvertSolution();

                    handled = true;
                    return;
                }
            }
        }

        #endregion

        #region Helper Methods

        private string GetCulturedMenuName(string englishName)
        {
            string result = englishName;

            try
            {
                string resourceName;
                ResourceManager resourceManager = new ResourceManager("NEnhancer.CommandBar", Assembly.GetExecutingAssembly());
                CultureInfo cultureInfo = new CultureInfo(_applicationObject.LocaleID);

                if (cultureInfo.TwoLetterISOLanguageName == "zh")
                {
                    System.Globalization.CultureInfo parentCultureInfo = cultureInfo.Parent;
                    resourceName = String.Concat(parentCultureInfo.Name, englishName);
                }
                else
                {
                    resourceName = String.Concat(cultureInfo.TwoLetterISOLanguageName, englishName);
                }

                result = resourceManager.GetString(resourceName);
            }
            catch
            {
                result = englishName;
            }

            return result;
        }

        private string GetCommandFullName(string cmdName)
        {
            return "NEnhancer.Connect." + cmdName;
        }

        private CommandBar GetCommandBarByName(string cmdBarName)
        {
            return ((CommandBars)_applicationObject.CommandBars)[cmdBarName];
        }

        private void AddNamedCommand2(CommandBar cmdBar, string cmdName, string buttonText, string toolTip, 
            bool useMsoButton, int iconIndex, int position)
        {
            // Do not try to add commands to a null bar
            if (cmdBar == null) { return; }

            // Get commands collection
            Commands2 commands = (Commands2)_applicationObject.Commands;
            object[] contextGUIDS = new object[] { };

            try
            {
                // Add command
                Command command = commands.AddNamedCommand2(_addInInstance, cmdName, buttonText, toolTip,
                    useMsoButton, iconIndex, ref contextGUIDS, 
                    (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, 
                    (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);
                if (command != null && cmdBar != null)
                {
                    command.AddControl(cmdBar, position);
                }
            }
            catch (ArgumentException)
            {
                // Command already exists, so ignore the exception.
            }
        }

        #endregion

        #region Command Handler

        private void ShowCmdBarViewer()
        {
            CommandBarViewer viewerForm = new CommandBarViewer();
            viewerForm.DTEObject = _applicationObject;
            viewerForm.ShowDialog();
        }

        private void CloseAllDocuments()
        {
            _applicationObject.ExecuteCommand("Window.CloseAllDocuments", string.Empty);
        }

        private void ConvertSolution()
        {
            CodeConverter converter = new CodeConverter();
            converter.DTEObject = _applicationObject;
            converter.ShowDialog();
        }

        #endregion
    }
}