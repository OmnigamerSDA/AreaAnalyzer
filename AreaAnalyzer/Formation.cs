using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaAnalyzer
{
    class Formation
    {
        public Enemy[] enemies;
        public int set_id;
        public int index;

        public Formation(Byte[] src, int newbase, int myid, Enemy[] enemylist)
        {
            index = newbase;
            set_id = myid;

            int entity;

            enemies = new Enemy[6];

            for(int i = 0; i < 6; i++)
            {
                if (index < src.Length && index > 0)
                    entity = src[index + 10 + i];
                else
                    entity = 0;

                if (entity != 0)
                {
                    enemies[i] = enemylist[entity];
                }
                else
                {
                    enemies[i] = new Enemy();
                }
            }
        }

        public override string ToString()
        {
            //int charlen = 4;
            //return String.Format("{0}{1}{2}{3}{4}{5}", new string(enemies[0].ToString().Take(charlen).ToArray()), new string(enemies[1].ToString().Take(charlen).ToArray()), new string(enemies[2].ToString().Take(charlen).ToArray()), new string(enemies[3].ToString().Take(charlen).ToArray()), new string(enemies[4].ToString().Take(charlen).ToArray()), new string(enemies[5].ToString().Take(charlen).ToArray()));
            string mystring = "\r\n";

            for(int i = 0; i < 6; i++)
            {
                mystring += enemies[i].ToString() + " ";
            }

            //mystring += "\r\n";

            //for (int i = 3; i < 6; i++)
            //{
            //    mystring += enemies[i].ToString() + " ";
            //}

            return mystring;
        }
    }
}
