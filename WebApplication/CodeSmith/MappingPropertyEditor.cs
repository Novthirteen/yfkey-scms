using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace com.Sconit.CodeSmith
{
    public class MappingPropertyEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService = null;
        private MappingPropertyEditorForm _mappingPropertyEditorForm = null;

        public MappingPropertyEditor()
            : base()
        {
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    if (_mappingPropertyEditorForm == null)
                    {
                        _mappingPropertyEditorForm = new MappingPropertyEditorForm();
                    }

                    _mappingPropertyEditorForm.Start(editorService, value);
                    editorService.ShowDialog(_mappingPropertyEditorForm);

                    if (_mappingPropertyEditorForm.DialogResult == DialogResult.OK)
                    {
                        MappingProperty mappingProperty = new MappingProperty();
                        mappingProperty.MappingInfoCollection = _mappingPropertyEditorForm.MappingInfoCollection;
                        value = mappingProperty;
                    }
                    else
                    {
                        value = null;
                    }
                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
