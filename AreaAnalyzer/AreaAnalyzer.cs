using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AreaAnalyzer
{
    public partial class AreaAnalyzer : Form
    {
        public int file_offset = 0;
        EncounterSet[] encsets;
        public int num_sets;

        public AreaAnalyzer()
        {
            InitializeComponent();
            LoadFile();
        }

        private void LoadFile()
        {
            string filename = "F:\\RE\\Sui2\\Sui2 Files\\030_ARH\\VC13.bin";
            byte[] src = File.ReadAllBytes(filename);

            int mybase = Pointer2Index(src, 0);

            if (mybase < 0x15DC50)
                file_offset = 0x10DC50;
            else
                file_offset = 0x15DC50;

            mybase = Pointer2Index(src, 0);
            int encbase = Pointer2Index(src, mybase+28);

            WriteDWord(src, 0);
            WriteDWord(src, mybase);
            WriteDWord(src, encbase);

            ParseSets(src, encbase);

            for(int i=0;i<num_sets;i++)
                updateBox.AppendText(string.Format("\r\n===Encounter Set {0}===\r\n{1}",i,encsets[i].ToString()));
        }

        private void ParseSets(byte[] src, int encbase)
        {
            num_sets = Pointer2DWord(src, encbase);
            int myset;
            encsets = new EncounterSet[num_sets];
            for(int i = 0; i < num_sets; i++)
            {
                myset = Pointer2Index(src, encbase + i * 4+4);
                encsets[i] = new EncounterSet(src, myset, file_offset);
            }
        }

        private void WriteDWord(byte[] fileBytes, int v)
        {
            updateBox.AppendText(String.Format("{0:X}{1:X}{2:X}{3:X}\r\n",fileBytes[v+3], fileBytes[v+2], fileBytes[v+1], fileBytes[v]));
        }

        public int Pointer2Index(byte[] fileBytes, int v)
        {
            return fileBytes[v] + 256 * fileBytes[v + 1] + 256 * 256 * fileBytes[v + 2] - file_offset;
        }

        public int Pointer2DWord(byte[] src, int v)
        {
            return src[v] + 256*src[v + 1] + 256*256*src[v + 2] + 256*256*256*src[v + 3];
        }
    }
}
