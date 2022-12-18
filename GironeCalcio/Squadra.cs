using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GironeCalcio
{
    public class Squadra
    {
        public string Nome { get; private set; }
        public int Punteggio { get; set; }
        public int DiffReti { get; set; }
        public int NumeroReti { get; set; }
        public int PunteggioCondotta { get; set; }
        public Squadra(string nome)
        {
            Nome = nome;
            Punteggio = 0;
            DiffReti = 0;
            NumeroReti = 0;
            PunteggioCondotta = 0;
        }
        public override string ToString()
        {
            return $"{Nome} con {Punteggio} punti.";
        }
    }
}
