using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTech
{
    internal class Producto
    {
        public string nombre { get; set; }
        public int nImg { get; set; }

        public Producto()
        {
            nombre = "";
            nImg = 0;
        }

        public Producto(string _nom, int _nImg)
        {
            nombre = _nom;
            nImg = _nImg;
        }
    }
}
