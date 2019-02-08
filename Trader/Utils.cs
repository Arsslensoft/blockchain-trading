using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace Trader
{
    public class SavedAccount
    {
        public string Url { get; set; }
        public string ContractAddress { get; set; }
        public string ContractABI { get; set; }
        public string Address { get; set; }

        public int Time { get; set; }
    }
    public class SavedAccounts
    {
        public List<SavedAccount> Accounts { get; set; } = new List<SavedAccount>();
    }
   public class Utils
    {
        public static DataTable ListViewToDataTable(ListViewEx list)
        {
            var dt = new DataTable("Data");
            list.Invoke(new Action(() =>
            {
                foreach (var listColumn in list.Columns)
                    dt.Columns.Add((listColumn as ColumnHeader).Text);
                int c = 0;

                foreach (var listItem in list.Items)
                {
                    var item = listItem as ListViewItem;
                    var values = new object[dt.Columns.Count];
                    for (int i = 0; i < item.SubItems.Count; i++)
                        values[i] = item.SubItems[i].Text;


                    dt.Rows.Add(values);
                }
            }));
            return dt;
        }
    }
}
