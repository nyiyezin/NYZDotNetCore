namespace NYZDotNetCore.WinFormsApp
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        } 
        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlog frm = new FrmBlog();
            frm.ShowDialog();
        }
        private void blogListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlogList frmBlogList = new FrmBlogList();
            frmBlogList.ShowDialog();
        }
    }
}
