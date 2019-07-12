using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaAnalyzer
{
    class Enemy
    {
        public int index;
        public int set_id;
        public string name;

        public Enemy()
        {
            index = 0;
            set_id = 0;
            name = "---";
        }

        public Enemy(byte[] src, int val, int myid)
        {
            index = val;
            set_id = myid;
            name = StringDecode(src, index);
        }

        private string StringDecode(byte[] src, int index)
        {
            string mystring = "";
            while (src[index] != 0)
            {
                mystring += CharSwap(src[index++]);
            }

            return mystring;
        }

        public override string ToString()
        {
            return name;
        }

        public char CharSwap(byte mychar)
        {
            switch (mychar)
            {
                case 0x10:
                    return ' ';
                case 0x11:
                    return 'a';
                case 0x12:
                    return 'b';
                case 0x13:
                    return 'c';
                case 0x14:
                    return 'd';
                case 0x15:
                    return 'e';
                case 0x16:
                    return 'f';
                case 0x17:
                    return 'g';
                case 0x18:
                    return 'h';
                case 0x19:
                    return 'i';
                case 0x1A:
                    return 'j';
                case 0x1B:
                    return 'k';
                case 0x1C:
                    return 'l';
                case 0x1D:
                    return 'm';
                case 0x1E:
                    return 'n';
                case 0x1F:
                    return 'o';
                case 0x20:
                    return 'p';
                case 0x21:
                    return 'q';
                case 0x22:
                    return 'r';
                case 0x23:
                    return 's';
                case 0x24:
                    return 't';
                case 0x25:
                    return 'u';
                case 0x26:
                    return 'v';
                case 0x27:
                    return 'w';
                case 0x28:
                    return 'x';
                case 0x29:
                    return 'y';
                case 0x2A:
                    return 'z';
                case 0x3B:
                    return 'A';
                case 0x3C:
                    return 'B';
                case 0x3D:
                    return 'C';
                case 0x3E:
                    return 'D';
                case 0x3F:
                    return 'E';
                case 0x40:
                    return 'F';
                case 0x41:
                    return 'G';
                case 0x42:
                    return 'H';
                case 0x43:
                    return 'I';
                case 0x44:
                    return 'J';
                case 0x45:
                    return 'K';
                case 0x46:
                    return 'L';
                case 0x47:
                    return 'M';
                case 0x48:
                    return 'N';
                case 0x49:
                    return 'O';
                case 0x4A:
                    return 'P';
                case 0x4B:
                    return 'Q';
                case 0x4C:
                    return 'R';
                case 0x4D:
                    return 'S';
                case 0x4E:
                    return 'T';
                case 0x4F:
                    return 'U';
                case 0x50:
                    return 'V';
                case 0x51:
                    return 'W';
                case 0x52:
                    return 'X';
                case 0x53:
                    return 'Y';
                case 0x54:
                    return 'Z';
                case 0x98:
                    return '0';
                case 0x99:
                    return '1';
                case 0x9A:
                    return '2';
                case 0x9B:
                    return '3';
                case 0x9C:
                    return '4';
                case 0x9D:
                    return '5';
                case 0x9E:
                    return '6';
                case 0x9F:
                    return '7';
                case 0xA0:
                    return '8';
                case 0xA1:
                    return '9';
                case 0x66:
                    return '.';
                default:
                    return '-';

            }
        }
    }
}
