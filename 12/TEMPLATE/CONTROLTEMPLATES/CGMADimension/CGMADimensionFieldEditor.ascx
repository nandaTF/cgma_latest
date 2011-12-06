<%@ Control Language="C#" Inherits="Aicpa.CGMA.SharePoint.Fields.CGMADimensionFieldEditor, Aicpa.CGMA.SharePoint.Fields.CGMADimension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f1d77f0a22f7a42b"    compilationMode="Always" %>

<div class="TopicsManagement">
    <ul class="TopicsList">
        <asp:Repeater ID="RptrDimensions" runat="server">
            <HeaderTemplate>
                <asp:Literal ID="RptrDimensions_Name" runat="server"></asp:Literal>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:LinkButton ID="RptrDimensions_Refinements" runat="server"></asp:LinkButton>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
      

