using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Final_Project_Management.DropDown
{
    public class DropdownMenu : ContextMenuStrip
    {
        private bool isMainMenu;
        private int menuItemHeight = 25;
        private Color menuItemTextColor = Color.FromArgb(39, 35, 67);
        private Color primaryColor = Color.FromArgb(255, 216, 3);

        private Bitmap menuItemHeaderSize;
        public DropdownMenu(IContainer container)
            :base (container)
        {

        }

        public bool IsMainMenu { get => isMainMenu; set => isMainMenu = value; }
        public int MenuItemHeight { get => menuItemHeight; set => menuItemHeight = value; }
        public Color MenuItemTextColor { get => menuItemTextColor; set => menuItemTextColor = value; }
        public Color PrimaryColor { get => primaryColor; set => primaryColor = value; }

        private void LoadMenuItemAppearance()
        {
            if (isMainMenu)
            {
                menuItemHeaderSize = new Bitmap(24, 48);
                primaryColor = Color.FromArgb(186, 232, 232);
            }
            else
            {
                menuItemHeaderSize = new Bitmap(16, menuItemHeight + 4);
            }
            foreach(ToolStripMenuItem menuItemL1 in this.Items)
            {
                menuItemL1.ForeColor = menuItemTextColor;
                menuItemL1.ImageScaling = ToolStripItemImageScaling.None;
                if (menuItemL1.Image == null) menuItemL1.Image = menuItemHeaderSize;
                foreach (ToolStripMenuItem menuItemL2 in this.Items)
                {
                    menuItemL2.ForeColor = menuItemTextColor;
                    menuItemL2.ImageScaling = ToolStripItemImageScaling.None;
                    if (menuItemL2.Image == null) menuItemL2.Image = menuItemHeaderSize;
                    foreach (ToolStripMenuItem menuItemL3 in this.Items)
                    {
                        menuItemL3.ForeColor = menuItemTextColor;
                        menuItemL3.ImageScaling = ToolStripItemImageScaling.None;
                        if (menuItemL3.Image == null) menuItemL3.Image = menuItemHeaderSize;
                        foreach (ToolStripMenuItem menuItemL4 in this.Items)
                        {
                            menuItemL4.ForeColor = menuItemTextColor;
                            menuItemL4.ImageScaling = ToolStripItemImageScaling.None;
                            if (menuItemL4.Image == null) menuItemL4.Image = menuItemHeaderSize;

                        }
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this.DesignMode == false)
            {
                LoadMenuItemAppearance();
                this.Renderer = new MenuRenderer(IsMainMenu, primaryColor, MenuItemTextColor);
            }
        }
    }
}
