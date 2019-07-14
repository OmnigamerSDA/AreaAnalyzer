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
        public string directory = "";
        public int num_sets;

        public AreaAnalyzer()
        {
            InitializeComponent();
            directory = "F:\\RE\\Sui2\\Sui2 Files\\";//050_ARE\\";
            char region = 'A';
            string filename = "";
            string report = "";
            string filecode = "";
            //string filename = "VE04";
            for (int j = 0; j < 9; j++)
            {
                filecode = string.Format("0{0}0_AR{1}\\", j + 1, region);
                directory = ("F:\\RE\\Sui2\\Sui2 Files\\" + filecode);
                report = "";
                for (int i = 1; i < 10; i++)
                {
                    filename = "V" + region + "0" + i;
                    Console.WriteLine(filename);
                    if (File.Exists(directory + filename + ".bin"))
                        report += LoadFile(filename);
                }

                for (int i = 10; i < 40; i++)
                {
                    filename = "V" + region + i;
                    //Console.WriteLine(filename);
                    if (File.Exists(directory + filename + ".bin"))
                        report += LoadFile(filename);
                }

                filename = "W" + region;
                //Console.WriteLine(filename);
                if (File.Exists(directory + filename + ".bin"))
                    report += LoadFile(filename);

                using (System.IO.StreamWriter file =
               new System.IO.StreamWriter(@"F:\\RE\\Sui2\\Reports\\Area_" + region + ".txt"))
                {
                    file.WriteLine(report);
                }

                updateBox.AppendText(report);
                region++;
            }

            for (int j = 0; j < 2; j++)
            {
                filecode = string.Format("1{0}0_AR{1}\\", j, region);
                directory = ("F:\\RE\\Sui2\\Sui2 Files\\" + filecode);
                report = "";
                for (int i = 1; i < 10; i++)
                {
                    filename = "V" + region + "0" + i;
                    Console.WriteLine(filename);
                    if (File.Exists(directory + filename + ".bin"))
                        report += LoadFile(filename);
                }

                for (int i = 10; i < 40; i++)
                {
                    filename = "V" + region + i;
                    //Console.WriteLine(filename);
                    if (File.Exists(directory + filename + ".bin"))
                        report += LoadFile(filename);
                }

                filename = "W" + region;
                //Console.WriteLine(filename);
                if (File.Exists(directory + filename + ".bin"))
                    report += LoadFile(filename);

                using (System.IO.StreamWriter file =
               new System.IO.StreamWriter(@"F:\\RE\\Sui2\\Reports\\Area_" + region + ".txt"))
                {
                    file.WriteLine(report);
                }

                updateBox.AppendText(report);
                region++;
            }
        }

        private string LoadFile(string filename)
        {
            EncounterSet[] encsets;
            //string filename = "F:\\RE\\Sui2\\Sui2 Files\\030_ARC\\VC13.bin";
            byte[] src = File.ReadAllBytes(directory + filename + ".bin");

            file_offset = 0;

            int mybase = Pointer2Index(src, 0);
            int encbase;
            //Console.WriteLine("{0:X}",mybase);

            if (mybase < 0x15DC50)
                file_offset = 0x10DC50;
            else
                file_offset = 0x15DC50;

            //Console.WriteLine("{0:X}", file_offset);

            mybase = Pointer2Index(src, 0);
            // Console.WriteLine("{0:X}", mybase);
            if (mybase < src.Length)
                encbase = Pointer2Index(src, mybase + 28);
            else
                return "";//string.Format("Error parsing region {0}\n", filename);
            //  Console.WriteLine("{0:X}", encbase);

            if (encbase > 0 && encbase <src.Length)
            {
                //WriteDWord(src, 0);
                //WriteDWord(src, mybase);
                //WriteDWord(src, encbase);

                encsets = ParseSets(src, encbase);

                if (encsets != null)
                    return GenerateReport(src, encsets, filename);
                else
                    return "";// string.Format("Error parsing region {0}\n", filename);
            }
            else
            {
                return "";// string.Format("No encounters for region {0}\n",filename);
            }
        }

        private string GenerateReport(byte[] src,EncounterSet[] encsets, string filename)
        {
            string mystring = "";
           // using (System.IO.StreamWriter file =
           //new System.IO.StreamWriter(@"F:\\RE\\Sui2\\Reports\\" + filename + ".txt"))
           // {
                //file.WriteLine("=+=Encounter Sets for {0}=+=", filename);
            mystring+=string.Format("\r\n+++Encounter Sets for {0}+++\r\n", filename);
            for (int i = 0; i < encsets.Length; i++)
            {
                if (i < 1)
                {
                    mystring += string.Format("\r\n==Encounter Set {0} - [{1}, {2}]===\r\n\r\n{3}", i+1,encsets[i].maxpartylevel,encsets[i].maxlevel, encsets[i].ToString());
                }
                else
                {
                    if (Pointer2DWord(src, encsets[0].base_addr + 4) == Pointer2DWord(src, encsets[i].base_addr + 4))
                        mystring += "";// string.Format("\r\n===Encounter Set {0} is duplicate===\r\n", i+1);
                    else
                        mystring += string.Format("\r\n==Encounter Set {0} - [{1}, {2}]===\r\n\r\n{3}", i + 1, encsets[i].maxpartylevel, encsets[i].maxlevel, encsets[i].ToString());
                }
            }

            return mystring;
            //}
        }

        private EncounterSet[] ParseSets(byte[] src, int encbase)
        {
            EncounterSet[] encsets;
            num_sets = Pointer2DWord(src, encbase);
            if (num_sets < 10 && num_sets > 0)
            {
                int myset;
                encsets = new EncounterSet[num_sets];
                for (int i = 0; i < num_sets; i++)
                {
                    myset = Pointer2Index(src, encbase + i * 4 + 4);
                    encsets[i] = new EncounterSet(src, myset, file_offset);
                }

                //for (int i = 0; i < num_sets; i++)
                //    updateBox.AppendText(string.Format("\r\n===Encounter Set {0}===\r\n{1}", i, encsets[i].ToString()));


                return encsets;
            }
            else
                return null;
            
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
