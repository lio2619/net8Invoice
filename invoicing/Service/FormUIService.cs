using invoicing.Service.Interface;

namespace invoicing.Service
{
    public class FormUIService : IFormUIService
    {
        /// <summary>
        /// 在TextBox的下面畫直線
        /// </summary>
        /// <param name="txt"></param>
        public void AddTextBoxUnderline(TextBox txt)
        {
            Panel underline = new Panel();
            underline.Height = 1;
            underline.Dock = DockStyle.Bottom;
            underline.BackColor = Color.Black;
            txt.Controls.Add(underline);
        }
    }
}
