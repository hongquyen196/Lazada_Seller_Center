using DevComponents.DotNetBar.SuperGrid;
using System.Windows.Forms;

namespace SellerCenterLazada.Controls
{
    public class MyDatePicker : GridDateTimePickerEditControl
    {
        public MyDatePicker()
        {
            Format = DateTimePickerFormat.Custom;
            CustomFormat = "MM/dd/yyyy HH:mm:ss";
            if (Value == null)
                Text = "";
        }
    }
}
