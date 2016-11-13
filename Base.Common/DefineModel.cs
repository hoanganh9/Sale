using System.Collections.Generic;

namespace Base.Common
{
    public class DFSelectItem
    {
        public DFSelectItem()
        {
        }

        public DFSelectItem(string Text, string Value)
        {
            this.Text = Text;
            this.Value = Value;
        }

        public DFSelectItem(string Text, string Value, bool Selected = false)
        {
            this.Text = Text;
            this.Value = Value;
            this.Selected = Selected;
        }

        public DFSelectItem(string Text, string Value, string Value2 = "", bool Selected = false)
        {
            this.Text = Text;
            this.Value = Value;
            this.Selected = Selected;
        }

        public DFSelectItem(string Text, string Value, string Value2 = "", string Value3 = "", bool Selected = false)
        {
            this.Text = Text;
            this.Value = Value;
            this.Value2 = Value2;
            this.Value3 = Value3;
            this.Selected = Selected;
        }

        public DFSelectItem(string Text, string Value, object Data)
        {
            this.Text = Text;
            this.Value = Value;
            this.Data = Data;
        }

        public string Text { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string PosPhat { get; set; }

        public object Data { get; set; }

        /// <summary>
        /// Dùng cho Select Search, nếu là true sẽ select mặc định trường đó
        /// </summary>
        public bool Selected = false;

        public string Filter { get; set; }
    }
}