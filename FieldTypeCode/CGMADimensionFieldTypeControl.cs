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
using System.Web.Configuration;


namespace Aicpa.CGMA.SharePoint.Fields
{
    public class CGMADimensionFieldTypeControl : BaseFieldControl
    {
        public CGMADimensionFieldType field;
        CGMADimensionFieldEditor CGMADimensionFieldEditorUC;
        string strNavigateURL = string.Empty;
        string _strSharepointListName = "Topics";
        string _strListHeader = "Topics";
        public string UrlToAppend;
        public string DimensionToAppend;

        public CGMADimensionFieldTypeControl() { }


        public string SharepointListName
        {
            get
            {
                return _strSharepointListName;
            }
            set
            {
                _strSharepointListName = value;
            }
        }

        public string ListHeader
        {
            get
            {
                return _strListHeader;
            }
            set
            {
                _strListHeader = value;
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            //SPListItemCollection spLDimensionsList = SPContext.Current.Site.RootWeb.Lists["Topics"].Items;

            //SPList splDimensions = SPContext.Current.Site.RootWeb.Lists["Topics"];
            SPList splDimensions = SPContext.Current.Site.RootWeb.Lists[SharepointListName];
            SPQuery myQuery = new SPQuery();
            myQuery.Query = "<OrderBy><FieldRef Name='Title'/></OrderBy>";
            SPListItemCollection spLDimensionsList = splDimensions.GetItems(myQuery);

            CGMADimensionFieldEditorUC = (CGMADimensionFieldEditor)this.Parent.Page.LoadControl("~/_CONTROLTEMPLATES/CGMADimension/CGMADimensionFieldEditor.ascx");


            CGMADimensionFieldEditorUC.RptrDimensions.DataSource = spLDimensionsList;
            CGMADimensionFieldEditorUC.RptrDimensions.DataBind();

            ((Literal)(CGMADimensionFieldEditorUC.RptrDimensions).Controls[0].FindControl("RptrDimensions_Name")).Text = ListHeader;


            if (ListHeader.ToLower().Contains("topics"))
            {
                strNavigateURL = "/_catalogs/masterpage/Search.aspx?DT=";
            }
            else if (ListHeader.ToLower().Equals("keywords"))
            {
                strNavigateURL = "/_catalogs/masterpage/Search.aspx?DK=";
                ((HtmlGenericControl)(CGMADimensionFieldEditorUC.FindControl("TopicsManagement"))).Attributes.Add("class", "TopicsManagement Keyword");
            }

            // Add classes for resources landing page
            string surl = SPContext.Current.File.ServerRelativeUrl;
            if (SPContext.Current.ListItem["Page Layout"].ToString().ToLower().Contains("CGMA blank web part page".ToLower()))
            {
                ((HtmlGenericControl)(CGMADimensionFieldEditorUC.FindControl("TopicsManagement"))).Attributes.Add("class", "TopicsManagement TopicsManagementResourcesLanding");
            }

            int lstDimensionsIndex = 0;
            foreach (SPListItem refinement in spLDimensionsList)
            {
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).Text = refinement.Title;
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).NavigateUrl = this.GetNavigateUrl(strNavigateURL, refinement.UniqueId.ToString());//strNavigateURL + refinement.UniqueId.ToString();
                lstDimensionsIndex++;
            }

            this.Controls.Add(CGMADimensionFieldEditorUC);
        }

        private string GetNavigateUrl(string strNavigateURL, string refinement)
        {
            if (this.UrlToAppend == null || this.DimensionToAppend == null)
            {
                this.setQueryStringToAppend();
            }

            return strNavigateURL + refinement + this.UrlToAppend + this.DimensionToAppend;
        }

        private void setQueryStringToAppend()
        {
            SPPropertyBag myWebProperties = SPContext.Current.Site.RootWeb.Properties;
            string[] urlsandtheirtargets = myWebProperties["urlsanddimensions"].Split(',');
            foreach (string urlandtargetcombo in urlsandtheirtargets)
            {
                string[] urlandtaget = urlandtargetcombo.Split('|');
                string surl = SPContext.Current.File.ServerRelativeUrl;
                if (surl.Equals(urlandtaget[0], StringComparison.InvariantCultureIgnoreCase)) //page level match
                {
                    this.UrlToAppend = "&DU=" + urlandtaget[0].Substring(0, urlandtaget[0].IndexOf("/Pages"));
                    this.DimensionToAppend = "&D=" + urlandtaget[1] + "&DN=" + urlandtaget[2];
                    break;
                }
                else
                {
                    if (surl.ToLower().Contains(urlandtaget[0].ToLower())) // site level match
                    {
                        this.UrlToAppend = "&DU=" + urlandtaget[0].Replace("/Pages", "/");
                        this.DimensionToAppend = "&D=" + urlandtaget[1] + "&DN=" + urlandtaget[2];
                        break;
                    }
                }
            }

            if (this.UrlToAppend == null || this.DimensionToAppend == null)
            {
                this.UrlToAppend = string.Empty;
                this.DimensionToAppend = string.Empty;
            }
        }

        protected override void RenderFieldForDisplay(HtmlTextWriter output)
        {
            EnsureChildControls();

            SPList splDimensions = SPContext.Current.Site.RootWeb.Lists[SharepointListName];
            SPQuery myQuery = new SPQuery();
            myQuery.Query = "<OrderBy><FieldRef Name='Title'/></OrderBy>";

            SPListItemCollection spLDimensionsList = splDimensions.GetItems(myQuery);

            CGMADimensionFieldEditorUC.RptrDimensions.DataSource = spLDimensionsList;
            CGMADimensionFieldEditorUC.RptrDimensions.DataBind();

            ((Literal)(CGMADimensionFieldEditorUC.RptrDimensions).Controls[0].FindControl("RptrDimensions_Name")).Text = ListHeader;

            int lstDimensionsIndex = 0;
            foreach (SPListItem refinement in spLDimensionsList)
            {
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).Text = refinement.Title;
                string strRefinementVal = refinement.Title.ToString().Replace("&", "_amp_");
                ((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).NavigateUrl = this.GetNavigateUrl(strNavigateURL, strRefinementVal);
                //((HyperLink)(CGMADimensionFieldEditorUC.RptrDimensions).Items[lstDimensionsIndex].FindControl("RptrDimensions_Refinements")).NavigateUrl = strNavigateURL + HttpUtility.HtmlEncode(refinement.Title);
                lstDimensionsIndex++;
            }
            RenderChildren(output);
        }
    }
}

