using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Githubapp
{
    public partial class AddBlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void addbtn_Click(object sender, EventArgs e)
        {
            Blog.addPost(txtTitle.Text, mytextarea.Value, txtAuthor.Text, exdate.Value.ToString());
            Response.Redirect("Blog.aspx");
        }
    }
}