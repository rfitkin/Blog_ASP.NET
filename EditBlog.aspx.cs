using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Githubapp
{
    public partial class EditBlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAuthor.Text = Session["OriginalFN"].ToString();
            txtTitle.Text = Session["UpdateTitle"].ToString();
            mytextarea.Value = Session["UpdateCont"].ToString();
            DateTime d = Convert.ToDateTime(Session["UpdateExpirationDate"]);
            exdate.Value = d.ToString("yyyy-MM-dd");
        }

        protected void editbtn_Click(object sender, EventArgs e)
        {
            Blog.EditPost(txtTitle.Text, mytextarea.Value, txtAuthor.Text, Convert.ToInt32(Session["PostID"]), exdate.Value);
            Response.Redirect("Blog.aspx");
        }
    }
}