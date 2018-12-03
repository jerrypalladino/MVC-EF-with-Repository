using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVC_EF_with_Repository.UI
{
    /// <summary>
    /// View model for code types so that it can be displayed on the UI.
    /// </summary>
    public class CustomViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The code generation context</param>
        public CustomViewModel(CodeGenerationContext context)
        {
            Context = context;
        }

        /// <summary>
        /// This gets all the Model types from the active project.
        /// </summary>
        public IEnumerable<ModelType> ModelTypes
        {
            get
            {
                ICodeTypeService codeTypeService = (ICodeTypeService)Context
                    .ServiceProvider.GetService(typeof(ICodeTypeService));

                return codeTypeService
                    .GetAllCodeTypes(Context.ActiveProject)
                    .Where(codeType => codeType.IsValidWebProjectEntityType())
                    .Select(codeType => new ModelType(codeType))
                    .OrderBy(codeType => codeType.ShortTypeName);
            }
        }

        public IEnumerable<ContextType> ContextTypes
        {
            get
            {
                ICodeTypeService codeTypeService = (ICodeTypeService)Context
                    .ServiceProvider.GetService(typeof(ICodeTypeService));

                return codeTypeService
                    .GetAllCodeTypes(Context.ActiveProject)
                    .Where(codeType => codeType.Name.Contains("ontext"))
                    .Select(codeType => new ContextType(codeType))
                    .OrderBy(codeType => codeType.ShortTypeName);
            }
        }

        public ContextType SelectedContextlType { get; set; }

        public CodeGenerationContext Context { get; private set; }
    }
}