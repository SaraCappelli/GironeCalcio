using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GironeCalcio
{
    public class ClassificaSquadre : IEnumerable
    {
        readonly List<Squadra> _classifica;
        List<Squadra> _listaSquadre;

        public int Count => _listaSquadre.Count;

        public Squadra this[int index]
        {
            get
            {
                return _classifica[index];
            }
        }
        public ClassificaSquadre()
        {
            _classifica = new();
        }
        public void Calcola(List<Squadra> squadre)
        {
            _listaSquadre = squadre;
            squadre.ForEach(Aggiungi);
        }
        public void Stampa()
        {
            for (int i = 0; i < _listaSquadre.Count; i++)
                Console.WriteLine($"{i+1}: {_classifica[i]}");
        }
        void Aggiungi(Squadra sq)
        {
            int index = 0;
            bool block = false;
            while (index < _classifica.Count && !block)
            {
                var sqi = _classifica[index];
                Partita? scontro = Girone.TrovaPartita(sqi, sq);

                block = sq.Punteggio > sqi.Punteggio;

                if (sq.Punteggio == sqi.Punteggio)
                {
                    if (sq.DiffReti > sqi.DiffReti) block = true;
                    else if (sq.DiffReti == sqi.DiffReti)
                    {
                        if (sq.NumeroReti > sqi.NumeroReti) block = true;
                        else if (sq.NumeroReti == sqi.NumeroReti)
                        {
                            if (scontro is not null && (scontro.Vincitore == sq)) block = true;
                            else if (scontro.Pareggio)
                            {
                                if (new Random().Next(0, 2) > 0) block = true;
                            }
                        }
                    }
                }

                if (!block) index++;
            }

            if (index < _classifica.Count) _classifica.Insert(index, sq);
            else _classifica.Add(sq);
        }
        public IEnumerator GetEnumerator()
        {
            // dall'esterno è possibile modificarla?
            return _listaSquadre.GetEnumerator();
        }
    }
}
