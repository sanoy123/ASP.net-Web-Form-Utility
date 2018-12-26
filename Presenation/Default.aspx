<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presenation._Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET Web Form Utility</h1>
        <p class="lead">Sample code for the ASP.net web form. CRUD operation, Telerik gridview, GridView with Image from Database, Cascading Dropdown List, Parent/child Tree View, File Upload to a folder, File Upload to the SQL Server Database, and Sending Email feature</p>

    </div>

    <div class="row">
        <div class="col-md-8">
            <h2>Telerik Grid</h2>

            <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />
            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                AllowSorting="True" AllowFilteringByColumn="true" GridLines="None" OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
                OnItemCreated="RadGrid1_ItemCreated" OnDeleteCommand="RadGrid1_DeleteCommand"
                OnInsertCommand="RadGrid1_InsertCommand">
                <ItemStyle Wrap="false"></ItemStyle>
                <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" AllowMultiColumnSorting="true" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="FontIconButton" />
                        <telerik:GridNumericColumn DataField="Id" HeaderText="User ID" ReadOnly="true" HeaderStyle-Width="100px"
                            FilterControlWidth="50px">
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="FirstName" HeaderText="FirstName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastName" HeaderText="LastName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Remark" HeaderText="Remark">
                        </telerik:GridBoundColumn>

                        <telerik:GridButtonColumn ConfirmText="Delete this product?" ConfirmDialogType="RadWindow"
                            ConfirmTitle="Delete" ButtonType="FontIconButton" CommandName="Delete" />

                    </Columns>
                </MasterTableView>
                <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>

            </telerik:RadGrid>
            <br />

            <h2>GridView with Image from Database</h2>
             <asp:GridView ID="gvGallery" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" DataKeyNames="Id"  OnRowDataBound="OnRowDataBound">
                            <Columns>
                              

                                <asp:TemplateField HeaderText="Picture">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" Width="150px" />
                                    </ItemTemplate>
                                </asp:TemplateField>



                              



                            </Columns>
                        </asp:GridView>
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <div class="panel panel-default">
                <div class="panel-heading">Cascading Dropdown</div>
                <div class="panel-body">
                    <table>
                        <tr>
                            <td>Category</td>
                            <td>
                                <asp:DropDownList CssClass="form-control" ID="ddlCategory" runat="server" Width="203px" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Sub-category</td>
                            <td>
                                <asp:DropDownList CssClass="form-control" ID="ddlSubCategory" runat="server" Width="203px"></asp:DropDownList></td>
                        </tr>
                    </table>
                    </div></div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div class="panel panel-default">
                <div class="panel-heading">File Uploader to Folder</div>
                <div class="panel-body">

          
            <asp:Label runat="server" ID="lblMassage"></asp:Label>
            File Upload:<asp:FileUpload ID="FileUpload1" runat="server" Width="356px"></asp:FileUpload>
            <asp:Button ID="btnUpload" OnClick="btnUpload_Click" runat="server" Text="Upload" CssClass="button"></asp:Button>
                    </div></div>
            <br />
            <div class="panel panel-default">
                <div class="panel-heading">File Uploader to Database</div>
                <div class="panel-body">
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
            <asp:Label runat="server" ID="Label1"></asp:Label>
            File Upload:<asp:FileUpload ID="FileUpload2" runat="server" Width="356px"></asp:FileUpload>
            <asp:Button ID="Button2" OnClick="btnUploadServer_Click" runat="server" Text="Upload" CssClass="button"></asp:Button>
                    </div></div>

        </div>
        <div class="col-md-4">

            <div class="panel panel-default">
                <div class="panel-heading">New User</div>
                <div class="panel-body">
                    <table>
                        <tr>
                            <td>Firstname
                            </td>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="txtFirstName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Lastname
                            </td>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="txtLastName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="Button1_Click" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="well">
            <h2>Parent/Child</h2>

            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                    NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                    VerticalPadding="0px" />
            </asp:TreeView>
                </div>
            <br />
        
            <div class="panel panel-default">
                <div class="panel-heading">Send Email</div>
                <div class="panel-body">
                    <table>
                        <tr>
                            <td>To
                            </td>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="txtTo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Subject
                            </td>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="txtSubject" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Message
                            </td>
                            <td>
                                <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSendMail" CssClass="btn btn-primary" runat="server" Text="Send E-mail" OnClick="btnSendMail_Click" /></td>
                        </tr>
                    </table>
                </div>
            </div>




        </div>






        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" />


        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Populate" TypeName="Business.User"></asp:ObjectDataSource>

    </div>

</asp:Content>
