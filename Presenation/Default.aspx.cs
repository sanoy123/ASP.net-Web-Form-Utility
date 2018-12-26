using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DataAccess;
using Business;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using System.Resources;


using System.Configuration;
using System.Data.SqlClient;

namespace Presenation
{
    public partial class _Default : Page
    {
        Business.User result;
        Business.Category cateogry;
        private AspCrudDataContext _context;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PoulateCatgory();
                Business.Category catgories = new Business.Category();
                List<DataAccess.Category> list = catgories.Populate();
                DataTable dt = ConvertToDataTable(list);
                this.PopulateTreeView(dt, 0, null);
                PopulateGallery();


            }
        }
        public void PoulateCatgory()
        {
            Business.Category catgories = new Business.Category();
            List<DataAccess.Category> list = catgories.Populate();
            ddlCategory.DataSource = list;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataBind();

        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
           

            var editableItem = ((GridEditableItem)e.Item);
            var id = (int)editableItem.GetDataKeyValue("Id");

            _context = new AspCrudDataContext();
            var _data = _context.Users.Where(p => p.Id == id).First();

            if (_data != null)
            {
                //update entity's state
                editableItem.UpdateValues(_data);

                try
                {
                    //save chanages to Db
                    _context.SubmitChanges();
                }
                catch (System.Exception)
                {
                    ShowErrorMessage();
                }
            }




        }
        private void ShowErrorMessage()
        {

        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {

        }
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
           
        }
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var id = (int)editableItem.GetDataKeyValue("Id");

            _context = new AspCrudDataContext();
            var _data = _context.Users.Where(p => p.Id == id).First();

            if (_data !=null)
            {
                _context.Users.DeleteOnSubmit(_data);
                try
                {
                    _context.SubmitChanges();
                }
                catch (System.Exception)
                {
                    ShowErrorMessage();
                }
            }

        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            result = new Business.User();

            RadGrid1.DataSource = result.Populate();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSubCategory(Convert.ToInt16(ddlCategory.SelectedItem.Value));
        }
        public void PopulateSubCategory(int category)
        {
            Business.Category catgories = new Business.Category();
            List<DataAccess.SubCategory> list = catgories.PouplateSubCateogory(Convert.ToInt16(ddlCategory.SelectedItem.Value));
            ddlSubCategory.DataSource = list;
            ddlSubCategory.DataTextField = "Name";
            ddlSubCategory.DataValueField = "Id";
            ddlSubCategory.DataBind();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {

            Boolean fileOK = false;
            String path = Server.MapPath("files/");
            if (FileUpload1.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions =
                    { ".gif", ".bmp", ".png", ".jpeg", ".jpg", ".doc", ".wma", ".txt", ".pdf", ".xlsx", ".ppt", ".xls" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    FileUpload1.PostedFile.SaveAs(path
                        + FileUpload1.FileName);
                    lblMassage.Text = "File uploaded!";

                }
                catch (Exception ex)
                {
                    lblMassage.Text = "File could not be uploaded.";
                }
            }
            else
            {
                lblMassage.Text = "Cannot accept files of this type.";
            }
        }
        public void SendMail(string to, string subject, string bodymessage)
        {
                var fromAddress = "sender@gmail.com";
                
                var mail = new MailMessage();
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(new MailAddress(to));
                mail.Subject = subject;
                mail.IsBodyHtml = true;

                mail.Body += "<html>";
                mail.Body += "<body style='font-family:arial,sans-serif; size:10px;'>";
                mail.Body += bodymessage + "</body>";
                mail.Body += "</html>";
                mail.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient("127.0.0.1");
                SmtpServer.Port = 25;
                SmtpServer.Send(mail);

        

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            SendMail(txtTo.Text, txtSubject.Text, txtMessage.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Business.User user = new Business.User();
            user.Insert(txtFirstName.Text, txtLastName.Text, txtRemark.Text);
        }

        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["Name"].ToString(),
                    Value = row["Id"].ToString()
                };
                if (parentId == 0)
                {
                    TreeView1.Nodes.Add(child);

                    Business.Category catgories = new Business.Category();
                    List<DataAccess.SubCategory> list = catgories.PouplateSubCateogory(Convert.ToInt16(child.Value));
                    


                    DataTable dtChild = ConvertToDataTable(list);
                    PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                }
            }
        }

        private void PopulateGallery()
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;



            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(constr))
            {

                using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM [Gallery] ", conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvGallery.DataSource = dt;
                    gvGallery.DataBind();
                }
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["Picture"]);
                (e.Row.FindControl("Image1") as Image).ImageUrl = imageUrl;
            }
        }

        protected void btnUploadServer_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;

            if (FileUpload2.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
                String[] allowedExtensions =
                    { ".gif", ".bmp", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK && FileUpload2.PostedFile.ContentLength < 10485760)
            {
                try
                {
                    byte[] bytes;
                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(FileUpload2.PostedFile.InputStream))
                    {
                        bytes = br.ReadBytes(FileUpload2.PostedFile.ContentLength);
                        //user.Save(txtFirstName.Text, txtLastname.Text, bytes, txtUsername.Text, txtPassword.Text, "anonymous", "not_approved", "");
                    }
                    string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    Guid Id = Guid.NewGuid();
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        string sql = "INSERT INTO [Gallery] VALUES(@Id, @Picture)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", Id);
                            cmd.Parameters.AddWithValue("@Picture", bytes);
                    
                          
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    

                    Response.Redirect(Request.Url.AbsoluteUri);




                }
                catch (Exception ex)
                {
                    lblInfo.Text = "File could not be uploaded.";
                }
            }
            else
            {
                lblInfo.Text = "Cannot accept files of this type.";
            }




        }
    }
}