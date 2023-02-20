using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using SAP.Middleware.Connector;
using System.Data;

namespace SAPGUIConnection
{
    public partial class _Default : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LabelNoUserIdExists.Visible = false;
                PanelIfUserExists.Visible = false;
                PanelUserDetails.Visible = false;
                
            }

        }

        public void SAPCon()
        {
            try
            {
                RfcConfigParameters SAPparam = new RfcConfigParameters();
                SAPparam.Add(RfcConfigParameters.Name, SystemID.Text);
                SAPparam.Add(RfcConfigParameters.AppServerHost, ServerD.Text);
                SAPparam.Add(RfcConfigParameters.SystemNumber, Instance.Text);
                SAPparam.Add(RfcConfigParameters.SystemID, SystemID.Text);
                SAPparam.Add(RfcConfigParameters.Client, Client.Text);
                SAPparam.Add(RfcConfigParameters.User, UserID.Text);
                SAPparam.Add(RfcConfigParameters.Password, Password.Text);
                SAPparam.Add(RfcConfigParameters.Language, "EN");

                Dest.destination = RfcDestinationManager.GetDestination(SAPparam);
                Dest.destination.Ping();
                MessageBox.Show("Connected to "+ SystemID.Text+" System  !!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

            }


        }

        public bool DoesUserExists(string userID)
        {
            //Calling the BAPI (BAPI_USER_GET_DETAIL) from C#
            RfcDestination SAPDestSys = Dest.destination;
            PanelIfUserExists.Visible = false;
            LabelNoUserIdExists.Visible = false;

            IRfcFunction bapiUserGetDetails = SAPDestSys.Repository.CreateFunction("BAPI_USER_GET_DETAIL");

            bapiUserGetDetails.SetValue("USERNAME", userID);


            bapiUserGetDetails.Invoke(SAPDestSys);

            IRfcStructure SAPuserDetails = bapiUserGetDetails.GetStructure("ISLOCKED");
            if (SAPuserDetails.GetString("WRNG_LOGON") == "L" || SAPuserDetails.GetString("LOCAL_LOCK") == "L" || SAPuserDetails.GetString("GLOB_LOCK") == "L" || SAPuserDetails.GetString("NO_USER_PW") == "L")
            {
                Dest.userLock = "L";
            }

            Dest.Roles = bapiUserGetDetails.GetTable("ACTIVITYGROUPS");


            IRfcTable UserA = bapiUserGetDetails.GetTable("RETURN");
            if (UserA.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void UserDetails_Click(object sender, EventArgs e)
        {
            PanelUserDetails.Visible = true;
            SAPSystem.Visible = false;
            PanelCreateModifyUser.Visible = false;
            TextBox1.Text = "";
            Label13.Visible = false;
            PanelModifyUser.Visible = false;
            RoleChangePanel.Visible = false;
            TextBoxUserId.Text = "";
        }

        protected void UserAdmin_Click(object sender, EventArgs e)
        {
            PanelCreateModifyUser.Visible = true;
            PanelUserDetails.Visible = false;
            PanelIfUserExists.Visible = false;
            RolesList.Visible = false;
            TextBoxUserId.Text = "";
            SAPSystem.Visible = false;
        }

        protected void RetrieveBtn_Click(object sender, EventArgs e)
        {

            try
            {
                //Calling the BAPI (BAPI_USER_GET_DETAIL) from C#
                RfcDestination SAPDestSys = Dest.destination;
                PanelIfUserExists.Visible = false;
                LabelNoUserIdExists.Visible = false;

                IRfcFunction bapiUserGetDetails = SAPDestSys.Repository.CreateFunction("BAPI_USER_GET_DETAIL");

                bapiUserGetDetails.SetValue("USERNAME", TextBoxUserId.Text);

                bapiUserGetDetails.Invoke(SAPDestSys);

                IRfcTable UserA = bapiUserGetDetails.GetTable("RETURN");


                // string returnCode = bapiUserGetDetails.GetString("RETURN_CODE");
                if (UserA.Count > 0)
                {
                    LabelNoUserIdExists.Visible = true;
                    PanelIfUserExists.Visible = false;
                }
                else
                {
                    PanelIfUserExists.Visible = true;
                    LabelNoUserIdExists.Visible = false;
                    
                    //Getting the output of BAPI Call from SAP and storing in fields
                    IRfcStructure SAPuserDetails = bapiUserGetDetails.GetStructure("ADDRESS");
                    string firstname = SAPuserDetails.GetString("FIRSTNAME");
                    string lastname = SAPuserDetails.GetString("LASTNAME");
                    SAPuserDetails = bapiUserGetDetails.GetStructure("LOGONDATA");
                    string usrtyp = SAPuserDetails.GetString("USTYP");
                    Dest.Roles = bapiUserGetDetails.GetTable("ACTIVITYGROUPS");
                    int RoleCount = Dest.Roles.Count;

                    LabelUserIdValue.Text = TextBoxUserId.Text;
                    LabeUserTypeValue.Text = usrtyp;
                    LabelUserNameValue.Text = firstname + " " + lastname;
                    LabelRolesValue.Text = RoleCount + " Roles Assigned";
                    if (RoleCount == 0)
                    {
                        viewrole.Visible = false;
                    }
                    else
                    {
                        viewrole.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }

            


            RolesList.Visible = false;
        }

        protected void CreateUserBtn_Click(object sender, EventArgs e)
        {

            if (DoesUserExists(TextBox1.Text))
            {
                Label13.Visible = true;
                Label13.Text = "User Already Exists !!";
                PanelModifyUser.Visible = false;
            }
            else
            {
                tbFirstName.Text = "";
                tbLastName.Text = "";
                tbPassword.Text = "";
                fnamechk.Visible = false;
                lnamechk.Visible = false;
                pwdchk.Visible = false;
                PanelModifyUser.Visible = true;
                RoleChangePanel.Visible = false;
                Label13.Visible = false;
                LabelFirstName.Visible = true;
                tbFirstName.Visible = true;
                LabelLastName.Visible = true;
                tbLastName.Visible = true;
                LabelPassword.Visible = true;
                tbPassword.Visible = true;
                CreateUsr.Visible = true;
                ChangeUsr.Visible = false;
                PasswordRst.Visible = false;

            }
        }

        protected void ModifyUserBtn_Click(object sender, EventArgs e)
        {
            if (!DoesUserExists(TextBox1.Text))
            {
                Label13.Visible = true;
                Label13.Text = "User Doesn't Exists !!";
                PanelModifyUser.Visible = false;

            }
            else
            {
                tbFirstName.Text = "";
                tbLastName.Text = "";
                tbPassword.Text = "";
                Label13.Visible = false;
                PanelModifyUser.Visible = true;
                RoleChangePanel.Visible = false;
                LabelFirstName.Visible = true;
                tbFirstName.Visible = true;
                LabelLastName.Visible = true;
                tbLastName.Visible = true;
                LabelPassword.Visible = true;
                tbPassword.Visible = true;
                fnamechk.Visible = true;
                lnamechk.Visible = true;
                pwdchk.Visible = true;
                CreateUsr.Visible = false;
                ChangeUsr.Visible = true;
                PasswordRst.Visible = false;



            }
        }

        protected void PasswordBtn_Click(object sender, EventArgs e)
        {

            if (!DoesUserExists(TextBox1.Text))
            {
                Label13.Visible = true;
                Label13.Text = "User Doesn't Exists !!";
                PanelModifyUser.Visible = false;

            }
            else
            {
                tbFirstName.Text = "";
                tbLastName.Text = "";
                tbPassword.Text = "";
                fnamechk.Visible = false;
                lnamechk.Visible = false;
                pwdchk.Visible = false;
                Label13.Visible = false;
                PanelModifyUser.Visible = true;
                RoleChangePanel.Visible = false;
                LabelFirstName.Visible = false;
                tbFirstName.Visible = false;
                LabelLastName.Visible = false;
                tbLastName.Visible = false;
                LabelPassword.Visible = true;
                tbPassword.Visible = true;
                CreateUsr.Visible = false;
                ChangeUsr.Visible = false;
                PasswordRst.Visible = true;
            }
        }

        protected void LockBtn_Click(object sender, EventArgs e)
        {
            if (!DoesUserExists(TextBox1.Text))
            {
                Label13.Visible = true;
                Label13.Text = "User Doesn't Exists !!";
                PanelModifyUser.Visible = false;

            }
            else
            {
                PanelModifyUser.Visible = false;
                if (Dest.userLock != "L")
                {
                    DialogResult result = MessageBox.Show("Lock User ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                    if (result == DialogResult.Yes)
                    {
                        RfcDestination SAPDestSys = Dest.destination;
                        IRfcFunction bapiUserLock = SAPDestSys.Repository.CreateFunction("BAPI_USER_LOCK");
                        bapiUserLock.SetValue("USERNAME", TextBox1.Text);
                        bapiUserLock.Invoke(SAPDestSys);
                        MessageBox.Show("User ID has been Locked", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        Dest.userLock = "L";
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("User ID is already Locked, Do you want to Unlock User ID ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                    if (result == DialogResult.Yes)
                    {
                        RfcDestination SAPDestSys = Dest.destination;
                        IRfcFunction bapiUserLock = SAPDestSys.Repository.CreateFunction("BAPI_USER_UNLOCK");
                        bapiUserLock.SetValue("USERNAME", TextBox1.Text);
                        bapiUserLock.Invoke(SAPDestSys);
                        MessageBox.Show("User ID has been Unlocked", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        Dest.userLock = "U";
                    }
                }

            }
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (!DoesUserExists(TextBox1.Text))
            {
                Label13.Visible = true;
                Label13.Text = "User Doesn't Exists !!";
                PanelModifyUser.Visible = false;

            }
            else
            {
                PanelModifyUser.Visible = false;
                RoleChangePanel.Visible = false;
                Label13.Visible = false;
                LabelFirstName.Visible = false;
                tbFirstName.Visible = false;
                LabelLastName.Visible = false;
                tbLastName.Visible = false;
                LabelPassword.Visible = false;
                tbPassword.Visible = false;
                CreateUsr.Visible = false;
                ChangeUsr.Visible = false;
                PasswordRst.Visible = false;
                DialogResult result = MessageBox.Show("Delete User ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                if (result == DialogResult.Yes)
                {
                    RfcDestination SAPDestSys = Dest.destination;
                    IRfcFunction bapiUserLock = SAPDestSys.Repository.CreateFunction("BAPI_USER_DELETE");
                    bapiUserLock.SetValue("USERNAME", TextBox1.Text);
                    bapiUserLock.Invoke(SAPDestSys);
                    MessageBox.Show("User ID has been Deleted", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                }
            }
        }

        protected void CreateUsr_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbPassword.Text == "" || tbPassword.Text == " ")
                {
                    MessageBox.Show("Invalid Password !! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                }
                else
                {
                    RfcDestination SAPDestSys = Dest.destination;
                    IRfcFunction bapiUserCreate = SAPDestSys.Repository.CreateFunction("BAPI_USER_CREATE1");
                    IRfcStructure passTemplate = SAPDestSys.Repository.GetStructureMetadata("BAPIPWD").CreateStructure();
                    passTemplate.SetValue("BAPIPWD", tbPassword.Text);
                    IRfcStructure addrsTemplate = SAPDestSys.Repository.GetStructureMetadata("BAPIADDR3").CreateStructure();
                    addrsTemplate.SetValue("FIRSTNAME", tbFirstName.Text);
                    addrsTemplate.SetValue("LASTNAME", tbLastName.Text);

                    //Set Address and Logon Data
                    bapiUserCreate.SetValue("USERNAME", TextBox1.Text);
                    bapiUserCreate.SetValue("PASSWORD", passTemplate);
                    bapiUserCreate.SetValue("ADDRESS", addrsTemplate);

                    //Create the User
                    bapiUserCreate.Invoke(SAPDestSys);
                    PanelModifyUser.Visible = false;
                    tbFirstName.Text = "";
                    tbLastName.Text = "";
                    tbPassword.Text = "";
                    IRfcTable returnTable = bapiUserCreate.GetTable("RETURN");
                    string message = returnTable.GetString("MESSAGE");
                    MessageBox.Show(" " + message, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                }
             }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
        }

        protected void ChangeUsr_Click(object sender, EventArgs e)
        {
            try
            {
                RfcDestination SAPDestSys = Dest.destination;
                IRfcFunction bapiUserChange = SAPDestSys.Repository.CreateFunction("BAPI_USER_CHANGE");

                IRfcStructure passTemplate = SAPDestSys.Repository.GetStructureMetadata("BAPIPWD").CreateStructure();
                passTemplate.SetValue("BAPIPWD", tbPassword.Text);

                IRfcStructure userName = SAPDestSys.Repository.GetStructureMetadata("BAPIADDR3").CreateStructure();
                userName.SetValue("LASTNAME", tbLastName.Text);
                userName.SetValue("FIRSTNAME", tbFirstName.Text);
    
                IRfcStructure userNameCheck = SAPDestSys.Repository.GetStructureMetadata("BAPIADDR3X").CreateStructure();
                if (fnamechk.Checked)
                { userNameCheck.SetValue("FIRSTNAME", "X"); }
                if (lnamechk.Checked)
                { userNameCheck.SetValue("LASTNAME", "X");   }

                IRfcStructure passCheck = SAPDestSys.Repository.GetStructureMetadata("BAPIPWDX").CreateStructure();
                if (pwdchk.Checked)
                { passCheck.SetValue("BAPIPWD", "X"); }

                bapiUserChange.SetValue("USERNAME", TextBox1.Text);
                bapiUserChange.SetValue("PASSWORD", passTemplate);
                bapiUserChange.SetValue("PASSWORDX", passCheck);
                bapiUserChange.SetValue("ADDRESS", userName);
                bapiUserChange.SetValue("ADDRESSX", userNameCheck);
            

                //Change User
                bapiUserChange.Invoke(SAPDestSys);
                tbFirstName.Text = "";
                tbLastName.Text = "";
                tbPassword.Text = "";
                PanelModifyUser.Visible = false;
                IRfcTable returnTable = bapiUserChange.GetTable("RETURN");
                string message = returnTable.GetString("MESSAGE");
                MessageBox.Show(" " + message, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(" " + ex);
            }
        }


        protected void PasswordRst_Click(object sender, EventArgs e)
        {
            try
            {
                RfcDestination SAPDestSys = Dest.destination;
                IRfcFunction bapiUserChange = SAPDestSys.Repository.CreateFunction("BAPI_USER_CHANGE");
                IRfcStructure passTemplate = SAPDestSys.Repository.GetStructureMetadata("BAPIPWD").CreateStructure();
                passTemplate.SetValue("BAPIPWD", tbPassword.Text);
                IRfcStructure passCheck = SAPDestSys.Repository.GetStructureMetadata("BAPIPWDX").CreateStructure();
                passCheck.SetValue("BAPIPWD", "X");

                bapiUserChange.SetValue("USERNAME", TextBox1.Text);
                bapiUserChange.SetValue("PASSWORD", passTemplate);
                bapiUserChange.SetValue("PASSWORDX", passCheck);

                //ResetPassword the User
                bapiUserChange.Invoke(SAPDestSys);
                PanelModifyUser.Visible = false;
                MessageBox.Show("Password has been changed Successfuly !!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
                
            }
        }


        protected void Viewrole_Click(object sender, EventArgs e)
        {
            if (Dest.Roles.Count > 0)
            {
                RolesList.Visible = true;
                RolesList.Items.Clear();
                foreach (IRfcStructure role in Dest.Roles)
                {
                    RolesList.Items.Add(role.GetString("AGR_NAME"));
                }
            }
        }


        protected void AddDelRoles_Click(object sender, EventArgs e)
        {

            if (!DoesUserExists(TextBox1.Text))
            {
                Label13.Visible = true;
                Label13.Text = "User Doesn't Exists !!";
                PanelModifyUser.Visible = false;
            }
            else
            {
                RoleChangePanel.Visible = true;
                PanelModifyUser.Visible = false;
                Label13.Visible = false;
                LabelFirstName.Visible = false;
                tbFirstName.Visible = false;
                LabelLastName.Visible = false;
                tbLastName.Visible = false;
                LabelPassword.Visible = false;
                tbPassword.Visible = false;
                CreateUsr.Visible = false;
                ChangeUsr.Visible = false;
                PasswordRst.Visible = false;
                RadioButtonList1.Visible = true;
                RadioButtonList1.ClearSelection();
            }



        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "Remove")
            {
                bool temp = DoesUserExists(TextBox1.Text);
                if (Dest.Roles.Count > 0)
                {
                    RoleListt.Visible = true;
                    NoRoleAssigned.Visible = false;
                    NewRoleName.Visible = false;
                    NewRoleNameText.Visible = false;
                    Finish.Text = "Remove Role";
                    Finish.Visible = true;
                    List<string> roleList = new List<string>();

                    foreach (IRfcStructure role in Dest.Roles)
                    {
                        roleList.Add(role.GetString("AGR_NAME"));
                    }
                    RoleListt.DataSource = roleList;
                    RoleListt.DataBind();
                }
                else
                {
                    NoRoleAssigned.Visible = true;
                }
            }
            else
            {
                NewRoleNameText.Text = "";
                RoleListt.Visible = false;
                NoRoleAssigned.Visible = false;
                NewRoleName.Visible = true;
                NewRoleNameText.Visible = true;
                Finish.Text = "Add Role";
                Finish.Visible = true;
            }
        }

        protected void Finish_Click(object sender, EventArgs e)
        {
            try
            {

                RfcDestination SAPDestSys = Dest.destination;

                if (RadioButtonList1.SelectedValue == "Remove")
                {
                    IRfcFunction bapiUserRoleDel = SAPDestSys.Repository.CreateFunction("BAPI_USER_ACTGROUPS_ASSIGN");
                    bapiUserRoleDel.SetValue("USERNAME", TextBox1.Text);
                    IRfcTable roletoAdd = bapiUserRoleDel.GetTable("ACTIVITYGROUPS");
                    List<string> rolecomp = new List<string>();
                    IRfcTable Roles = Dest.Roles;
                    List<string> selectedRoles = new List<string>();
                    foreach (ListItem item in RoleListt.Items)
                    {
                        if (item.Selected)
                        {
                            selectedRoles.Add(item.Value);
                        }
                    }

                    if (selectedRoles.Count == 0)
                    {
                        MessageBox.Show("No Roles Selected !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                    }
                    else
                    {
                        for(int i=0; i<Roles.Count; i++)
                        {
                            Roles.CurrentIndex = i;
                            IRfcStructure row = Roles.CurrentRow;
                            string val = row.GetString("AGR_NAME");
                            if (selectedRoles.Contains(val))
                            {
                                Roles.Delete(i);
                                i--;
                            }
                        }
                        //Calling the Role Addition Bapi here and Passing "Roles" Table..
                        foreach (IRfcStructure role in Roles)
                        {
                            roletoAdd.Insert();
                            roletoAdd.CurrentRow.SetValue("AGR_NAME", role.GetString("AGR_NAME"));
                            roletoAdd.CurrentRow.SetValue("FROM_DAT", role.GetString("FROM_DAT"));
                            roletoAdd.CurrentRow.SetValue("TO_DAT", role.GetString("TO_DAT"));
                        }

                        bapiUserRoleDel.Invoke(SAPDestSys);
                        RadioButtonList1.ClearSelection();
                        RoleListt.Visible = false;
                        Finish.Visible = false;
                        IRfcTable returnTable = bapiUserRoleDel.GetTable("RETURN");
                        string message = returnTable.GetString("MESSAGE");
                        MessageBox.Show(" " + message, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                    }
                }
                else
                {
                    IRfcFunction bapiUserRoleAdd = SAPDestSys.Repository.CreateFunction("BAPI_USER_ACTGROUPS_ASSIGN");
                    bapiUserRoleAdd.SetValue("USERNAME", TextBox1.Text);
                    IRfcTable roletoAdd = bapiUserRoleAdd.GetTable("ACTIVITYGROUPS");
                    foreach (IRfcStructure role in Dest.Roles)
                    {
                        roletoAdd.Insert();
                        roletoAdd.CurrentRow.SetValue("AGR_NAME", role.GetString("AGR_NAME"));
                        roletoAdd.CurrentRow.SetValue("FROM_DAT", role.GetString("FROM_DAT"));
                        roletoAdd.CurrentRow.SetValue("TO_DAT", role.GetString("TO_DAT"));
                    }

                    roletoAdd.Insert();
                    roletoAdd.CurrentRow.SetValue("AGR_NAME", NewRoleNameText.Text);
                    roletoAdd.CurrentRow.SetValue("FROM_DAT", DateTime.Now.ToString("yyyyMMdd"));
                    roletoAdd.CurrentRow.SetValue("TO_DAT", "99991231");

                    bapiUserRoleAdd.Invoke(SAPDestSys);
                    RadioButtonList1.ClearSelection();
                    NewRoleName.Visible = false;
                    NewRoleNameText.Visible = false;
                    Finish.Visible = false;
                    IRfcTable returnTable = bapiUserRoleAdd.GetTable("RETURN");
                    string message = returnTable.GetString("MESSAGE");
                    MessageBox.Show(" " + message);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(" "+ ex);
            }
        }

        protected void SAPServerDetails_Click(object sender, EventArgs e)
        {
            SAPSystem.Visible = true;
            PanelCreateModifyUser.Visible = false;
            PanelIfUserExists.Visible = false;
            PanelUserDetails.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
            if (ServerD.Text == "" || ServerD.Text == " "|| Instance.Text == "" || Instance.Text == " " || SystemID.Text == "" || SystemID.Text == " " || Client.Text == "" || Client.Text == " " || UserID.Text == "" || UserID.Text == " " || Password.Text == "" || Password.Text == " ")
            {
                MessageBox.Show("Invalid System Details !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            }
            else
            {
            SAPCon();
                UserAdmin.Enabled = true;
                UserDetailsButton.Enabled = true;
            }

            
        }
    }
    public static class Dest
    {
       public static RfcDestination destination;
       public static IRfcTable Roles;
       public static string userLock;
       

    }
}