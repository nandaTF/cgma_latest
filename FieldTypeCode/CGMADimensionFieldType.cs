using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Aicpa.CGMA.SharePoint.Fields
{
    public class CGMADimensionFieldType : SPFieldText
    {
        public CGMADimensionFieldType(SPFieldCollection fields, string fieldName)
            : base(fields, fieldName)
        {
            //InitProperties();
        }

        public CGMADimensionFieldType(SPFieldCollection fields, string typeName, string displayName)
            : base(fields, typeName, displayName)
        {
            //InitProperties();
        }

        public override BaseFieldControl FieldRenderingControl
        {
            get
            {
                BaseFieldControl fieldControl = new CGMADimensionFieldTypeControl();

                fieldControl.FieldName = InternalName;

                return fieldControl;
            }
        }
    }

}
