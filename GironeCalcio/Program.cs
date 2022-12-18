using GironeCalcio;

string percorsoFileSquadre = "../../../squadre.txt";

string[] letturaSquadre= File.ReadAllLines(percorsoFileSquadre);
Dictionary<string, Squadra> squadre = letturaSquadre.Select(nome => new Squadra(nome)).ToDictionary(sq => sq.Nome);


// TODO: perfavore sta roba no
Girone.Gioca(squadre.Keys.Select(k => squadre[k]).ToList());