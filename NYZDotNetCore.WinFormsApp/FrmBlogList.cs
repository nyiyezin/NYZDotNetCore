using System.Windows.Forms;
using NYZDotNetCore.Shared;
using NYZDotNetCore.WinFormsApp.Models;

namespace NYZDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;

        public FrmBlogList()
        {
            InitializeComponent();
            _dapperService = new DapperService(
                ConnectionStrings.sqlConnectionStringBuilder.ConnectionString
            );
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(Queries.BlogQuery.BlogLists);
            dgvBlog.DataSource = lst;
        }

        private void dbvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            #region If Case

            var blogId = Convert.ToInt32(dgvBlog.Rows[e.RowIndex].Cells["colId"].Value);
            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();
                BlogList();
            }
            else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Ary you sure you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;
                DeleteBlog(blogId);
            }

            #endregion

            #region Switch Case

            int index = e.ColumnIndex;
            EnumFormControlType enumFormControlType = (EnumFormControlType)index;
            switch (enumFormControlType)
            {
                case EnumFormControlType.Edit:
                    FrmBlog frm = new FrmBlog();
                    frm.ShowDialog();
                    break;
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Ary you sure you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes) return;
                    DeleteBlog(blogId);
                    break;
                case EnumFormControlType.None:
                    break;
                default:
                    MessageBox.Show("Invalid Case.");
                    break;
            }

            #endregion
        }

        private void DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            int result = _dapperService.Execute(query, new { BlogId = id });

            string message = result > 0 ? "Deleted Successfully" : "Deleting failed";
            MessageBox.Show(message);
        }
    }
}
