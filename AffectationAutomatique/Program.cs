using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffectationAutomatique
{
    class Program
    {
        static void Main(string[] args)
        {
            //liste des juges ne sont pas encore affecté
            List<Judge> judges = new List<Judge>();
            judges.Add(new Judge() { id = 1, score = 180, tribunalId=0, oldTribunalId=2 });
            judges.Add(new Judge() { id = 2, score = 280, tribunalId = 0, oldTribunalId = 3 });
            judges.Add(new Judge() { id = 3, score = 90, tribunalId = 0, oldTribunalId = 2 });
            judges.Add(new Judge() { id = 4, score = 120, tribunalId = 0, oldTribunalId = 3 });

            List<Tribunal> tribunals = new List<Tribunal>();
            tribunals.Add(new Tribunal() { id = 1, priorité = 4, besoinNumber = 1 });
            tribunals.Add(new Tribunal() { id = 2, priorité = 6, besoinNumber = 0 });
            tribunals.Add(new Tribunal() { id = 3, priorité = 1, besoinNumber = 3 });

            // le trie des juges
            judges.Sort(delegate (Judge x, Judge y)
            {
                return y.score.CompareTo(x.score);
            });

            //affectation
            foreach (Judge judge in judges)
            {
                //ajouter 1 au nombre de besoin de l'ancienne tribunal du juge
                tribunals.Where(w => w.id == judge.oldTribunalId).First().besoinNumber++;
                //affecter le juge à 1er tribunal à un besoin des juges
                judge.tribunalId = tribunals.Where(w => w.besoinNumber > 0).OrderByDescending(w => w.priorité).First().id;
                //soustraire 1 au nombre de besoin de nouveau tribunal
                tribunals.Where(w => w.id == judge.tribunalId).First().besoinNumber--;
            }


            foreach (Judge judge in judges)
            {
                Console.WriteLine("id : " + judge.id + " score : " + judge.score + " nouveau tribunal : " + judge.tribunalId + " ancienne tribunal : " + judge.oldTribunalId);
            }
            Console.WriteLine();
        }
    }
}
