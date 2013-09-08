using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public class UlazniObrazacIzDatoteke : UlazniObrazac
    {
        public UlazniObrazacIzDatoteke(string name) : base(name)
        { }

        /// <summary>
        /// Jednostavan obrazac, koji jednostavno preslika ono sto je pronasao u opisu obrasca
        /// </summary>
        /// <returns></returns>
        public override string generateInput()
        {
            return _sadrzajObrasca;
        }
    }
}
