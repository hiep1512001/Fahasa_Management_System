using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Final_Project_Management.DropDown
{
    public class MenuColorTable : ProfessionalColorTable
    {
        private Color backColor,
            leftColumnColor,
            borderColor,
            menuItemBorderColor,
            menuItemSelectedColor;

        public MenuColorTable(bool isMainMenu, Color primaryColor)
        {
            if (isMainMenu)
            {
                backColor = Color.FromArgb(255, 255, 255);
                leftColumnColor = Color.FromArgb(255, 216, 3);
                borderColor = Color.FromArgb(175, 175, 165);
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;  
            }
            else
            {
                backColor = Color.White;
                leftColumnColor = Color.LightGray;
                borderColor = Color.LightGray;
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
        }

        //Overrided
        public override Color ToolStripDropDownBackground => backColor;

        public override Color MenuBorder => borderColor;

        public override Color MenuItemBorder => menuItemBorderColor;

        public override Color MenuItemSelected => menuItemSelectedColor;

        public override Color ImageMarginGradientBegin => leftColumnColor;
        public override Color ImageMarginGradientMiddle => leftColumnColor;
        public override Color ImageMarginGradientEnd => leftColumnColor;
    }
}
