using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace who
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static EventHandler<string> OnSpWho;

        public MainPage()
        {
            InitializeComponent();
            var update = new DispatcherTimer();
            update.Interval = new TimeSpan(0, 0, 0, 3, 0);
            update.Tick += (o, s) => DownloadDataAsync();
            OnSpWho += (o, j) => listBox.ItemsSource = JsonConvert.DeserializeObject(j);

            update.Start();
        }

        private static async void DownloadDataAsync()
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync("http://localhost:3000/spwho"))
            using (var content = response.Content)
            {
                var result = await content.ReadAsStringAsync();
                OnSpWho?.Invoke(null, result);
            }
        }
    }
}