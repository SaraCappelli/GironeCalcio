using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GironeCalcio
{
    public class Partita
    {
        // TODO: migliore struttura dati
        readonly KeyValuePair<Squadra, InfoSquadra> _squadraA;
        readonly KeyValuePair<Squadra, InfoSquadra> _squadraB;
        public Squadra? Vincitore { get; private set; }

        readonly int _diffReti;
        public bool Pareggio { get; private set; }
        public override string ToString()
        {
            return $"{_squadraA.Key.Nome} vs {_squadraB.Key.Nome}: {_squadraA.Value.Reti} - {_squadraB.Value.Reti}";
        }
        public Squadra? this[Squadra p]
        {
            get
            {
                if (_squadraA.Key == p)
                {
                    return _squadraA.Key;
                }
                else if (_squadraB.Key == p)
                {
                    return _squadraB.Key;
                }
                return null;
            }
        }

        public Partita(Squadra a, Squadra b)
        {
            _squadraA = new();
            _squadraB = new();
            
            _diffReti = _squadraA.Value.Reti - _squadraB.Value.Reti;
            Pareggio = _diffReti == 0;

            if (!Pareggio)
            Vincitore = _diffReti > 0 ? a : b;
            else Vincitore = null;
            ModificaPunteggi();
        }
        void ModificaPunteggi()
        {
            if (Vincitore is not null) Vincitore.Punteggio += 3;
            if (Pareggio)
            {
                _squadraA.Key.Punteggio += 1;
                _squadraB.Key.Punteggio += 1;
            }
            _squadraA.Key.NumeroReti += _squadraA.Value.Reti;
            _squadraB.Key.NumeroReti += _squadraB.Value.Reti;

            _squadraA.Key.DiffReti += _diffReti;
            _squadraB.Key.DiffReti -= _diffReti;

            _squadraA.Key.PunteggioCondotta += (int)_squadraA.Value.Penalità;
            _squadraB.Key.PunteggioCondotta += (int)_squadraB.Value.Penalità;
        }

        public struct InfoSquadra
        {
            public int Reti { get; private set; }
            public Penalità Penalità { get; private set; }
            public int DiffReti { get; private set; }

            public InfoSquadra()
            {
                Random r = new();
                Reti = r.Next(5);
                Penalità = (Penalità)r.Next(0, Enum.GetValues(typeof(Penalità)).Length);
            }
        }
        public enum Penalità
        {
            Nessuna = 0,
            Ammonizione = -1,
            EspulsioneIndiretta = -3,
            EspulsioneDiretta = -4,
            EspulsioneDirettaDopoAmmonizione = -5,
        }
    }
}
