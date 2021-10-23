using Guna.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Management
{
    public partial class Frm_Container : Form
    {
        public Frm_Container()
        {
            InitializeComponent();
            this.Text = lbl_Title.Text + " - My project";
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            ActivateButton(btn_Dashboard);
        }

        private GunaAdvenceButton currentButton;
        private Form activeForm;

        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int IPrama);

        private void lbl_Title_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        

        const int _ = 10; 

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        //Override WndProc Method
        protected override void DefWndProc(ref Message m)
        {
            const int
                HTLEFT = 10,
                HTRIGHT = 11,
                HTTOP = 12,
                HTTOPLEFT = 13,
                HTTOPRIGHT = 14,
                HTBOTTOM = 15,
                HTBOTTOMLEFT = 16,
                HTBOTTOMRIGHT = 17;

            const int WM_NCCALCSIZE = 0x0083;
            if(m.Msg== WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.DefWndProc(ref m);
            if (m.Msg == 0x0084) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) m.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) m.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) m.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) m.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) m.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) m.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) m.Result = (IntPtr)HTBOTTOM;
            }
        }

        //Component Event
        private void btn_Logo_Nav_Click(object sender, EventArgs e)
        {
            ActivateCollapse();
        }
        private void ActivateCollapse()
        {
            if (pnl_Menu.Width > 200)
            {
                pnl_Menu.Width = 80;
            }
            else
            {
                pnl_Menu.Width = 220;
            }
        }

        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            MenuDrop.Show(this.btn_Dashboard, this.btn_Dashboard.Width, 0);
        }

        private void ActivateButton(object sender)
        {

            if (sender != null)
            {
                if (currentButton != (GunaAdvenceButton)sender)
                {
                    DisableButton();
                    currentButton = (GunaAdvenceButton)sender;
                    currentButton.LineBottom = 4;
                    currentButton.ImageSize = new System.Drawing.Size(32, 32);
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void DisableButton()
        {
            foreach(Control button in pnl_Button_Container.Controls)
            {
                if (button.GetType() == typeof(GunaAdvenceButton))
                {
                    currentButton = (GunaAdvenceButton)button;
                    currentButton.LineBottom = 0;
                    currentButton.ImageSize = new System.Drawing.Size(28, 28);
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            this.pnl_Container.Controls.Add(childForm);
            this.pnl_Container.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbl_Title.Text = childForm.Text;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Maximize_Click(object sender, EventArgs e)
        {
            FormWindowState formState = this.WindowState;
            if(formState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Padding = new Padding(4);
                btn_Maximize.Image = global::Final_Project_Management.Properties.Resources.normal;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                btn_Maximize.Image = global::Final_Project_Management.Properties.Resources.maximize;
            }
        }

        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pnl_TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Frm_Container_Load(object sender, EventArgs e)
        {
            MenuDrop.IsMainMenu = true;
        }
    }
}
