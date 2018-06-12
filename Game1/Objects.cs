using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Objects
    {
        public bool[,] ar;
        public int n;
        public int m;
       public Objects(int type)
        {

            switch (type)
            {
                case 1:
                    n = 2;m =4;
                    ar = new bool[2, 4];
                    ar[0,0] = true; ar[0, 1] = true; ar[0, 2] = true; ar[0, 3] = true;
                    break;
                case 2:
                    n = 3; m = 3;
                    ar = new bool[3, 3];
                    ar[0, 0] = true; ar[1, 0] = true; ar[1, 1] = true; ar[1, 2] = true;
                    break;
                case 3:
                    n = 3; m = 3;
                    ar = new bool[3, 3];
                    ar[0, 2] = true; ar[1, 0] = true; ar[1, 1] = true; ar[1, 2] = true;
                    break;
                case 4:
                    n = 2; m =2 ;
                    ar = new bool[2, 2];
                    ar[0, 0] = true; ar[1, 0] = true; ar[1, 1] = true; ar[0, 1] = true;
                    break;
                case 5:
                    n = 3; m = 3;
                    ar = new bool[3, 3];
                    ar[0, 1] = true; ar[0, 2] = true; ar[1, 1] = true; ar[1, 0] = true;
                    break;
                case 6:
                    n = 3; m = 3;
                    ar = new bool[3, 3];
                    ar[0, 1] = true; ar[1, 0] = true; ar[1, 1] = true; ar[1, 2] = true;
                    break;
                case 7:
                    n = 3; m = 3;
                    ar = new bool[3, 3];
                    ar[0, 0] = true; ar[0, 1] = true; ar[1, 1] = true; ar[1, 2] = true;
                    break;

            }
        }

        public static Objects getObj(int type)
        {
            return new Objects(type);
        }

    }
}
