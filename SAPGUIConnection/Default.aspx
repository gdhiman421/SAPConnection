<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SAPGUIConnection._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>SAP Connection</h3>
        <h5> <b>1. Create/Modify User:</b> To Create, Change, Reset Password, Add/Remove Role, Lock/Unlock and Delete an SAP User.</h5>
        <h5> <b>2. Retrieve User Details:</b> To extract the Details of user from SAP System.</h5>
        <h5> <b>3. SAP Server Details:</b> To enter the SAP System Parameters.</h5>
    </div>

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Button ID="UserAdmin" runat="server" Text="Create/Modify User" OnClick="UserAdmin_Click" />

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Button ID="UserDetailsButton" runat="server" Text="Retrieve User Details" OnClick="UserDetails_Click" />

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="SAPServerDetails" runat="server" Text="SAP Server Details" OnClick="SAPServerDetails_Click" />

    <asp:Panel ID="PanelUserDetails" runat="server" Height="415px" style="margin-top: 12px">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="User ID"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="TextBoxUserId" runat="server" ></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="RetrieveBtn_Click" Text="Retrieve" />
        <br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LabelNoUserIdExists" runat="server" Text="User ID Doesn't Exists"></asp:Label><br />
       
        
       
        <div>
            <asp:Panel ID="PanelIfUserExists" runat="server">
                &nbsp;&nbsp;&nbsp;<asp:Label ID="LabelUserId" runat="server" Text="User ID"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelUserIdValue" runat="server" Text="124214"></asp:Label>
                <br />
                &nbsp;&nbsp;
                <asp:Label ID="LabeUserType" runat="server" Text="User Type"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabeUserTypeValue" runat="server" Text="Label"></asp:Label>
               <br />  &nbsp;&nbsp;
                <asp:Label ID="LabelUserName" runat="server" Text="User Name"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelUserNameValue" runat="server" Text="Label"></asp:Label>
                <br />&nbsp;&nbsp;
                <asp:Label ID="LabelRoles" runat="server" Text="Roles"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelRolesValue" runat="server" Text="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="viewrole" runat="server" Visible="false" OnClick="Viewrole_Click">View Roles</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <br />
        <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ListBox ID="RolesList" runat="server" Height="250px" Width="250px" Visible="false"></asp:ListBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                </asp:Panel>
       </div>
    </asp:Panel>


    <asp:Panel ID="PanelCreateModifyUser" runat="server" Visible="false">
        <br />

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label12" runat="server" Text="User ID"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label13" runat="server" Text="User Already Exists !!" Visible="false"></asp:Label>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CreateUserBtn" runat="server" Text="Create" OnClick="CreateUserBtn_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="ModifyUserBtn" runat="server" OnClick="ModifyUserBtn_Click" Text="Change" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="AddDelRoles" runat="server"  Text="Add/Delete Roles" OnClick="AddDelRoles_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="PasswordBtn" runat="server" Text="Password" OnClick="PasswordBtn_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="LockBtn" runat="server" Text="Lock/Unlock" OnClick="LockBtn_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="DeleteBtn" runat="server" Text="Delete" OnClick="DeleteBtn_Click" />


        <br />


        <asp:Panel ID="PanelModifyUser" runat="server" Visible="false">
            <br />
            <asp:Label ID="LabelFirstName" runat="server" Text="First Name"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>

            &nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="fnamechk" runat="server" Visible="false" />
            <br />
            <br />
            <asp:Label ID="LabelLastName" runat="server" Text="Last Name"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>

            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="lnamechk" runat="server" Visible="false"/>
            <br />
            <br />
            <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="pwdchk" runat="server" Visible="false" />

            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="CreateUsr" runat="server" Text="Finish" OnClick="CreateUsr_Click" Visible="false"  />
            <asp:Button ID="ChangeUsr" runat="server" Text="Finish" OnClick="ChangeUsr_Click" Visible="false" />
            <asp:Button ID="PasswordRst" runat="server" Text="Reset" OnClick="PasswordRst_Click" Visible="false" />
        </asp:Panel>

        <asp:Panel ID="RoleChangePanel" runat="server" Height="349px" Visible="false">
             <br />
             <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" margin-left=30px RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" >
                <asp:ListItem Text="&nbsp;&nbsp;&nbsp;&nbsp;Add Role(s)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Value="Add"></asp:ListItem> 
                <asp:ListItem Text="Remove Role(s)" Value="Remove" ></asp:ListItem>
            </asp:RadioButtonList>
            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="NoRoleAssigned" runat="server" Text="No Role(s) Assigned to the User" Visible="false"></asp:Label>
             <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="NewRoleName" runat="server" Text="Role: " Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="NewRoleNameText" runat="server" Width="253px" Visible="false"></asp:TextBox>
            <br />&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:ListBox ID="RoleListt" runat="server" SelectionMode="Multiple" Width="263px" Visible="false" Height="152px">
             </asp:ListBox>
             <br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Finish" runat="server"  Text="Button" Visible="false" OnClick="Finish_Click"/>
            
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="SAPSystem" runat="server" Height="349px" Visible="false">
             <br />
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID= "ServerL" runat="server" Text="Server"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID= "ServerD" runat="server"></asp:TextBox>
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID= "InstanceL" runat="server" Text="Instance No"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID= "Instance" runat="server"></asp:TextBox>
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID= "SystemIDL" runat="server" Text="System ID"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID= "SystemID" runat="server"></asp:TextBox>
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID= "ClientL" runat="server" Text="Client"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
             <asp:TextBox ID= "Client" runat="server"></asp:TextBox>
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID= "UserIDL" runat="server" Text="User ID"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID= "UserID" runat="server"></asp:TextBox>
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID= "PasswordL" runat="server" Text="Password" ></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID= "Password" runat="server" TextMode="Password" ></asp:TextBox>
             <br />
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Connect" />
             <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            
        </asp:Panel>
</asp:Content>
