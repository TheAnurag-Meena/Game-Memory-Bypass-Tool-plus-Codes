using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Mem MemLib = new Mem();
        public async void SampleAoBScan()
        {
            // open the process and check if it was successful before the AoB scan
            if (!MemLib.OpenProcess("MyGamesProcessName")) // you can also specify the process ID. Check Wiki for more info.
            {
                MessageBox.Show("Process Is Not Found or Open!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // AoB scan and store it in AoBScanResults. We specify our start and end address regions to decrease scan time.
            IEnumerable<long> AoBScanResults = await MemLib.AoBScan(0x01000000, 0x04000000, "?? ?? ?? ?5 ?? ?? 5? 00 ?? 00 00 00 ?? 00 50 00", false, true);

            // get the first found address, store it in the variable SingleAoBScanResult
            long SingleAoBScanResult = AoBScanResults.FirstOrDefault();

            // pop up message box that shows our first result
            MessageBox.Show("Our First Found Address is " + SingleAoBScanResult);

            // Ex: iterate through each found address. This prints each address in the debug console in Visual Studio.
            foreach (long res in AoBScanResults)
            {
                Debug.WriteLine("I found the address {0} in the AoB scan.", res, null);
            }

            // Ex: read the value from our first found address, convert it to a string, and show a pop up message - https://github.com/erfg12/memory.dll/wiki/Read-Memory-Functions
            MessageBox.Show("Value for our address is " + MemLib.ReadFloat(SingleAoBScanResult.ToString("X")).ToString());

            // Ex: write to our first found address - https://github.com/erfg12/memory.dll/wiki/writeMemory
            MemLib.WriteMemory(SingleAoBScanResult.ToString("X"), "float", "100.0");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
