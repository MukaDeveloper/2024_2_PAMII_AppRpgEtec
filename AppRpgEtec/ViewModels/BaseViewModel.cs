using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppRpgEtec.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static string conexaoAzureStorage = "DefaultEndpointsProtocol=https;AccountName=etecstorageazurestudents;AccountKey=WlXzdnr7VEmuixkU7G6P2EwMHzJusPpi7BpGq0TH2JFeZrzPnmPhi+ay4FMGxVWu1tiCtMgfqLy5+AStA+eUUQ==;EndpointSuffix=core.windows.net";
        public static string container = "arquivos";

        public void OnPropertyChanged([CallerMemberName] string name = "")
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
