<%@ Control Language="C#" Inherits="Aicpa.CGMA.SharePoint.Fields.CGMADimensionFieldEditor, Aicpa.CGMA.SharePoint.Fields.CGMADimension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f1d77f0a22f7a42b"    compilationMode="Always" %>

<div id="TopicsManagement" runat="server" class="TopicsManagement">
    <asp:Repeater ID="RptrDimensions" runat="server">
        <HeaderTemplate>
            <b>
            <div id="Topics">
                <asp:Literal ID="RptrDimensions_Name" runat="server"></asp:Literal>
            </div>
            </b>
        </HeaderTemplate>
        <ItemTemplate>
            <div id="TopicsValue">
                <asp:HyperLink ID="RptrDimensions_Refinements" runat="server"></asp:HyperLink>
            </div></br>
        </ItemTemplate>
    </asp:Repeater>
</div>
