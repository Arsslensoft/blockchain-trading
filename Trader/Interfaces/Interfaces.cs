using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trader.Core;
using Trader.Core.Models;
using Trader.Core.Reporting;

namespace Trader.Interfaces
{
    public interface IControlRegistry
    {
        List<Control> RegistredControls { get; set; }
        IEnumerable<T> GetControls<T>();
        void InitializeRegistry();

      
    }
    public interface IAccountControl
    {
        IControlRegistry Registry { get; }
        Account Account {get;}
        void OnLogout();
        void OnLogin();
    }
    public interface IAssetControl
    {
        void Changed(Asset a);
    }
    public interface IOwnerControl
    {
        void Changed(string address);
    }
    public interface ITransactionControl
    {
        void Changed(Transaction t);
    }
    public interface IExportable
    {
        void Export(Exporter e);
    }

}
