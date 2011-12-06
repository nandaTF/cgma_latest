using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Endeca.Web.UI;

namespace Aicpa.CGMA.SharePoint.Fields
{
    public class CGMADimensionFieldEditor : UserControl
    {
        // Fields
        private CGMADimensionFieldType fldCGMADimensionFieldType;
        public global::System.Web.UI.WebControls.Repeater RptrDimensions;
        
        public void InitializeWithField(SPField field)
        {
            this.fldCGMADimensionFieldType = field as CGMADimensionFieldType;

            if (this.Page.IsPostBack)
            {
                return;
            }
        }
    }

}