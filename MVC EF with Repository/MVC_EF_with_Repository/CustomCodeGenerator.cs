using MVC_EF_with_Repository.UI;
using Microsoft.AspNet.Scaffolding;
using System.Collections.Generic;
using System;

namespace MVC_EF_with_Repository
{
    public class CustomCodeGenerator : CodeGenerator
    {
        CustomViewModel _viewModel;
        SelectModelWindow _window;

        /// <summary>
        /// Constructor for the custom code generator
        /// </summary>
        /// <param name="context">Context of the current code generation operation based on how scaffolder was invoked(such as selected project/folder) </param>
        /// <param name="information">Code generation information that is defined in the factory class.</param>
        public CustomCodeGenerator(
            CodeGenerationContext context,
            CodeGeneratorInformation information)
            : base(context, information)
        {
            _viewModel = new CustomViewModel(Context);
        }


        /// <summary>
        /// Any UI to be displayed after the scaffolder has been selected from the Add Scaffold dialog.
        /// Any validation on the input for values in the UI should be completed before returning from this method.
        /// </summary>
        /// <returns></returns>
        public override bool ShowUIAndValidate()
        {
            // Bring up the selection dialog and allow user to select a model type
            _window = new SelectModelWindow(_viewModel);
            bool? showDialog = _window.ShowDialog();
            return showDialog ?? false;
        }

        /// <summary>
        /// This method is executed after the ShowUIAndValidate method, and this is where the actual code generation should occur.
        /// In this example, we are generating a new file from t4 template based on the ModelType selected in our UI.
        /// </summary>
        public override void GenerateCode()
        {
            //SelectModelWindow window = new SelectModelWindow(_viewModel);
            var _dbContextType = _viewModel.SelectedContextlType.CodeType;
            var _selectedModelTypes = _window.LstModels.SelectedItems;
            // Get each selected code type
            foreach (ModelType item in _selectedModelTypes)
            {
                var codeType = item.CodeType;
                var _namespace = item.NamespaceParentNameOnly;

                // Setup the scaffolding item creation parameters to be passed into the T4 template.
                var parameters = new Dictionary<string, object>()
                {
                    { "ModelType", codeType },
                    { "NamespaceParentNameOnly", _namespace },
                    { "DBContextName", _dbContextType.Name }                
                    //You can pass more parameters after they are defined in the template
                };

                this.AddFileFromTemplate(Context.ActiveProject,
                    "Repositories\\" + codeType.Name + "Repository",
                    //codeType.Name + "Repository",
                    "Repository",
                    parameters, skipIfExists: true);

                this.AddFileFromTemplate(Context.ActiveProject,
                    "Controllers\\" + codeType.Name + "Controller",
                    // codeType.Name + "Controller",
                    "ControllerWithRepository",
                    parameters, skipIfExists: true);

                //if (_window.Create.IsChecked == true)
                //{
                //    this.AddFolder(Context.ActiveProject, "Views\\" + codeType.Name);
                //    try
                //    {
                //        this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\_CreateOrEdit.cshtml", "_CreateOrEdit", parameters, skipIfExists: true);
                //        //this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\Create.cshtml", "Create", parameters, skipIfExists: true);
                //    }
                //    catch (Exception e)
                //    {

                //        Console.WriteLine(e.Message);
                //    }

                //}

                //if (_window.Delete.IsChecked == true)
                //{
                //    this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\Delete.cshtml", "Delete", parameters, skipIfExists: true);
                //}

                //if (_window.Edit.IsChecked == true)
                //{
                //    this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\_CreateOrEdit.cshtml", "_CreateOrEdit", parameters, skipIfExists: true);
                //    this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\Edit.cshtml", "Edit", parameters, skipIfExists: true);
                //}

                //if (_window.Details.IsChecked == true)
                //{
                //    this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\Details.cshtml", "Details", parameters, skipIfExists: true);
                //}

                //if (_window.Index.IsChecked == true)
                //{
                //    this.AddFileFromTemplate(Context.ActiveProject, "Views\\" + codeType.Name + "\\Index.cshtml", "Index", parameters, skipIfExists: true);
                //}

                // ContextType dbContextName = (ContextType)_window.cboDBContext.SelectedValue;
                this.AddClassMemberFromTemplate(Context.ActiveProject, _dbContextType, "DBContext", parameters, item.CodeType.Name);
            }
        }


    }
}
