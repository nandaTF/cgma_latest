<%@ Control Language="C#" Inherits="Aicpa.CGMA.SharePoint.Fields.CGMADimensionFieldEditor, Aicpa.CGMA.SharePoint.Fields.CGMADimension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f1d77f0a22f7a42b"    compilationMode="Always" %>

<div class="TopicsManagement">
    <asp:Repeater ID="RptrDimensions" runat="server">
        <HeaderTemplate>
            <b>
                <span>Browse by Topic</span>
                <asp:Literal ID="RptrDimensions_Name" runat="server" visible="false"></asp:Literal></b><br />
        </HeaderTemplate>
        <ItemTemplate>
            <span>
                <asp:HyperLink ID="RptrDimensions_Refinements" runat="server"></asp:HyperLink></span><br />
        </ItemTemplate>
    </asp:Repeater>
</div>
