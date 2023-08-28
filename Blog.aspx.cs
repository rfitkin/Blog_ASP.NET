using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Githubapp
{
    public partial class Blog : System.Web.UI.Page
    {
        public static string conn = ("Data Source = Server Name; Initial Catalog = DataBase Name; User ID = UserName Here; Password=Password Here");
        public static SqlConnection con = new SqlConnection(conn);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid();
                dropfilter();
                ArchiveExpiredPost();
                deletebutton.Visible = false;
                Editbutton.Visible = false;
            }
        }

        public static DataSet getPost(int postident)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("BlogDisplayPost", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@postID", SqlDbType.Int));
            cmd.Parameters["@postID"].Value = postident;
            try
            {
                con.Open();
                adp.Fill(ds, "Post");
            }
            catch (Exception)
            { }
            finally
            {
                con.Close();
            }

            return ds;
        }
        public static void addPost(string title, string cont, string auth, string Exdate)
        {
            var timeUTC = DateTime.UtcNow;
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var timeStamp = TimeZoneInfo.ConvertTimeFromUtc(timeUTC, est);
            SqlCommand cmd = new SqlCommand("BlogAddPost", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@pTitle", SqlDbType.VarChar).Value = title;
            cmd.Parameters.AddWithValue("@pContent", SqlDbType.NVarChar).Value = cont;
            cmd.Parameters.AddWithValue("@pAuthor", SqlDbType.VarChar).Value = auth;
            cmd.Parameters.AddWithValue("@Exdate", SqlDbType.VarChar).Value = Exdate;
            cmd.Parameters.AddWithValue("@creationdate", SqlDbType.DateTime).Value = timeStamp;
            int added = 0;
            try
            {
                con.Open();
                added = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
        }

        public static void EditPost(string title, string cont, string auth, int postid, string Exdate)
        {
            var timeUTC = DateTime.UtcNow;
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var timeStamp = TimeZoneInfo.ConvertTimeFromUtc(timeUTC, est);
            SqlCommand cmd = new SqlCommand("BlogEditPost", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@pTitle", SqlDbType.VarChar).Value = title;
            cmd.Parameters.AddWithValue("@pContent", SqlDbType.NVarChar).Value = cont;
            cmd.Parameters.AddWithValue("@pAuthor", SqlDbType.VarChar).Value = auth;
            cmd.Parameters.AddWithValue("@pid", SqlDbType.Int).Value = postid;
            cmd.Parameters.AddWithValue("@Exdate", SqlDbType.Date).Value = Exdate;
            cmd.Parameters.AddWithValue("@editdate", SqlDbType.DateTime).Value = timeStamp;
            int added = 0;
            try
            {
                con.Open();
                added = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void dropfilter()
        {
            RefreshGrid();
            if (drpPosts.SelectedValue == "Last 7 Days")
            {
                DateTime dateWeek = DateTime.Today.AddDays(-7);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    string filter = row.Cells[1].Text;
                    DateTime filterdate = Convert.ToDateTime(filter);
                    if (filterdate < dateWeek)
                    { row.Visible = false; }

                }

            }
            if (drpPosts.SelectedValue == "Last 30 Days")
            {
                DateTime dateM = DateTime.Today.AddDays(-30);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    string filter = row.Cells[1].Text;
                    DateTime filterdate = Convert.ToDateTime(filter);
                    if (filterdate < dateM)
                    { row.Visible = false; }

                }

            }
            if (drpPosts.SelectedValue == "Last 365 Days")
            {
                DateTime dateY = DateTime.Today.AddDays(-365);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    string filter = row.Cells[1].Text;
                    DateTime filterdate = Convert.ToDateTime(filter);
                    if (filterdate < dateY)
                    { row.Visible = false; }

                }

            }
        }

        public static DataSet getPosts()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("BlogDisplayPosts", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                adp.Fill(ds, "Posts");
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            finally
            {
                con.Close();
            }

            return ds;
        }

        private void RefreshGrid()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Select PostId, PostTitle, DatePosted, PostContent, PostAuthor from BlogPosts ORDER BY DatePosted ASC", con);
            try
            {
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ds.Clear();
                ad.Fill(ds);
                con.Close();

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void PostDelete()
        {
            int i = 0;
            Int32.TryParse(GridView1.SelectedRow.Cells[4].Text, out i);
            DataTable dt = new DataTable();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM BlogPosts WHERE PostId=@pid", con);
            cmd1.Parameters.AddWithValue("@pid", i);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            con.Close();

            SqlCommand cmd2 = new SqlCommand("INSERT INTO BlogPostsArchives (PostId, PostTitle, DatePosted, DateEdited, PostContent, PostAuthor, ExpirationDate, DateDeleted, Deletedby) VALUES (@0, @1, @2, @3, @4, @5, @6, @dt, @du)", con);
            cmd2.Parameters.AddWithValue("@0", dt.Rows[0][0]);
            cmd2.Parameters.AddWithValue("@1", dt.Rows[0][1]);
            cmd2.Parameters.AddWithValue("@2", dt.Rows[0][2]);
            cmd2.Parameters.AddWithValue("@3", dt.Rows[0][3]);
            cmd2.Parameters.AddWithValue("@4", dt.Rows[0][4]);
            cmd2.Parameters.AddWithValue("@5", dt.Rows[0][5]);
            cmd2.Parameters.AddWithValue("@6", dt.Rows[0][6]);
            cmd2.Parameters.AddWithValue("@dt", DateTime.Now.ToString());
            cmd2.Parameters.AddWithValue("@du", Session["FullName"].ToString());


            SqlCommand cmd3 = new SqlCommand("DELETE FROM BlogPosts WHERE PostId=@pid", con);
            cmd3.Parameters.AddWithValue("@pid", i);
            con.Open();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            con.Close();
            Response.Redirect("home.aspx");

        }

        private void ArchiveExpiredPost()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM BlogPosts WHERE ExpirationDate<=GETDATE()", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt.Clear();
            da.Fill(dt);
            con.Close();

            int count = dt.Rows.Count;

            if (count > 0)
            {
                SqlCommand cmd2 = new SqlCommand("INSERT INTO BlogPostsArchives (PostId, PostTitle, DatePosted, DateEdited, PostContent, PostAuthor, ExpirationDate) VALUES (@0, @1, @2, @3, @4, @5, @6)", con);
                cmd2.Parameters.AddWithValue("@0", dt.Rows[0][0]);
                cmd2.Parameters.AddWithValue("@1", dt.Rows[0][1]);
                cmd2.Parameters.AddWithValue("@2", dt.Rows[0][2]);
                cmd2.Parameters.AddWithValue("@3", dt.Rows[0][3]);
                cmd2.Parameters.AddWithValue("@4", dt.Rows[0][4]);
                cmd2.Parameters.AddWithValue("@5", dt.Rows[0][5]);
                cmd2.Parameters.AddWithValue("@6", dt.Rows[0][6]);

                SqlCommand cmd3 = new SqlCommand("DELETE FROM BlogPosts WHERE ExpirationDate<=GETDATE()", con);
                con.Open();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                con.Close();
            }

            dt.Clear();


        }
        protected void drpPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropfilter();
            frmPosts.Controls.Clear();
            deletebutton.Visible = false;
            Editbutton.Visible = false;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);

            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    string cellText = row.Cells[i].Text;
                    row.Cells[i].ToolTip = cellText;
                }
            }
        }

        protected void AddPost_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBlog.aspx");
        }

        protected void Editbutton_Click(object sender, EventArgs e)
        {
            int i = 0;
            Int32.TryParse(GridView1.SelectedRow.Cells[4].Text, out i);
            DataTable dt = new DataTable();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM BlogPosts WHERE PostId=@pid", con);
            cmd1.Parameters.AddWithValue("@pid", i);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            dt.Clear();
            da.Fill(dt);
            con.Close();
            Session["PostID"] = i;
            Session["UpdateTitle"] = dt.Rows[0][1].ToString();
            Session["UpdateCont"] = dt.Rows[0][4].ToString();
            Session["OriginalFN"] = dt.Rows[0][5].ToString();
            Session["UpdateExpirationDate"] = dt.Rows[0][6].ToString();
            Response.Redirect("EditBlog.aspx");
        }

        protected void deletebutton_Click(object sender, EventArgs e)
        {
            PostDelete();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            deletebutton.Visible = false;
            Editbutton.Visible = false;
            int i = 0;
            Int32.TryParse(GridView1.SelectedRow.Cells[4].Text, out i);

            DataSet dat = getPost(i);
            frmPosts.DataSource = dat;
            frmPosts.DataBind();
            Editbutton.Visible = true;
            deletebutton.Visible = true;
            
            
        }

        
    }
}