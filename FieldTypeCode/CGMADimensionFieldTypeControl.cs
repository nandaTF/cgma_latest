using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Endeca.Data;
using Endeca.Data.Provider;
using Endeca.Web;
using Endeca.Web.UI;
using Endeca.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
        

namespace Aicpa.CGMA.SharePoint.Fields
{
    public class CGMADimensionFieldTypeControl : BaseFieldControl
    {
        public CGMADimensionFieldType field;
        CGMADimensionFieldEditor CGMADimensionFieldEditorUC;

        public CGMADimensionFieldTypeControl() { }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            //SPListItemCollection spLDimensionsList = SPContext.Current.Site.RootWeb.Lists["Topics"].Items;

            SPList splDimensions = SPContext.Current.Site.RootWeb.Lists["Topics"];
            SPQuery myQuery = new SPQuery();
            myQuery.Query = "<OrderBy><FieldRef Name='Title'/></OrderBy>"; 
            SPListItemCollection spLDimensionsList = splDimensions.GetItems(myQuery);
            
            CGMADimensionFieldEditorUC = (CGMADimensionFieldEditor)this.Parent.Page.LoadControl("~/_CONTROLTEMPLATES/CGMADimension/CGMADimensionFieldEditor.ascx");
            
            
            CGMADimensionFieldEditorUC.RptrDimensions.DataSource = spLDimensionsList;
            CGMADimensionFieldEditorUC.RptrDimensions.DataBind();

            ((Literal)(CGMADimensionFieldEditorUC.RptrDimensions).Controls[0].FindControl("RptrDimensions_Name")).Text = spLDimensionsList.List.Title;

            int lstDimensionsIndex = 0;
            foreach (SPListItem refinement in spLDimensionsList)
            {
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).Text = refinement.Title;
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).NavigateUrl = "/_catalogs/masterpage/Search.aspx?DT=" + HttpUtility.HtmlEncode(refinement.UniqueId.ToString());
                lstDimensionsIndex++;
            }
            
            this.Controls.Add(CGMADimensionFieldEditorUC);        
        }

        protected override void RenderFieldForDisplay(HtmlTextWriter output)
        {
            EnsureChildControls();

            SPList splDimensions = SPContext.Current.Site.RootWeb.Lists["Topics"];
            SPQuery myQuery = new SPQuery();
            myQuery.Query = "<OrderBy><FieldRef Name='Title'/></OrderBy>";
            
            SPListItemCollection spLDimensionsList = splDimensions.GetItems(myQuery);

            CGMADimensionFieldEditorUC.RptrDimensions.DataSource = spLDimensionsList;
            CGMADimensionFieldEditorUC.RptrDimensions.DataBind();

            ((Literal)(CGMADimensionFieldEditorUC.RptrDimensions).Controls[0].FindControl("RptrDimensions_Name")).Text = spLDimensionsList.List.Title;

            int lstDimensionsIndex = 0;
            foreach (SPListItem refinement in spLDimensionsList)
            {
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).Text = refinement.Title;
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).NavigateUrl = "/_catalogs/masterpage/Search.aspx?DT=" + HttpUtility.HtmlEncode(refinement.Title);
                lstDimensionsIndex++;
            }
            RenderChildren(output);
        }
    }
}

