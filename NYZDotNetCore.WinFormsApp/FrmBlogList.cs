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
            List<BlogModel> lst = _dapperService.Query<BlogModel>(Queries.BlogQuery.BlogLists);
            dgvBlog.DataSource = lst;
        }
    }
}
