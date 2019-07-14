using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaAnalyzer
{
    
    class EncounterSet
    {
        readonly int FORM_OFFSET = 4;
        readonly int ENEMY_OFFSET = 8;
        public int base_addr = 0;
        public int offset;
        public int num_enemies;
        public double num_formations;
        public Enemy[] enemies;
        public List<Formation> formations;
        public int maxlevel;
        public int maxpartylevel;

        public EncounterSet(byte[] src, int newbase, int file_offset)
        {
            base_addr = newbase;
            offset = file_offset;

            int entity_base = Pointer2Index(src, base_addr + ENEMY_OFFSET);
            num_enemies = Pointer2DWord(src, entity_base);
            enemies = new Enemy[num_enemies+1];
            int entity;
            enemies[0] = new Enemy();
            maxpartylevel = 0;
            maxlevel = 0;

            for(int i = 1; i <= num_enemies; i++)
            {
                entity = Pointer2Index(src,entity_base + i * 4);
                enemies[i] = new Enemy(src, entity, i);
            }

            entity_base = Pointer2Index(src, base_addr + FORM_OFFSET);
            num_formations = Pointer2DWord(src, entity_base);
            //formations = new Formation[num_formations];
            formations = new List<Formation>();
            Formation myform;

            for (int i = 0; i < num_formations; i++)
            {
                entity = Pointer2Index(src, entity_base + i * 4+4);
                if (!FormationExists(entity))
                {
                    myform = new Formation(src, entity, i, enemies);
                    formations.Add(myform);
                    maxlevel = Math.Max(maxlevel, myform.maxlevel);
                    maxpartylevel = Math.Max(maxpartylevel, myform.level);
                }
            }

        }

        private bool FormationExists(int entity)
        {
            bool found = false;
            formations.ForEach((delegate (Formation f)
            {
                if (f.index == entity)
                {
                    f.instances++;
                    found = true;
                }
            }));

            if (found)
                return true;

            return false;
        }

        public override string ToString()
        {
            string mystring="";
            int i = 1 ;

            formations.ForEach((delegate (Formation f)
            {
                mystring += String.Format("Formation {0} - {1:0.0%} - [{2}, {3}]  {4}\r\n\r\n", i++,f.instances/num_formations, f.level, f.maxlevel, f.ToString());
            }));

            return mystring;
        }

        public int Pointer2Index(byte[] fileBytes, int v)
        {
            return fileBytes[v] + 256 * fileBytes[v + 1] + 256 * 256 * fileBytes[v + 2] - offset;
        }

        public int Pointer2DWord(byte[] src, int v)
        {
            return src[v] + 256 * src[v + 1] + 256 * 256 * src[v + 2] + 256 * 256 * 256 * src[v + 3];
        }
    }
}
