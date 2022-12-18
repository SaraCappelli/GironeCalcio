using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GironeCalcio
{
    public static class Girone
    {
        static readonly List<Squadra> _listaSquadre = new();
        static readonly List<Partita> _listaPartite = new();
        public static ClassificaSquadre ClassificaSquadre { get; } = new();
        // non indico la variabile come static perché lo è di natura, essendo cost(ante)
        const int _numeroSquadrePassanti = 2;
        public static List<Squadra> SquadrePassanti { get; } = new();

        static void AggiungiPartita(Partita p) => _listaPartite.Add(p);
        static void DeterminaSquadrePassanti()
        {
            if (ClassificaSquadre.Count >= _numeroSquadrePassanti)
            {
                for (int i = 0; i < _numeroSquadrePassanti; i++) SquadrePassanti.Add(ClassificaSquadre[i]);
            }
        }
        static void GeneraPartite(List<Squadra> squadre)
        {
            foreach (var sq in squadre)
            {
                _listaSquadre.Add(sq);
            }
            foreach (var couple in GeneraCombinazioni(squadre.Count))
            {
                AggiungiPartita(new Partita(_listaSquadre[couple.Item1], _listaSquadre[couple.Item2]));
            }
        }
        //TODO: descrizioni dei metodi con summary
        static List<(int, int)> GeneraCombinazioni(int upbound)
        {
            var ret = new List<(int, int)>();
            for (int i = 0; i < upbound - 1; i++)
            {
                for (int y = i + 1; y < upbound; y++)
                {
                    ret.Add((i, y));
                }
            }
            return ret;
        }
        public static Partita? TrovaPartita(Squadra a, Squadra b)
        {
            return _listaPartite.Find(p => p[a] != null && p[b] != null);
        }
        public static void Gioca(List<Squadra> squadre)
        {
            GeneraPartite(squadre);
            _listaPartite.ForEach(Console.WriteLine);

            Console.WriteLine();

            ClassificaSquadre.Calcola(_listaSquadre);
            DeterminaSquadrePassanti();
            ClassificaSquadre.Stampa();
        }
    }
}
