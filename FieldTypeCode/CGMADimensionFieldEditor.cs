using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
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
        public bool SortByDate = false;

        public void InitializeWithField(SPField field)
        {
            this.fldCGMADimensionFieldType = field as CGMADimensionFieldType;
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                return;
            }
        }
    }

}