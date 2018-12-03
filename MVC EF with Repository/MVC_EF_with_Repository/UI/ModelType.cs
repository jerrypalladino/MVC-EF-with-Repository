using System;
using System.Globalization;
using EnvDTE;

namespace MVC_EF_with_Repository.UI
{
    /// <summary>
    /// Wrapper for CodeType so we can use it in the UI.
    /// </summary>
    public class ModelType
    {
        public ModelType(CodeType codeType)
        {
            if (codeType == null)
            {
                throw new ArgumentNullException("codeType");
            }

            CodeType = codeType;
            TypeName = codeType.FullName;
            ShortTypeName = codeType.Name;
            NamespaceParentNameOnly = codeType.Namespace.FullName.Replace(".Models", "");
            DisplayName = (codeType.Namespace == null || String.IsNullOrWhiteSpace(codeType.Namespace.FullName))
                            ? codeType.Name
                            : String.Format(CultureInfo.InvariantCulture, "{0} ({1})", codeType.Name, codeType.Namespace.FullName);
        }

        public CodeType CodeType { get; set; }

        public string DisplayName { get; set; }

        public string TypeName { get; set; }

        public string ShortTypeName { get; set; }

        public string NamespaceParentNameOnly { get; set; }

    }

    public class ContextType
    {
        public ContextType(CodeType codeType)
        {
            if (codeType == null)
            {
                throw new ArgumentNullException("codeType");
            }
            DBContext = codeType.FullName;
            TypeName = codeType.FullName;
            CodeType = codeType;
            ShortTypeName = codeType.Name;

            DisplayName = (codeType.Namespace == null || String.IsNullOrWhiteSpace(codeType.Namespace.FullName))
                            ? codeType.Name
                            : String.Format(CultureInfo.InvariantCulture, "{0} ({1})", codeType.Name, codeType.Namespace.FullName);

        }
        public CodeType CodeType { get; set; }
        public string DBContext { get; set; }
        public string ShortTypeName { get; set; }
        public string DisplayName { get; set; }
        public string TypeName { get; set; }
        public string NamespaceParentNameOnly { get; set; }
    }
}
